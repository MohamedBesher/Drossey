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
using Drossey.Models;
using Microsoft.AspNetCore.Authorization;
using Drossey.Data.Core.Dto;

namespace Drossey.Controllers
{
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class WishListController : BaseController
    {
        public WishListController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }
        public IActionResult Index(int? page)
        {
            //var User = _userMgr.GetUserId(HttpContext.User);
            //var model = _unitOfWork.FavoriteRepository.GetAllFavourite(User).ToList();
            ////var BookModel = _mapper.Map<List<Subject>, List<SubjectsViewModel>>(model.ToList());
            //int pageSize = 10;


            ViewBag.page = page ?? 1;

            //profile navigation
            var user = _unitOfWork.UserRepository.GetOneUser(_userMgr.GetUserId(HttpContext.User));
            ViewData["userName"] = user.FirstName + ' ' + user.LastName;
            ViewData["Balance"] = user.Balance;
            ViewData["PhotoUrl"] = user.PhotoUrl;
            ViewData["Active"] = "wishList";

            //ViewBag.pageSize = pageSize;

            //ViewBag.ResultCount = model.Any() ? model.Count() : 0;

            //var Favorite = PaginatedList<SubjectDto>.Create(model.AsEnumerable(), page ?? 1, pageSize);
            //return View(Favorite);
            return View();
        }
        [HttpPost]
        public ViewComponentResult wishList(int page)
        {
            return ViewComponent("WishList", new
            {
                page = page

            });
        }

        public int Create(int id)
        {
            var User = _userMgr.GetUserId(HttpContext.User);
            var wish = new Favorite()
            {
                BookId = id,
                UserId = User
            };
            _unitOfWork.FavoriteRepository.Create(wish);
            _unitOfWork.Commit();
            return id;

        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _unitOfWork.FavoriteRepository.GetOnefavorite(id);
            if (book == null)
            {
                return NotFound();
            }
            //
            //var BookModel = _mapper.Map<Subject, SubjectsViewModel>(book);
            return View(book);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
            var favorite = _unitOfWork.FavoriteRepository.Find(c => c.BookId == id);
            _unitOfWork.FavoriteRepository.Delete(favorite);
            _unitOfWork.Commit();
            return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }
           
          
        }

    }
}