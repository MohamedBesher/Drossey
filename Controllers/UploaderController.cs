using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drossey.Controllers
{

    [Route("[controller]/[action]")]
    [Authorize(Roles = "User")]
    [ApiExplorerSettings(IgnoreApi = true)]


    public class UploadeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadeController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile()
        {

            //
            string filename = "";
            string filePath = "";

            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var files = _httpContextAccessor.HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //file.FileName
                          filename = $"{Guid.NewGuid()}.png";
                         filePath = Path.Combine(uploads, filename);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
            return Json(filename);

            }

            else
               return Json(filename);

        }



        private void DeleteImage(string imageUrl)
        {
            string slogn = "/Uploads/";
            slogn += imageUrl;
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath,slogn);
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
        [HttpGet]
        public IActionResult Save()
        {
            return Ok();
        }
    
}
}