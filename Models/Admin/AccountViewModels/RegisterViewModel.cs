using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage =" {0} مطلوب")]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاخير")]
        public string LastName { get; set; }
        [Required(ErrorMessage = " {0} مطلوب")]
        [EmailAddress(ErrorMessage ="بريد الكترونى صحيح")]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الهاتف")]
        [Phone(ErrorMessage ="رقم هاتف صحيح")]

        public string PhoneNumber { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "النوع")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [StringLength(100, ErrorMessage = "  لا تقل عن 6 حرف ولا تزيد عن 100 حرف .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "كلمات المرور هذه غير متطابقة.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name ="البلد")]
        public long CountryId { get; set; }


        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الصف")]
        public long GradeId { get; set; }

        [Display(Name = "Country")]
        public SelectList CountryList { get; set; }
        public SelectList Grades { get;  set; }
    }
}
