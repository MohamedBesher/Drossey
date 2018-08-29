using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class GradeViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "اسم الصف مطلوب")]
        [Display(Name = "اسم الصف ")]
        [StringLength(100, ErrorMessage = "اسم الصف لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }
        [Required(ErrorMessage = "اسم البلد مطلوب")]
        [Display(Name = "اسم البلد ")]


        public long CountryId { get; set; }


        [Display(Name = "حالة النشر  ")]


        public bool IsPuplished { get; set; }

    }


}
