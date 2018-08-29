using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using Drossey.Data.Core.Dto;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin.Services;

namespace Drossey.Api.Controllers
{
    [Route("api/payment")]
    [Area("API")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class PaymentController : BaseController
    {
        private readonly IWizIQSender _wizIQSender;


        public PaymentController(IUnitOfWorkAsync unitOfWork, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper, IWizIQSender wizIQSender)
            : base(unitOfWork, userMgr, hasher, logger, config, mapper)
        {
            _wizIQSender = wizIQSender;

        }




        [HttpGet]
        public async Task<IActionResult> Pay()
        {

            try
            {
                var userId = await GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var applicationUser = _unitOfWork.UserRepository.Find(userId);
                var model = _unitOfWork.CartRepository.GetAllCarts(userId);

                if (model.Any())
                {
                    var total = Convert.ToInt32(model.Sum(u => (u.Price - ((u.Price * u.DiscountPercentage) / 100))));
                    var balance = applicationUser.Balance;
                    if (total > balance)
                    {
                        return BadRequest("NotEnoughBalance");
                    }
                    else
                    {
                        List<PaymentBookDto> bookids = model.Select(u => new PaymentBookDto()
                        {
                            Id = u.Id,
                            Price = u.Price,
                            DiscountPercentage = u.DiscountPercentage
                        }).ToList();
                        await paybooksAsync(bookids, total, userId, $"{applicationUser.FirstName} {applicationUser.LastName}");
                        return Ok();
                    }
                }
                else
                    return NotFound("NoElementsInCart");

            }
            catch (Exception e)
            {

                return StatusCode(500,e.Message);

            }



        }


        private async Task paybooksAsync(List<PaymentBookDto> list, int total, string userId, string userName)
        {
            List<TransactionDetails> transactionsDetails = new List<TransactionDetails>();
            List<Cart> removedCart = new List<Cart>();

            foreach (var book in list)
            {
                transactionsDetails.Add(new TransactionDetails()
                {
                    SubjectId = book.Id,
                    Price = Convert.ToInt32(book.Price - ((book.Price * book.DiscountPercentage) / 100)),
                    DisCountPertcentage = ((book.Price * book.DiscountPercentage) / 100)
                });
                var selectedCart = _unitOfWork.CartRepository.All().FirstOrDefault(u => u.SubjectId == book.Id);

                if (selectedCart != null)

                    removedCart.Add(selectedCart);

            }


            var transaction = new Transaction()
            {
                Total = total,
                UserId = userId,
                TransactionDetails = transactionsDetails
            };
            _unitOfWork.TransactionRepository.Create(transaction);
            _unitOfWork.CartRepository.DeleteRange(removedCart);
            var applicationUser = _unitOfWork.UserRepository.Find(userId);
            applicationUser.Balance -= total;
            if ((await _unitOfWork.CommitAsync()))
            {

                AddBookLessonsToUser(list, userName, userId);
            }


        }

        private void AddBookLessonsToUser(List<PaymentBookDto> list, string username, string userId)
        {
            var bookIds = list.Select(g => g.Id);
            List<PaymentLiveDto> liveListIds = _unitOfWork.LiveLessonRepository.All()
                 .Include(u => u.Lesson)
                 .ThenInclude(u => u.Module)
                 .Where(u => bookIds.Contains(u.Lesson.Module.SubjectId))
                 .Select(u => new PaymentLiveDto()
                 { ClassId = u.Class_id, LiveClassId = u.Id }).ToList();
            //.Select(u => u.LiveLesson.Class_id).ToList();

            liveListIds.ForEach(u => Add_Attendees(username, userId, u.ClassId.ToString(), u.LiveClassId));
        }

        private void Add_Attendees(string userName, string userId, string classId, long? liveClassId)
        {
            int max = _unitOfWork.UserLiveLessonRepository.All().Count(u => u.LiveLessonId.ToString() == classId);
            var tuple = _wizIQSender.Add_Attendees(userName, (max + 1).ToString(), classId);
            if (tuple.Item2 == "ok")
            {
                _unitOfWork.UserLiveLessonRepository.Create(new UserLiveLesson()
                {
                    LiveLessonId = liveClassId.Value,
                    AttendUrl = tuple.Item1,
                    UserId = userId
                });
                _unitOfWork.Commit();
            }
            else
                return;



        }
    }

}