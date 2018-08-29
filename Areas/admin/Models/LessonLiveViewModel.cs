using Drossey.Data.Core.Dto;
using Drossey.Data.Core.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class LessonLiveViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "اسم الدرس مطلوب")]
        [Display(Name = "اسم الدرس ")]
        [StringLength(100, ErrorMessage = "اسم الدرس لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Title { get; set; }


        [Required(ErrorMessage = "اسم الوحدة الدراسية مطلوب")]
        [Display(Name = "اسم الوحدة الدراسية ")]
        public long BookId { get; set; }


        [Required(ErrorMessage = "اسم الترم الدراسى مطلوب")]
        [Display(Name = "اسم الترم ")]
        public long TermId { get; set; }
        [Required(ErrorMessage = "اسم البلد  مطلوب")]

        [Display(Name = "اسم البلد ")]

        public long CountryId { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        [Required(ErrorMessage = "اسم الصف الدراسى مطلوب")]
        public long GradeId { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        [Required(ErrorMessage = "اسم المادة الدراسية مطلوبة")]
        public long Subject { get; set; }


        [Display(Name = "اسم الدرس  ")]
        [Required(ErrorMessage = "اسم الدرس مطلوب")]
        public long LessonId { get; set; }

        

        public long TeacherId { get; set; }
        [Display(Name = "وقت البداية")]

        [Required(ErrorMessage = "وقت البداية مطلوب")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Remote(action: "VerifyDate", controller: "LiveLessons")]

        public DateTime Start_time { get; set; } = DateTime.Now.AddDays(1);
        [Display(Name = "المنطقة الزمنية")]
        [Required(ErrorMessage = "المنطقة الزمنية مطلوب")]

        public string Time_zone { get; set; }

        [Display(Name = "اسم المدرس ")]
        [Required(ErrorMessage = "اسم المدرس مطلوب")]
        public string Presenter_email { get; set; }

        [Display(Name = "عدد الحضور")]
        [Required(ErrorMessage = "عدد الحضور مطلوب")]

        public int Attendee_limit { get; set; }
        [Display(Name = "وقت الدرس بالدقائق")]
        [Required(ErrorMessage = "وقت الدرس مطلوب")]

        public int Duration { get; set; }
        [Display(Name = "اضافة تسجيل")]
        public bool  Create_recording { get; set; }
        [Display(Name = "اللغة")]
        [Required(ErrorMessage = "اللغة مطلوبة")]
        public string Language_culture_name { get; set; }
        public string ClassId { get;  set; }
    }


    public class QuizViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "اسم الأختبار مطلوب")]
        [Display(Name = "اسم الأختبار ")]
        [StringLength(100, ErrorMessage = "اسم الأختبار لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "اسم البلد  مطلوب")]
        [Display(Name = "اسم البلد ")]
        public long CountryId { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        [Required(ErrorMessage = "اسم البلد  مطلوب")]

        public long GradeId { get; set; }


        [Required(ErrorMessage = "اسم الترم الدراسى مطلوب")]
        [Display(Name = "اسم الترم ")]
        public long TermId { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public long? SubjectId { get; set; } 

        [Display(Name = "اسم الوحدة الدراسية ")]

        public long? BookId { get; set; }

        [Display(Name = "اسم الدرس  ")]
        public long? LessonId { get; set; } 

        [Display(Name = "عددالأسئلة الأختيارية")]

        [Range(1, 30, ErrorMessage = "عدد الاسئله ما بين 1 و 30")]
        [Required(ErrorMessage = " عددالأسئلة الأختيارية مطلوب")]
        public int ChooseCount { get; set; }
        [Display(Name = " عددأسئلة الصح والخطأ")]
        [Range(1, 30, ErrorMessage = "عدد الاسئله ما بين 1 و 30")]
        [Required(ErrorMessage = " عددأسئلة الصح والخطأ مطلوب")]
        public int TrueFalseCount { get; set; }
        [Display(Name = "عددالأسئلة التكميليه ")]
        [Range(1, 30, ErrorMessage = "عدد الاسئله ما بين 1 و 30")]
        [Required(ErrorMessage = "  عددالأسئلة التكميلية مطلوب")]
        public int CompeleteCount { get; set; }
    }
    public class QuestionViewModel
    {
        public long Id { get; set; }

        [Display(Name = "اسم البلد ")]
        [Required(ErrorMessage = "اسم البلد  مطلوب")]
        public long CountryId { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        [Required(ErrorMessage = "اسم الصف الدراسى مطلوب")]

        public long GradeId { get; set; }


        [Required(ErrorMessage = "اسم الترم الدراسى مطلوب")]
        [Display(Name = "اسم الترم ")]
        public long TermId { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        [Required(ErrorMessage = "اسم المادة الدراسية مطلوب")]
        public long SubjectId { get; set; }

        [Display(Name = "اسم الوحدة الدراسية ")]
        [Required(ErrorMessage = "اسم الوحدة الدراسية مطلوب")]

        public long BookId { get; set; }

        [Display(Name = "اسم الدرس  ")]
        [Required(ErrorMessage = "اسم الدرس  مطلوب")]
        public long LessonId { get; set; }


        [Display(Name = " السؤال")]
        [Required(ErrorMessage = " السؤال  مطلوب")]
        public string Body { get; set; }
        [Display(Name = "درجة السؤال")]
        [Required(ErrorMessage = " درجةالسؤال  مطلوبة")]
        [Range(1, 1000, ErrorMessage = "درجة السؤال اكبر من صفر")]
        public long grade { get; set; }
        [Display(Name = "نوع السؤال")]
        [Required(ErrorMessage = "نوع السؤال  مطلوب")]
        public QuestionType Type { get; set; }
       // public long QuizId { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
        public bool IsCorrect { get; set; }
        [Display(Name = "نص الاجابة")]
        public string Answer { get; set; }

        public bool IsAjax { get; set; } = false;
    }


    public class QuestionsViewModel
    {
        public long Id { get; set; }

        [Display(Name = "اسم البلد ")]
        [Required(ErrorMessage = "اسم البلد  مطلوب")]
        public long CountryId { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        [Required(ErrorMessage = "اسم الصف الدراسى مطلوب")]

        public long GradeId { get; set; }


        [Required(ErrorMessage = "اسم الترم الدراسى مطلوب")]
        [Display(Name = "اسم الترم ")]
        public long TermId { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        [Required(ErrorMessage = "اسم المادة الدراسية مطلوب")]
        public long SubjectId { get; set; }

        [Display(Name = "اسم الوحدة الدراسية ")]
        [Required(ErrorMessage = "اسم الوحدة الدراسية مطلوب")]

        public long BookId { get; set; }

        [Display(Name = "اسم الدرس  ")]
        [Required(ErrorMessage = "اسم الدرس  مطلوب")]
        public long LessonId { get; set; }

        [Display(Name = "نص الملف")]
        [Required(ErrorMessage = "نص الملف مطلوب")]

        public string TextData { get; set; }


        public string TextJson { get; set; }
    }

    public class AnswerViewModel
    {
        public long Id { get; set; }
        [Display(Name = "اجابة السؤال ")]
        [Required(ErrorMessage = "اجابة السؤال  مطلوبة")]
        public string Answer { get; set; }

        public bool IsCorrect { get; set; }
        public bool IsPhoto { get; set; }
    }

}
