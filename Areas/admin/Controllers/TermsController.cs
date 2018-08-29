using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drossey.Data;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class TermsController : BaseController
    {
        public TermsController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment) : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
        }

        public IActionResult Index(SearchTermModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.GradeId = model.GradeId;
            ViewBag.grades = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");
            return View();
 
        }


        [HttpPost]
        public ViewComponentResult Search(SearchTermModel model)
        {
            return ViewComponent("SearchTerms", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, gradeId = model.GradeId });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long gradeId)
        {
            return ViewComponent("SearchTerms", new { pageSize = pageSize, page = page, keyword = keyword, gradeId = gradeId });
        }

        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = _unitOfWork.TermRepository.Find(id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        public IActionResult Create()
        {
            ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");
            return View("Create", new TermViewModel());
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TermViewModel Term)
        {

            if (_unitOfWork.TermRepository.All().Any(u => u.Name.ToLower() == Term.Name.ToLower() && u.GradeId==Term.GradeId))
            {
                ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");
                ModelState.AddModelError("", "هذا الترم مسجل من قبل .");
                return View(Term);
            }
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<TermViewModel, Term>(Term);
               

                _unitOfWork.TermRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة الترم  بنجاح");
                return RedirectToAction(nameof(Index));
            }
            return View(Term);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Term = _unitOfWork.TermRepository.Find(id);
            if (Term == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Term, TermViewModel>(Term);
            ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");

            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, TermViewModel Term)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (_unitOfWork.TermRepository.All().Any(u => u.Name.ToLower() == Term.Name.ToLower() && u.GradeId == Term.GradeId && u.Id!=id))
                    {
                        ModelState.AddModelError("", "هذا الترم مسجل من قبل .");
                        ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.GetAllGrades(), "Id", "Name");

                        return View(Term);
                    }

                    var old=_unitOfWork.TermRepository.Find(Term.Id);
                    if (old == null)
                    {
                        return NotFound();

                    }
                    old.Update(Term.Name,  Term.GradeId,Term.IsPuplished);
                    _unitOfWork.Commit();
                     

                }
                catch (DbUpdateConcurrencyException)
                {
                    _messenger.Error(
               title: $"تحذير",
                text: "حدث خطأ أثناء تعديل الترم .");

                }

                return RedirectToAction(nameof(Index));
            }
            return View(Term);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(SearchTermModel model)
        {

            try
            {
                var cat = _unitOfWork.TermRepository.Find(model.Id);

                if (cat != null)
                {
                    _unitOfWork.TermRepository.Delete(cat);
                    await _unitOfWork.CommitAsync();


                    var users = _unitOfWork.SubjectRepository.Filter(x => x.TermId == model.Id).Any();
                    if (users)
                        return StatusCode(404, "SubjectUsedTerms");
                    return RedirectToAction("Search", new { Page = model.Page, PageSize = model.PageSize , keyword =model.Keyword,CountryId=model.GradeId});

                }

                else
                {
                    return StatusCode(404, "NotFound");

                }

            }
            catch (Exception)
            {
                _messenger.Error(
                   title: $"تنبية !",
                  text: "هذا التصنيف مستخدم لا يمكن حذفه ");
                return StatusCode(404, "Error");
           

            }
        }



       
    }
}
