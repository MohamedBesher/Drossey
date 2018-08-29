using System;
using System.Collections.Generic;
using AutoMapper;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotleyFlash;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using MotleyFlash.Extensions;
using Drossey.Data.Core.Dto;
using Drossey.Admin.Services;

namespace Drossey.Areas.admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]


    public class TeachersController : BaseController
    {
        private readonly IWizIQSender _wizIQSender;
        private readonly ITimeZone _timeZone;

        public TeachersController(IUnitOfWorkAsync unitOfWork, SignInManager<ApplicationUser> signInMgr, UserManager<ApplicationUser> userMgr, IPasswordHasher<ApplicationUser> hasher, IConfiguration config, IMapper mapper, ILogger<BaseController> logger, IMessenger messenger, IHostingEnvironment hostingEnvironment, IWizIQSender wizIQSender,ITimeZone timeZone) 
            : base(unitOfWork, signInMgr, userMgr, hasher, config, mapper, logger, messenger, hostingEnvironment)
        {
            _wizIQSender = wizIQSender;
            _timeZone = timeZone;


        }

        public IActionResult Index(SearchBookModel model)
        {
            ViewBag.Keyword = model.Keyword;
            ViewBag.page = model.Page;
            ViewBag.pageSize = model.PageSize;
            ViewBag.termId = model.TermId;
            ViewBag.countryId = model.CountryId;
            ViewBag.gradeId = model.GradeId;
            ViewBag.subjectId = model.SubjectId;
            ViewBag.countries = new SelectList(_unitOfWork.CountryRepository.Filter(u=>u.IsPuplished), "Id", "Name");
            return View();

        }


        [HttpPost]
        public ViewComponentResult Search(SearchBookModel model)
        {
            return ViewComponent("SearchTeachers", new { pageSize = model.PageSize, page = model.Page, keyword = model.Keyword, countryId = model.CountryId, gradeId = model.GradeId, TermId = model.TermId,SubjectId=model.SubjectId });
        }
        [HttpGet]
        public ViewComponentResult Search(int pageSize, int page, string keyword,long countryId ,long gradeId,long termId,long subjectId)
        {
            return ViewComponent("SearchTeachers", new { pageSize = pageSize, page = page, keyword = keyword, countryId = countryId, gradeId = gradeId, TermId = termId , SubjectId = subjectId });
        }


        public IActionResult Create()
            {
            loadcountries();
            loadtimeZones();
            return View("Create", new TeacherViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                if (TryValidateModel(teacher))
                {
                    loadtimeZones();
                    return View(teacher);
                }
                teacher.Phone_number = $"{teacher.PhoneCountryCode} {teacher.Phone_number}";
                teacher.Mobile_number = $"{teacher.MobileCountryCode} {teacher.Mobile_number}";

                if (CheckIfTeacherNameExists(teacher.Name))
                {
                    ModelState.AddModelError("", "هذا الاسم موجود من قبل");
                    loadtimeZones();
                    loadPhoto(teacher.PhotoUrl);
                    return View(teacher);
                }


                var teacherId = _wizIQSender.AddTeacher(teacher.Name, teacher.Email, teacher.Password,
                    teacher.Phone_number, teacher.Mobile_number, teacher.Time_zone, teacher.About_the_teacher, Convert.ToInt32(teacher.Can_schedule_class).ToString(), Convert.ToInt32(teacher.Is_active).ToString(), "");

                if (teacherId.Item2 == "ok")
                    teacher.teacher_id = teacherId.Item1;
                else
                {
                    ModelState.AddModelError("", teacherId.Item1);
                    loadtimeZones();
                    loadPhoto(teacher.PhotoUrl);
                    return View(teacher);
                }
                var model = _mapper.Map<TeacherViewModel, Teacher>(teacher);
                _unitOfWork.TeacherRepository.Create(model);
                await _unitOfWork.CommitAsync();
                _messenger.Success(
                 title: $"تنبية",
                  text: "تم اضافة المدرس  بنجاح");
                return RedirectToAction(nameof(Index));
            }
           
            loadPhoto(teacher.PhotoUrl);
            loadtimeZones();

            return View(teacher);
        }

        private bool CheckIfTeacherNameExists(string name,long id=0)
        {
            return _unitOfWork.TeacherRepository.All()
                .Any(u => u.Name.Trim().Equals(name.Trim())  
                  &&  (id==0 || u.Id!=id));

        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }          
            var teacher = _unitOfWork.TeacherRepository.Filter(u=>u.Id==id)
                //.Include(y => y.Subject)
                //.ThenInclude(y=>y.Term)
                //.ThenInclude(u=>u.Grade)
                //.ThenInclude(u=>u.Country)
                .FirstOrDefault();
            if (teacher == null)
            {
                return NotFound();
            }

           
            
            var model = new TeacherViewModel()
            {
                Id = teacher.Id,
                Name=teacher.Name,
                //SubjectId=teacher.SubjectId,
                //TermId = teacher.Subject.TermId,
                //CountryId = teacher.Subject.Term.Grade.CountryId,
                //GradeId = teacher.Subject.Term.GradeId,
                teacher_id=teacher.teacher_id,
                Mobile_number=teacher.Mobile_number,
                Phone_number=teacher.Phone_number,
                Email=teacher.Email,
                Password=teacher.Password,
                Time_zone=teacher.Time_zone,
                About_the_teacher=teacher.About_the_teacher,
                Can_schedule_class=teacher.Can_schedule_class,
                Is_active=teacher.Is_active,
                PhotoUrl =teacher.PhotoUrl,
            };
            model.MobileCountryCode = teacher.Mobile_number.Substring(0, 3);
            model.PhoneCountryCode = teacher.Phone_number.Substring(0, 3);

            model.Mobile_number = teacher.Mobile_number.Substring(3, teacher.Phone_number.Length - 3);
            model.Phone_number = teacher.Phone_number.Substring(3, teacher.Phone_number.Length-3);

            if (CheckIfTeacherNameExists(teacher.Name,model.Id))
            {
                ModelState.AddModelError("", "هذا الاسم موجود من قبل");
                loadtimeZones();
                loadPhoto(teacher.PhotoUrl);
                return View(teacher);
            }
            loadtimeZones();
            loadPhoto(model.PhotoUrl);
            

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, TeacherViewModel teacher)
        {

            if (ModelState.IsValid)
            {
                if (TryValidateModel(teacher))
                {
                    loadtimeZones();
                    return View(teacher);
                }
                try
                {
                    teacher.Phone_number = $"{teacher.PhoneCountryCode} {teacher.Phone_number}";
                    teacher.Mobile_number = $"{teacher.MobileCountryCode} {teacher.Mobile_number}";




                    var teacherId = _wizIQSender.EditTeacher(teacher.teacher_id, teacher.Name, teacher.Email, teacher.Password,
                  teacher.Phone_number, teacher.Mobile_number, teacher.Time_zone, teacher.About_the_teacher, Convert.ToInt32(teacher.Can_schedule_class).ToString(), Convert.ToInt32(teacher.Is_active).ToString(), "");

                    if (teacherId.Item2 == "ok")
                        teacher.teacher_id = teacherId.Item1;
                    else
                    {
                        ModelState.AddModelError("", teacherId.Item1);
                        loadPhoto(teacher.PhotoUrl);
                        return View(teacher);
                    }

                    var old = _unitOfWork.TeacherRepository.Find(teacher.Id);
                   
                    if (old == null)
                    {
                        return NotFound();
                    }


                    var photoUrl = old.PhotoUrl;

                    old.Update(teacher.Name,teacher.Email,teacher.Password,teacher.Phone_number,
                               teacher.Mobile_number,teacher.Time_zone,teacher.About_the_teacher,
                               teacher.Can_schedule_class,teacher.Is_active,teacher.PhotoUrl);

                    if (_unitOfWork.Commit() && old.PhotoUrl!=teacher.PhotoUrl)
                        DeleteImage(photoUrl);


                    _messenger.Success(
               title: $"تنبية",
                text: "تم تعديل المدرس  بنجاح");

                return RedirectToAction(nameof(Index));


                }
                catch (Exception)
                {
                    _messenger.Error(
               title: $"تحذير",
                text: "حدث خطأ أثناء تعديل المدرس .");

                }

            }
            loadPhoto(teacher.PhotoUrl);
            return View(teacher);
           
               
                
        }

        [HttpPost]
        public async Task<ActionResult> Delete(SearchBookModel model)
        {

            try
            {
                var teacher = _unitOfWork.TeacherRepository.Find(model.Id);

                if (teacher != null)
                {

                    var teacherId = _wizIQSender.EditTeacher(teacher.teacher_id, teacher.Name, teacher.Email, teacher.Password,
                 teacher.Phone_number, teacher.Mobile_number, teacher.Time_zone, teacher.About_the_teacher, Convert.ToInt32(teacher.Can_schedule_class).ToString(), "0", "");

                    if (teacherId.Item2 == "ok")
                        teacher.teacher_id = teacherId.Item1;

                    else
                    {
                        return StatusCode(404, teacherId.Item1);
                    }

                    teacher.Is_active = false;

                    await _unitOfWork.CommitAsync();

                    return RedirectToAction("Search", new { Page = model.Page, PageSize = model.PageSize, keyword = model.Keyword,  countryId=model.CountryId,  gradeId=model.GradeId,  termId=model.TermId,subjectId=model.SubjectId });

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
                  text: "حدث خطأ اثناء ايقاف المدرس");
                return StatusCode(404, "Error");


            }
        }

        [HttpGet]
        [Route("{id}/{isActive}")]
        public async Task<ActionResult> ApproveUser(long? id, bool? isActive = false)
        {
            try
            {
                var teacher = _unitOfWork.TeacherRepository.Find(id);

                if (teacher != null)
                {

                    var Active = Convert.ToInt32(isActive).ToString();
                    var teacherId = _wizIQSender.EditTeacher(teacher.teacher_id, teacher.Name, teacher.Email, teacher.Password,
                     teacher.Phone_number, teacher.Mobile_number, teacher.Time_zone, teacher.About_the_teacher, Convert.ToInt32(teacher.Can_schedule_class).ToString(), Active, "");

                    if (teacherId.Item2 == "ok")
                        teacher.teacher_id = teacherId.Item1;

                    else
                    {
                        return StatusCode(404, teacherId.Item1);
                    }

                    teacher.Is_active = isActive.Value;
                    await _unitOfWork.CommitAsync();
                   return Json("OK");
                }
                return Json("NotFound");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json("Error");
            }
        }



        //public ActionResult Details(long id)
        //{

        //    var book = _unitOfWork.BookRepository.Filter(u => u.Id == id)
        //      .Include(y => y.Subject)
        //      .ThenInclude(y => y.Term)
        //      .ThenInclude(u => u.Grade)
        //      .ThenInclude(u => u.Country).FirstOrDefault();
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new BookDto()
        //    {
        //        Id = book.Id,
        //        Name = book.Name,
        //        SubjectName = book.Subject.Name,
        //        TermName = book.Subject.Term.Name,
        //        CountryName = book.Subject.Term.Grade.Country.Name,
        //        GradeName = book.Subject.Term.Grade.Name,
        //        IsPublished = book.IsPuplished,
        //        Price = book.Price,
        //        DiscountPercentage = book.DiscountPercentage,
        //        PhotoUrl = book.PhotoUrl,
        //        CreationDate=book.CreationDate
        //    };

        //    return View(model);
        //}

        [HttpPost]
        public IActionResult LoadDrp([FromBody] ItemDto model)
        {
            List<ItemDto> result = new List<ItemDto>();
                
            switch (model.Name)
            {
                case "CountryId":
                    result = _unitOfWork.GradeRepository.Filter(u => u.CountryId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;
                case "GradeId":
                    result = _unitOfWork.TermRepository.Filter(u => u.GradeId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;

                case "TermId":
                    result = _unitOfWork.SubjectRepository.Filter(u => u.TermId == model.Id).Select(u => new ItemDto()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                    break;
                default:
                    break;
            };
            return Json(result);
        }


        private bool TryValidateModel(TeacherViewModel teacher)
        {
            #region validate Model

           
            if (!CheckIfFileExists(teacher.PhotoUrl))
            {
                ModelState.AddModelError("", "يجب اضافة الصورة");             
                return true;

            }
            return false;
            #endregion
        }

        private void loadsubjects(long termId)
        {
            ViewData["subjects"] = new SelectList(_unitOfWork.SubjectRepository.Filter(u => u.IsPuplished && u.TermId == termId), "Id", "Name");
        }
        private void loadtimeZones()
        {
            ViewData["timeZones"] = new SelectList(_timeZone.GetTimesZones(), "Value", "Name");

        }

        private void loadTerms(long gradeId)
        {
            ViewData["terms"] = new SelectList(_unitOfWork.TermRepository.Filter(u => u.IsPuplished && u.GradeId == gradeId), "Id", "Name");
        }

        private void loadGrades(long countryId)
        {
            ViewData["grades"] = new SelectList(_unitOfWork.GradeRepository.Filter(u => u.IsPuplished && u.CountryId == countryId), "Id", "Name");

        }

        private void loadcountries()
        {
            ViewData["countries"] = new SelectList(_unitOfWork.CountryRepository.Filter(u => u.IsPuplished), "Id", "Name");

        }



        private void loadPhoto(string photoUrl)
        {
            if (!string.IsNullOrEmpty(photoUrl))
                ViewBag.PhotoUrl = GetFullPath(photoUrl);
            else
                ViewBag.PhotoUrl = "";
        }
    }
}
