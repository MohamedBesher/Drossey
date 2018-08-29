using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = " {0} مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور ")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [StringLength(100, ErrorMessage = "كلمة المرورلا تقل عن {2} ولا تزيد عن {1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور الجديدة وتأكيد كلمة المرور غير متطابقين .")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
