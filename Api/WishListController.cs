using AutoMapper;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Drossey.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Area("API")]
    [Route("api/WishList")]
    public class WishListController : BaseController
    {
        public WishListController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, ILogger<AuthController> logger, IConfiguration config, IMapper mapper) : base(unitOfWork, signInMgr, userMgr, hasher, logger, config, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] Pager model)
        {
            try
            {
                var userId = await GetUserId();
                var total = 0;
                var subjects = _unitOfWork.FavoriteRepository.GetAllFavourite(out total, userId, model.Page, model.PageSize);
                return Ok(new { total = total, items = subjects });
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }


        [HttpGet("{id}/{status}")]
        public async Task<IActionResult> Create(long id,bool status=false)
        {
            try
            {
                var subject = _unitOfWork.SubjectRepository.Find(id);
                if (subject == null)
                    return NotFound();

                var userId = await GetUserId();
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();
                //remove 
                if (status)
                {
                   var result= _unitOfWork.FavoriteRepository.All().FirstOrDefault(u => u.BookId==id && u.UserId==userId);
                    if (result == null)
                        return NotFound();
                    _unitOfWork.FavoriteRepository.Delete(result);
                }
                //add
                else
                {
                    var wish = new Favorite(){BookId = id,UserId = userId};
                    _unitOfWork.FavoriteRepository.Create(wish);

                }
                _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
               
            }
          
            
        }



        //[HttpDelete("{id}")]
        //public async Task<IActionResult> delete(long id)
        //{
        //    try
        //    {
        //        var userId = await GetUserId();
        //        var subject = _unitOfWork.FavoriteRepository.Find(id);
        //        if (subject == null )
        //            return NotFound();

        //        if (subject.UserId != userId)
        //            return Unauthorized();

        //        _unitOfWork.FavoriteRepository.Delete(subject);
        //        _unitOfWork.Commit();
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);

        //    }


        //}
    }

}