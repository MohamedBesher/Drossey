using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class CoiffeurServiceLevelViewModel
    {
        [Required(ErrorMessage = "{0} مطلوب")]
        public long Id { get; set; }

        public string UserId { get; set; }
        [Display(Name = "مستوى الكوافير")]
        [Required(ErrorMessage = "{0} مطلوب")]

        public long? LevelId { get; set; }
        [Display(Name = "السعر المقترح من الكوافير")]
        [Required(ErrorMessage = "{0} مطلوب")]
        public decimal Price { get; set; }

        [Display(Name = "الخدمة المقدمة")]
        [Required(ErrorMessage = "{0} مطلوب")]

        public long ServiceId { get; set; }
    }
}