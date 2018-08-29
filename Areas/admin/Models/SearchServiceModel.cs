using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class SearchServiceModel : Pager
    {
        [Display(Name = "التصنيف")]
        public long CategoryId { get; set; }

        //[Display(Name = "التفعيل")]
        //public bool IsActive { get; set; } = true;
        public long ServiceId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }


    
}