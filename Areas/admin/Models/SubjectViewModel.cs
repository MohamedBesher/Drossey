using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class SubjectViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "اسم المادة مطلوب")]
        [Display(Name = "اسم المادة ")]
        [StringLength(100, ErrorMessage = "اسم المادة لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }

        [Required(ErrorMessage = "اسم الترم الدراسى مطلوب")]
        [Display(Name = "اسم الترم ")]





        public long TermId { get; set; }

        [Required(ErrorMessage = "الوصف مطلوب")]
        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Required(ErrorMessage = "اسم البلد الدراسى مطلوب")]

        [Display(Name = "اسم البلد ")]

        public long CountryId { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        [Required(ErrorMessage = "اسم الصف الدراسى مطلوب")]

        public long GradeId { get; set; }


        [Display(Name = "حالة النشر  ")]


        public bool IsPuplished { get; set; }

        public string PhotoUrl { get; set; }


        
        [Required(ErrorMessage = "ثمن المادة الدراسية مطلوبة")]
        [Display(Name = "ثمن المادة الدراسية")]
        [Range(1.0, 100000, ErrorMessage = "ثمن المادة الدراسية بين {1} وبين {2}")]

        public decimal Price { get; set; }

        [Display(Name = "نسبة الخصم")]
        public decimal DiscountPercentage { get; set; } = 0.0m;


    }


}
