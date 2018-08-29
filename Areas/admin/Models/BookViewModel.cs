using System;
using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class BookViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "اسم الوحدة مطلوب")]
        [Display(Name = "اسم الوحدة ")]
        [StringLength(100, ErrorMessage = "اسم الوحدة لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }

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

        public long SubjectId { get; set; }

        //public string PhotoUrl { get; set; }


        public int Order { get; set; } = 0;

        public DateTime CreationDate { get; set; } =DateTime.Now;

        [Display(Name = "حالة المادة  ")]


        public bool IsPuplished { get; set; }

    }


}
