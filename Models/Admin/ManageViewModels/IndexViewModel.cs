using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الاسم الاخير")]
        public string LastName { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        public string Username { get; set; }
        [Display(Name = "النوع")]
        public bool Gender { get; set; }
        public bool IsEmailConfirmed { get; set; }

       
        [Required(ErrorMessage = " {0} مطلوب")]
        [EmailAddress(ErrorMessage = "بريد الكترونى صحيح")]
        [Display(Name = "البريد الالكترونى")]
        public string Email { get; set; }

      
        [Required(ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الهاتف")]
        [Phone(ErrorMessage = "رقم هاتف صحيح")]
        public string PhoneNumber { get; set; }

        public string PhotoUrl { get; set; }
        public string StatusMessage { get; set; }

        [Display(Name = "البلد")]
        [Required(ErrorMessage = " {0} مطلوب")]
        public long CountryId { get; set; }


        [Required(ErrorMessage = " {0} مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = " {0} مطلوب")]
        [Display(Name = "الصف")]
        public long GradeId { get; set; }

        public SelectList Grades { get; set; }


        [Display(Name = "البلد")]

        public string CountryName { get; set; }

        [Display(Name = "Country")]
        public SelectList CountryList { get; set; }
        public decimal Balance { get; set; }

    }
}
