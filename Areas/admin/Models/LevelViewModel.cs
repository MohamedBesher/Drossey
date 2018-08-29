using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class LevelViewModel
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

        [Display(Name = "الثمن")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public decimal Price { get; set; }

        [Display(Name = "اسم الخدمة")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public long ServiceId { get; set; }

        [Required(ErrorMessage = "{0} مطلوب")]
        public int Key { get; set; }

    }
}