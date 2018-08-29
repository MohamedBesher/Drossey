using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Drossey.Models.Api.Models;
using Drossey.Models;
using Drossey.Data.Core.Enum;
using Microsoft.EntityFrameworkCore;

namespace Drossey.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Cart")]
    [Area("API")]

    public class CartController : BaseController
    {
       

        public CartController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) 
            : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var userId = await GetUserId();
                var model = _unitOfWork.CartRepository.GetAllCarts(userId);
                var total = Convert.ToInt32(model.Sum(u => (u.Price - ((u.Price * u.DiscountPercentage) / 100))));
                var cart = model.ToList();
                var result = new { Sum = total, Cart = cart };
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
          
        }

        [HttpGet("{id}/{status}")]
        public async Task<IActionResult> Create(long id,bool status)
        {
            try
            {
                var userId = await GetUserId();
                var subject = _unitOfWork.SubjectRepository.Find(id);
                if (subject == null)
                    return NotFound();



                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var result = _unitOfWork.CartRepository.All().FirstOrDefault(u => u.SubjectId == id && u.UserId == userId);
                
                if (status)
                {
                    if (result == null)
                        return NotFound();
                    _unitOfWork.CartRepository.Delete(result);
                }
                //add
                else
                {
                    var trans = _unitOfWork.TransactionDetailsRepository.All().Include(u => u.Transaction)
                        .Where(u => u.Transaction.UserId == userId).Any(u => u.SubjectId == id);
                    if (!trans && result == null)
                    {
                        var cart = new Cart()
                        {
                            SubjectId = id,
                            Price = subject.Price,
                            UserId = userId,
                            Status = CartStatus.New
                        };

                        _unitOfWork.CartRepository.Create(cart);
                    }


                }
                var price = Convert.ToInt32(subject.Price - ((subject.Price * subject.DiscountPercentage) / 100));
                _unitOfWork.Commit();
                return Ok(price);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
          



        }



        [HttpGet("count")]
        public async Task<IActionResult> GetUserCartCount()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = await GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var u = await _userMgr.FindByIdAsync(userId);
                var count = _unitOfWork.CartRepository.All().Where(u => u.UserId == userId).Count();


                return Ok(count);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return BadRequest("GetUserCart --- " + msg);
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(long? id)
        //{

        //    try
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }
        //        var userId = await GetUserId();
        //        var book = _unitOfWork.CartRepository.Find(id);
        //        if (book == null)               
        //            return NotFound();

        //        if (book.UserId != userId)
        //            return Unauthorized();

        //        _unitOfWork.CartRepository.Delete(book);
        //        await _unitOfWork.CommitAsync();
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }

        //}
    }

}