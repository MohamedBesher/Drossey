using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = " اسم المستخدم مطلوب ")]
        [EmailAddress(ErrorMessage ="بريد الكترونى غير صحيح")]
        [Display(Name = "اسم المستخدم")]

        public string Email { get; set; }

        [Required(ErrorMessage = " كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]

        public string Password { get; set; }

        [Display(Name = "تذكرنى")]
        public bool RememberMe { get; set; }
    }

    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = " اسم المستخدم مطلوب ")]
        [Display(Name = "اسم المستخدم")]

        public string Email { get; set; }

        [Required(ErrorMessage = " كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]

        public string Password { get; set; }

        [Display(Name = "تذكرنى")]
        public bool RememberMe { get; set; }
    }
}
