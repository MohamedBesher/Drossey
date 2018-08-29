using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class TermViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "اسم الترم مطلوب")]
        [Display(Name = "اسم الترم ")]
        [StringLength(100, ErrorMessage = "اسم الترم لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }
        [Required(ErrorMessage = "اسم الصف مطلوب")]
        [Display(Name = "اسم الصف ")]


        public long GradeId { get; set; }


        [Display(Name = "حالة الترم  ")]


        public bool IsPuplished { get; set; }

    }


    public class SubjectTeacherViewModel
    {
        [Required(ErrorMessage = "اسم المدرس مطلوب")]
        public long TeacherId { get; set; }

        public long SubjectId { get; set; }

        

        [Required(ErrorMessage = "اسم المدرس مطلوب")]
        [Display(Name = "اسم المدرس ")]
        [StringLength(100, ErrorMessage = "اسم المدرس لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }


        [Display(Name = "مدرس رئيسى")]
        public bool IsMajor { get; set; }



    }

}
