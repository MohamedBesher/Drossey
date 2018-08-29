using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Drossey.Data.Core.Enum;
using Drossey.Data.Core.Models;

namespace Drossey.Areas.admin.Models
{
    public class TransactionViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب")]
        [Display(Name = "المدفوع لة")]
        public string PayeeId { get; set; }


        [Display(Name = "الدافع")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public string PayerId { get; set; }



        [Display(Name = "رقم الطلب")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public long? RequestId { get; set; }




        [Display(Name = "التاريخ")]
        public DateTime CreateDate { get; set; } 



        [Display(Name = "القيمة")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public decimal Value { get; set; }



        [Display(Name = "نسبة ساعة الذروة")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public decimal? RushHours { get; set; }



        [Display(Name = "نسبة النظام")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public decimal? SystemPercentage { get; set; }


        [Display(Name = "طريقة الدفع")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public PaymentMethod PaymentMethod { get; set; }


    }
}
