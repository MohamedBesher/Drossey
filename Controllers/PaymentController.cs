using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Drossey.Areas.admin.Controllers;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using Microsoft.AspNetCore.Authorization;
using Drossey.Models;
using Drossey.Data.Core.Dto;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin.Services;

namespace Drossey.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    [Authorize(Roles = "User")]
    public class PaymentController : BaseController
    {
        private readonly IWizIQSender _wizIQSender;

        public PaymentController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment, IWizIQSender wizIQSender)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
            _wizIQSender = wizIQSender;

        }

        public IActionResult Index(string message="")
        {

            var userId = _userMgr.GetUserId(HttpContext.User);
            var applicationUser=_unitOfWork.UserRepository.Find(userId);
            var model = _unitOfWork.CartRepository.GetAllCarts(userId);
            ViewBag.total = model.Sum(u => u.Price);
           decimal totalafter = model.Sum(u => (u.Price - ((u.Price * u.DiscountPercentage) / 100)));
            ViewBag.totalafter =Convert.ToInt32(totalafter);
            ViewBag.balance = applicationUser.Balance;
            ViewBag.message = message;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Pay()
        {
         
            var message = "";
           var userId = _userMgr.GetUserId(HttpContext.User);
            var applicationUser = _unitOfWork.UserRepository.Find(userId);
            var model = _unitOfWork.CartRepository.GetAllCarts(userId);
            
            var total = Convert.ToInt32(model.Sum(u => (u.Price-((u.Price * u.DiscountPercentage)/100 ))));
            var balance = applicationUser.Balance;
            if (total > balance)
            {
                message = "لا يمكن اتمام العملية . رصيدك غير كافى";
                return RedirectToAction(nameof(Index),new {message= message });
            }
            else
            {
            List<PaymentBookDto> bookids = model.Select(u => new PaymentBookDto()
               {
                Id =u.Id,
                Price =u.Price,
                DiscountPercentage = u.DiscountPercentage
               }).ToList();
            await paybooksAsync(bookids, total, userId,$"{applicationUser.FirstName} {applicationUser.LastName}");
            return RedirectToAction("Index", "MyBooks");
            }

           
        }


        private async Task paybooksAsync(List<PaymentBookDto> list,int total,string userId,string userName)
        {
            //list.ForEach(PayBook);
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

                if (selectedCart!=null)

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

                AddBookLessonsToUser(list, userName,userId);
            }


        }

        private void AddBookLessonsToUser(List<PaymentBookDto> list,string username,string userId)
        {
            var bookIds = list.Select(g => g.Id);
            List<PaymentLiveDto> liveListIds = _unitOfWork.LiveLessonRepository.All()
                 .Include(u => u.Lesson)
                 .ThenInclude(u=>u.Module)
                 .Where(u => bookIds.Contains(u.Lesson.Module.SubjectId) )
                 .Select(u => new PaymentLiveDto()
                 { ClassId = u.Class_id, LiveClassId = u.Id }).ToList();
                //.Select(u => u.LiveLesson.Class_id).ToList();

            liveListIds.ForEach(u => Add_Attendees(username,userId, u.ClassId.ToString(),u.LiveClassId));
        }

        private void Add_Attendees(string userName,string userId, string classId,long? liveClassId)
        {
            int max=_unitOfWork.UserLiveLessonRepository.All().Count(u => u.LiveLessonId.ToString() == classId);
            var tuple=_wizIQSender.Add_Attendees(userName, (max + 1).ToString(), classId);
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





        //// GET: Movies/Delete/5
        //public  IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var book =  _unitOfWork.CartRepository.GetOneCart(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    var BookModel = _mapper.Map<Book, BooksViewModel>(book);
        //    return View(BookModel);
        //}

        //// POST: Movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var cart = _unitOfWork.CartRepository.Find(c=>c.BookId == id);
        //    _unitOfWork.CartRepository.Delete(cart);
        //    _unitOfWork.Commit();

        //    return RedirectToAction(nameof(Index));
        //}


    }
}