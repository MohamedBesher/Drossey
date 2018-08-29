using System.ComponentModel.DataAnnotations;

namespace Drossey.Models
{
    public class CodeAddViewModel
    {
        [Display(Name = "كود الكارت")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [StringLength(15, ErrorMessage = "كود الكارت لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 15)]

        public string Code { get; set; }
    }

    public class ContactUsModel
    {

        [Display(Name = "نص الرسالة")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [StringLength(250, ErrorMessage = "نص الرسالة لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]
        public string Message { get; set; }

    }
    public class ContactUsViewModel : ContactUsModel
    {
        [Display(Name = "رقم الهاتف")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [Phone(ErrorMessage = "رقم هاتف صحيح")]

        public string Phone { get; set; }

        [Display(Name = "البريد الالكترونى")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [EmailAddress(ErrorMessage = "بريد الكترونى صحيح")]
        public string Email { get; set; }

        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public string Name { get; set; }
    }

}

  

