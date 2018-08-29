using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drossey.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    public class BaseController : Controller
    {

        public readonly SignInManager<ApplicationUser> _signInMgr;
        public readonly UserManager<ApplicationUser> _userMgr;
        public readonly IPasswordHasher<ApplicationUser> _hasher;
        public readonly IConfiguration _config;
        public IUnitOfWorkAsync _unitOfWork;
        public readonly IMapper _mapper;
        public ILogger<BaseController> _logger;
        public readonly IMessenger _messenger;
        public readonly IHostingEnvironment _hostingEnvironment;

        public BaseController(IUnitOfWorkAsync unitOfWork,
            SignInManager<ApplicationUser> signInMgr,
            UserManager<ApplicationUser> userMgr,
            IPasswordHasher<ApplicationUser> hasher,
            IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _signInMgr = signInMgr;
            _userMgr = userMgr;
            _hasher = hasher;
            _config = config;
            _mapper = mapper;
            _logger = logger;
            _messenger = messenger;
            _hostingEnvironment = hostingEnvironment;
        }



        protected bool CheckIfFileExists(string url,int type=0)
        {
            if (type==0)
            {
                // content path wwwroot folder
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string filePath = uploads+"\\"+ url;
            return System.IO.File.Exists(filePath);
            }
            else
            {
                // content path not in wwwroot folder
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, url);
            return System.IO.File.Exists(filePath);
            }
        }

        protected bool CreateFolder(long id, int type = 0)
        {
            try
            {
                // Lessons
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons", id.ToString());
                //quiz
                if (type == 1)
                    path = Path.Combine(_hostingEnvironment.ContentRootPath, "Quiz", id.ToString());

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        protected bool DeleteFolder(long id, int type = 0)
        {
            try
            {
                // Lessons
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Lessons", id.ToString());
                //quiz
                if (type == 1)
                    path = Path.Combine(_hostingEnvironment.ContentRootPath, "Quiz", id.ToString());

                if (Directory.Exists(path))
                    Directory.Delete(path);

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }


        protected string GetFullPath(string imageUrl)
        {

            //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            //string filePath = uploads + "\\" + imageUrl;
            //return filePath;
            var uploads = "/uploads";
            string filePath = uploads + "/" + imageUrl;
            return filePath;


        }

        protected async Task<ApplicationUser> GetCurrentUser()
        {
            var user = await _userMgr.GetUserAsync(HttpContext.User);
            return user;
        }
        protected void DeleteImage(string imageUrl)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            string filePath = uploads + "\\" + imageUrl;
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                    throw;
                }

            }

        }


        protected Task<ApplicationUser> GetCurrentUserAsync() => _userMgr.GetUserAsync(HttpContext.User);

        protected string GetMIMEtype(string extension)
        {
            string mimetype = "";
            switch (extension)
            {
                case ".js":
                    mimetype = "text/javascript";
                    break;

                case ".css":
                    mimetype = "text/css";
                    break;

                case ".ttf":
                    mimetype = "application/octet-stream";
                    break;
                case ".woff":
                    mimetype = "application/octet-stream";
                    break;

                case ".woff2":
                    mimetype = "application/octet-stream";
                    break;
                case ".html":
                    mimetype = "text/html";
                    break;

                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpeg":
                    mimetype = " image/jpeg";
                    break;


                case ".png":
                    mimetype = " image/png";
                    break;
                case ".svg":
                    mimetype = " image/svg+xml";
                    break;


                case ".wav":
                    mimetype = "audio/wav";
                    break;

                case ".x-wav":
                    mimetype = "audio/x-wav";
                    break;

                case ".x-pn-wav":
                    mimetype = "audio/x-pn-wav";
                    break;

                case ".webm":
                    mimetype = "video/webm";
                    break;

                case ".ogg":
                    //mimetype = "video/ogg";
                    mimetype = "mp3";
                    break;

                case ".mp3":
                    mimetype = "mp3";

                    // mimetype = "audio/mpeg";
                    break;

                case ".mp4":
                    mimetype = "video/mp4";
                    break;

                default:
                    break;
            }

            return mimetype;
        }
        protected void GetParameters(List<string> parameters, ref string path, int end, ref string extension)
        {
            for (int i = 0; i < end; i++)
            {
                path += "/" + parameters[i];
                if (i == (end - 1))
                    extension = Path.GetExtension(parameters[i]);
            }
        }

        public void LoadCountries()
        {
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.All(), "Id", "Name");

        }
        public void LoadGrades()
        {
            ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.All(), "Id", "Name");

        }

        public void LoadGrades(long countryId)
        {
            ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.Filter(u=>u.CountryId == countryId), "Id", "Name");

        }
        public void LoadTerms(long gradeId)
        {
            ViewData["terms"] = new SelectList(_unitOfWork.TermRepository.Filter(u => u.GradeId == gradeId), "Id", "Name");

        }
        public void LoadLessons(long moduleId)
        {
            ViewData["lessons"] = new SelectList(_unitOfWork.LessonRepository.Filter(u => u.ModuleId == moduleId), "Id", "Name");

        }

        public void LoadBooks(long subjectId)
        {
          ViewData["books"] = new SelectList(_unitOfWork.BookRepository.Filter(u => u.SubjectId == subjectId), "Id", "Name");
        }

        public void LoadSubjects(long termId)
        {
            ViewData["subjects"] = new SelectList(_unitOfWork.SubjectRepository.Filter(u => u.TermId ==termId), "Id", "Name");
        }
        public void LoadSellers()
        {
            ViewData["sellers"] = new SelectList(_unitOfWork.SellerRepository.Filter(u => u.IsActive), "Id", "Name");
        }






    }
}