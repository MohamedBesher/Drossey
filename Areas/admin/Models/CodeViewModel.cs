using Drossey.Data.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class CodeViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "رصيد الكارت")]
        [Range(1.0, 100000, ErrorMessage = "رصيد الكارت بين {1} وبين {2}")]

        public int Amount { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "عدد الكروت ")]

        [Range(1.0, 1000, ErrorMessage = "عدد الكروت بين {1} وبين {2}")]

        public int Count { get; set; }

        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "اسم الموزع")]

        public long SellerId { get; set; }


        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "ثمن الكارت")]
        [Range(1.0, 10000, ErrorMessage = "ثمن الكارت بين {1} وبين {2}")]

        public decimal Price { get; set; }


        [Display(Name = "حالة الكروت")]


        public CodeStatus Status { get; set; }

    }


}
