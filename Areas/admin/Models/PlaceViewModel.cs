using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class PlaceViewModel
    {
        public long Id { get; set; }
        [Display(Name = "الاسم بالانجليزية")]
        [Required(ErrorMessage = "اسم المكان مطلوب")]
        [StringLength(200, ErrorMessage = "اسم المكان لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string Name { get; set; }


        [Display(Name = "الاسم بالعربية")]
        [Required(ErrorMessage = "اسم المكان مطلوب")]
        [StringLength(200, ErrorMessage ="اسم المكان لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]

        public string NameAr { get; set; }
    }
}