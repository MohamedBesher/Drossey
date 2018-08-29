using System;
using System.ComponentModel.DataAnnotations;
using Drossey.Data.Core.Models;

namespace Drossey.Areas.admin.Models
{
    public class TicketsReplayViewModel
    {
        public long Id { get; set; }
        [Display(Name = "الطلب")]
        public long TicketId { get; set; }
        [Display(Name = "الطلب")]
        public Ticket Ticket { get; set; }
        [Display(Name = "الشكوى")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [StringLength(500, ErrorMessage = "الشكوى لا تقل عن {2} حرف ولا تزيد عن {1} حرف ", MinimumLength = 3)]

        public string Message { get; set; }
        [Display(Name = "تاريخ الارسال")]
        public DateTime SendDate { get; set; } = DateTime.Now;
        [Display(Name = "المرسل")]
        public string UserId { get; set; }

        [Display(Name = "المرسل")]
        public ApplicationUser User { get; set; }



    }
}