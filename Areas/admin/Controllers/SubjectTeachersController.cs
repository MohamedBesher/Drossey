using AutoMapper;
using Drossey.Admin.Services;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using MotleyFlash.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class SubjectTeachersController : BaseController
    {
        private readonly IWizIQSender _wizIQSender;
        private readonly ITimeZone _timeZone;

        public SubjectTeachersController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment, IWizIQSender wizIQSender, ITimeZone timeZone)
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
            _wizIQSender = wizIQSender;
            _timeZone = timeZone;


        }

        public IActionResult Index(SearchBookModel model)
        {

            ViewBag.subjectId = model.SubjectId;
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchBookModel model)
        {
            return ViewComponent("SearchSubjectTeachers", new { SubjectId = model.SubjectId });
        }
        [HttpGet]
        public ViewComponentResult Search(long subjectId)
        {
            return ViewComponent("SearchSubjectTeachers", new { SubjectId = subjectId });
        }


        public IActionResult Create(long? subjectId)
        {
            if (subjectId == null)
            {
                return NotFound();
            }

            var subject = _unitOfWork.SubjectRepository.Find(subjectId);
            if (subject == null)
            {
                return NotFound();
            }

            return View(new SubjectTeacherViewModel { SubjectId = subjectId.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubjectTeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (teacher.TeacherId == 0)
                    {
                       // ModelState.AddModelError("", "اختر مدرس من القائمة.");
                        ModelState.AddModelError("", "هذا المدرس ليس موجود.");
                        return View(teacher);
                    }

                    var teachers = _unitOfWork.TeacherSubjectRepository.All()
                        .Where(u => u.SubjectId == teacher.SubjectId)
                        .ToList();
                    if (teachers.Any() && teacher.IsMajor)
                        teachers.ForEach(u => { u.IsMajor = false; });

                    _unitOfWork.TeacherSubjectRepository.Create(new TeacherSubject { SubjectId = teacher.SubjectId, TeacherId = teacher.TeacherId, IsMajor = teacher.IsMajor });
                    _unitOfWork.Commit();
                    _messenger.Success(
                     title: $"تنبية",
                      text: "تم اضافة المدرس  بنجاح");
                    return RedirectToAction("Details", "Subjects", new { id = teacher.SubjectId });
                }
                catch (Exception)
                {
                    _messenger.Error(
                title: $"تحذير",
                 text: "حدث خطأ أثناء اضافة المدرس");
                    return RedirectToAction("Details", "Subjects", new { id = teacher.SubjectId });

                }

            }
            return View(teacher);


        }
        // AutoComplete
        public IActionResult getTeachers(string term, long subjectId)
        {
            var registedTeachers = _unitOfWork.TeacherSubjectRepository.All()
                .Where(u => u.SubjectId == subjectId).Select(u => u.TeacherId);



            var teachers = _unitOfWork.TeacherRepository.All()
                .Where(c => c.Name.Contains(term) && (!registedTeachers.Any() || !registedTeachers.Contains(c.Id)))
                .Select(a => new { label = a.Name, id = a.Id }
               );
            return Json(teachers);
        }

        //  [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {

            try
            {

                var code = _unitOfWork.TeacherSubjectRepository.Find(id);

                if (code != null)
                {
                    var subjectId = code.SubjectId;
                    _unitOfWork.TeacherSubjectRepository.Delete(code);
                    await _unitOfWork.CommitAsync();

                    _messenger.Success(
                  title: $"تنبية !",
                 text: "تم حذف المدرس بنجاح .");
                    return RedirectToAction("Search", new
                    { subjectId = subjectId });
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
                  text: "حدث خطأ أثناء حذف الكارت .");
                return StatusCode(404, "Error");


            }
        }


        [HttpPost]
        public ActionResult SetAsMajor(DeleteTeacherSubjectModel model)
        {

            try
            {
                var sub = _unitOfWork.SubjectRepository.Find(model.SubjectId);

                if (sub != null)
                {
                    var teacher = _unitOfWork.TeacherRepository.Find(model.TeacherId);
                    if (teacher != null)
                    {
                        var teachers = _unitOfWork.TeacherSubjectRepository.All()
                            .Where(u => u.SubjectId == model.SubjectId)
                            .ToList();
                        if (teachers.Any() && model.IsMajor)
                            teachers.ForEach(u => { u.IsMajor = false; });

                        var teachersubject = _unitOfWork.TeacherSubjectRepository.Find(model.Id);
                        teachersubject.IsMajor = model.IsMajor;
                        _unitOfWork.Commit();

                        return RedirectToAction("Search", new { subjectId = model.SubjectId });
                    }
                    return StatusCode(404, "NotFound");

                }

                else
                {
                    return StatusCode(404, "NotFound");

                }

            }
            catch (Exception)
            {

                return StatusCode(404, "Error");


            }
        }

    }
}
