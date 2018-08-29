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
using Drossey.Data.Core.Enum;

namespace Drossey.Controllers
{
   [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class CartController : BaseController
    {
        public CartController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            var User = _userMgr.GetUserId(HttpContext.User);
            var model = _unitOfWork.CartRepository.GetAllCarts(User);
            ViewBag.total = Convert.ToInt32(model.Sum(u => (u.Price - ((u.Price * u.DiscountPercentage) / 100))));
            ViewBag.TotalPtice = Convert.ToInt32(model.Sum(u => (u.Price )));
            var cart = model.AsEnumerable();
            return View(cart);
        }
        public IActionResult Create(int id)
        {
            var User = _userMgr.GetUserId(HttpContext.User);
            var book = _unitOfWork.BookRepository.GetOneSubject(id);
            var cart = new Cart()
            {
                SubjectId = id,
                Price = book.Price,
                UserId = User,
                Status= CartStatus.New
            };
            _unitOfWork.CartRepository.Create(cart);
            _unitOfWork.Commit();
            var price = Convert.ToInt32(book.Price - ((book.Price * book.DiscountPercentage) / 100));

            return Json(price);

           

        }


        // GET: Movies/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  _unitOfWork.CartRepository.GetOneCart(id);
            if (book == null)
            {
                return NotFound();
            }
            //var BookModel = _mapper.Map<Book, BooksViewModel>(book);
            //BookModel
            return View(book);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cart = _unitOfWork.CartRepository.Find(c=>c.SubjectId == id);
            var book = _unitOfWork.CartRepository.GetOneCart(id);
            _unitOfWork.CartRepository.Delete(cart);
            _unitOfWork.Commit();
           // var price = Convert.ToInt32(book.Price - ((book.Price * book.DiscountPercentage) / 100));
            
            return Json(book);

           // return RedirectToAction(nameof(Index));
        }


    }
}