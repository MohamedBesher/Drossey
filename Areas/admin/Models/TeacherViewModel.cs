using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class TeacherViewModel
    {
        public long Id { get; set; }
        //WizIQ Teacher Id
        public string teacher_id { get; set; }
        

        [Display(Name = "كود الهاتف")]
        [Required(ErrorMessage = " {0} مطلوب")]

        [RegularExpression("^(\\+(1 ?(2(4[2,6]|6[4,8]|84)|34(0|5)|4(41|73)|6(49|64|7[0,1]|84)|7(58|67|8[4,7])|8(09|29|6[8,9]|76)|939)?|2(0|1[1-4,6,8]|2[0-9]|3[0-9]|4[0-9]|5[0-8]|6[0-9]|7|9[0,1,7-9])|3([0-4]|5[0-9]|6|7[0-9]|8[0-7,9]|9)|4(0|1|2[0,1,3]|[3-9])|5(0[0-9]|[1-8]|9[0-9])|6([0-6]|7[0,2-9]|8[0-3,5-9]|9[0-2])|7|8([1,2,4]|5[0,2,3,5,6]|6|8[0,6])|9([0-5]|6[0-8]|7[0-7]|8|9([2-6]|8))))",ErrorMessage ="كود البلد غير صحيح")]
        public string PhoneCountryCode { get; set; }

        [Display(Name="كود الجوال")]
        [Required(ErrorMessage =" {0} مطلوب")]
        [RegularExpression("^(\\+(1 ?(2(4[2,6]|6[4,8]|84)|34(0|5)|4(41|73)|6(49|64|7[0,1]|84)|7(58|67|8[4,7])|8(09|29|6[8,9]|76)|939)?|2(0|1[1-4,6,8]|2[0-9]|3[0-9]|4[0-9]|5[0-8]|6[0-9]|7|9[0,1,7-9])|3([0-4]|5[0-9]|6|7[0-9]|8[0-7,9]|9)|4(0|1|2[0,1,3]|[3-9])|5(0[0-9]|[1-8]|9[0-9])|6([0-6]|7[0,2-9]|8[0-3,5-9]|9[0-2])|7|8([1,2,4]|5[0,2,3,5,6]|6|8[0,6])|9([0-5]|6[0-8]|7[0-7]|8|9([2-6]|8))))", ErrorMessage = "كود الجوال غير صحيح")]
        public string MobileCountryCode { get; set; }

        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "اسم المدرس مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 3)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="بريد الكترونى صحيح")]
        [Display(Name = "بريد الكترونى")]
        //[Remote(action: "VerifyEmail", controller: "Teachers",areaName:"admin")]
        public string Email { get; set; }

        [Display(Name = "كلمة المرور")]
        [Required(ErrorMessage = "كلمة المرور مطلوب")]
        [StringLength(50, ErrorMessage = "كلمة المرور لا تقل عن {2} حرف ولا يزيد عن {1} حرف ", MinimumLength = 6)]
       // [RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$", ErrorMessage = "كلمة المرور يجب ان تحتوى حروف وارقام .")]

        public string Password { get; set; }


        [Display(Name = "الهاتف")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "رقم هاتف صحيح")]
        [Required(ErrorMessage = "الهاتف مطلوب")]

        public string Phone_number { get; set; }

        [Display(Name = "الجوال")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="رقم جوال صحيح")]
        [Required(ErrorMessage = "الجوال مطلوب")]
        public string Mobile_number { get; set; }

        [Display(Name = "المنطقة الزمنية")]

        public string Time_zone { get; set; }

        [Display(Name = "عن المدرس")]

        public string About_the_teacher { get; set; }
        [Display(Name = "يمكن الغاء الدرس")]

        public bool Can_schedule_class { get; set; } = false;

        [Display(Name = "الحالة")]

        public bool Is_active { get; set; } = true;

        public string PhotoUrl { get; set; }
    }


}
