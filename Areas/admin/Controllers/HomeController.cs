using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Authorization;
using MotleyFlash;
using MotleyFlash.Extensions;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Drossey.Models;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class HomeController : BaseController
    {


        

        public HomeController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.users = (await _userMgr.GetUsersInRoleAsync("User")).Count();
            ViewBag.transactions =_unitOfWork.TransactionRepository.All().Sum(u => u.Total);
            ViewBag.teachers = _unitOfWork.TeacherRepository.All().Count(u=>u.Is_active);
            ViewBag.countries = _unitOfWork.CountryRepository.All().Count(u => u.IsPuplished);
            ViewBag.grades = _unitOfWork.GradeRepository.All().Count(u=>u.IsPuplished);
            ViewBag.terms = _unitOfWork.TermRepository.All().Count(u => u.IsPuplished);
            ViewBag.subjects = _unitOfWork.SubjectRepository.All().Count(u => u.IsPuplished);
            ViewBag.books = _unitOfWork.BookRepository.All().Count(u => u.IsPuplished);
            ViewBag.lessons = _unitOfWork.LessonRepository.All().Count(u => u.IsPuplished);
            ViewBag.livelessons = _unitOfWork.LiveLessonRepository.All().Count();
            ViewBag.codes = _unitOfWork.PinCodeRepository.All().Count();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult test()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}
