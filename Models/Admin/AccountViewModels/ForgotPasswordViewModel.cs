using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="البريد الالكترونى مطلوب" )]
        [EmailAddress(ErrorMessage ="بريد الكترونى صحيح")]
        public string Email { get; set; }
    }
}
