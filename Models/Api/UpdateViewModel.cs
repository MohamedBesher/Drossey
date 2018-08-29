using System.ComponentModel.DataAnnotations;

namespace Drossey.Models.Api.Models
{
    public class UpdateViewModel
    {

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاخير ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "النوع")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        public long CountryId { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الصف")]
        public long GradeId { get; set; }

        public string PhotoUrl { get; set; }
        public string Base64 { get; set; }
        public string Email { get;  set; }
        public string CountryName { get;  set; }
        public string GradeName { get;  set; }
    }
}
