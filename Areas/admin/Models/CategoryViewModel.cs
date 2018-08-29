using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Areas.admin.Models
{



    public class LessonViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "اسم الدرس مطلوب")]
        [Display(Name = "اسم الدرس ")]
        [StringLength(100, ErrorMessage = "اسم الدرس لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        [Required(ErrorMessage = "اسم المادة الدراسية مطلوبة")]
        public long SubjectId { get; set; }


        [Required(ErrorMessage = "اسم الترم الدراسى مطلوب")]
        [Display(Name = "اسم الترم ")]
        public long TermId { get; set; }
        [Required(ErrorMessage = "اسم البلد  مطلوب")]

        [Display(Name = "اسم البلد ")]

        public long CountryId { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        [Required(ErrorMessage = "اسم الصف الدراسى مطلوب")]
        public long GradeId { get; set; }


        [Display(Name = "اسم الوحدة الدراسية  ")]
        [Required(ErrorMessage = "اسم الوحدة الدراسية مطلوبة")]
        public long ModuleId { get; set; }



        //public string PhotoUrl { get; set; }


        //[Display(Name = "رابط الدرس")]
        //[Required(ErrorMessage = "رابط الدرس مطلوب")]

        //public string Link { get; set; }
        [Display(Name = "يوجد شرح")]

        public bool HasLink { get; set; } = true;


        public string MeetingId { get; set; }

        [Display(Name = "رابط البث المباشر")]
        [DataType(DataType.Url,ErrorMessage = "اضف رابط صحيح")]

        public string MeetingLiveLink { get; set; }
        [Display(Name = "رابط الحلقة المسجلة")]
        [DataType(DataType.Url, ErrorMessage = "اضف رابط صحيح")]


        public string MeetingRecoredLink { get; set; }

        //[Display(Name = "رابط الاختبار")]
        //[Required(ErrorMessage = "رابط الاختبار مطلوب")]

        //public string QuizLink { get; set; }
       [Display(Name = "يوجد اختبار")]


        public bool HasQuizLink { get; set; } = true;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Display(Name = "حالة النشر  ")]
        public bool IsPuplished { get; set; }

    }


}
