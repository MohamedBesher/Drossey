using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class SettingViewModel
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

        [Display(Name = "المعرف")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public string key { get; set; }

        [Display(Name = "القيمة")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [Range(0, int.MaxValue, ErrorMessage = "ادخل قيمة صحيحة")]
        public long Value { get; set; }


    }
}