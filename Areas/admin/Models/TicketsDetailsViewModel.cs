using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Drossey.Data.Core.Models;

namespace Drossey.Areas.admin.Models
{
    public class TicketsDetailsViewModel
    {

        [Display(Name = "شكوى")]
        public long TicketId { get; set; }
        public List<TicketReply> Replayes { get; set; }




    }
}
