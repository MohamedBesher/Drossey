using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models.Api.Models
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاخير ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [EmailAddress]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "النوع")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [StringLength(100, ErrorMessage = "كلمة المرور لا تقل عن 6 احرف .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تاكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور وتاكيد كلمة المرور غير متطابقين .")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = " {0} مطلوب")]
        public long CountryId { get; set; }



        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الصف")]
        public long GradeId { get; set; }
        public string Username { get;  set; }
        public bool IsEmailConfirmed { get;  set; }
      
    }
}
