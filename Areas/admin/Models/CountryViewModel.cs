using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class CountryViewModel
    {
        public long Id { get; set; }
        [Display(Name = "الاسم بالانجليزية")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }

    }

    public class ContactUsViewModel
    {
        public long Id { get; set; }
        [Display(Name = "الاسم بالانجليزية")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

    }


    public class SellerViewModel
    {
        public long Id { get; set; }
        [Display(Name = "الاسم ")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }


        [Display(Name = "البريد الالكترونى")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [EmailAddress(ErrorMessage = "بريد الكترونى صحيح")]
        public string Email { get; set; }


        [Display(Name = "الهاتف")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [Phone(ErrorMessage ="رقم هاتف صحيح")]
        public string PhoneNumber { get; set; }


        [Display(Name = "حالة النشر")]
        public bool IsActive { get; set; } = true;



    }
}
