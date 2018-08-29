using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{


    public class ServiceViewModel
    {
        public long Id { get; set; }

        [Display(Name = "الاسم بالانجليزية")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [StringLength(200, ErrorMessage = "اسم المستوى لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }

        [Display(Name = "الاسم بالعربية")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [StringLength(200, ErrorMessage = "اسم المستوى لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string NameAr { get; set; }
        [Display(Name = "الصورة")]
        [Required(ErrorMessage = "{0} مطلوب")]

        public string PhotoUrl { get; set; }

        [Display(Name = " التفاصيل بالانجليزية")]
        [Required(ErrorMessage = "{0} مطلوب")]

        [StringLength(500, ErrorMessage = "تفاصيل المستوى لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Desp { get; set; }
        [Display(Name = "التفاصيل بالعربية")]
        [Required(ErrorMessage = "{0} مطلوب")]

        [StringLength(500, ErrorMessage = "تفاصيل المستوى لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string DespAr { get; set; }
        [Display(Name = "التصنيف")]
        [Required(ErrorMessage = "{0} مطلوب")]

        public long CategoryId { get; set; }
        [Display(Name = "مفعل")]
        public bool IsActive { get; set; }
        public List<LevelViewModel> Levels { get; set; }
        public int Count { get; set; } = 3;

        public bool? IsAjax { get; set; }
    }
}