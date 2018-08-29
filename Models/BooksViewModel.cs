using Drossey.Data.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models
{
    public class BooksViewModel
    {
        public long Id { get; set; }
        [Display(Name = "اسم الوحدة الدراسية ")]
        public string Name { get; set; }

        [Display(Name = "اسم الترم ")]
        public string TermName { get; set; }

        [Display(Name = "اسم البلد ")]
        public string CountryName { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        public string GradeName { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        public string SubjectName { get; set; }
        public Subject Subject { get; set; }
        public string PhotoUrl { get; set; }

        [Display(Name = "ثمن الكتاب")]
        public decimal Price { get; set; }

        [Display(Name = "نسبة الخصم")]
        public decimal DiscountPercentage { get; set; } = 0.0m;


        [Display(Name = "حالة المادة  ")]
        public bool IsPuplished { get; set; }

    }

    public class SubjectsViewModel
    {
        public long Id { get; set; }
        [Display(Name = "اسم المادة الدراسية ")]
        public string Name { get; set; }

        [Display(Name = "اسم الترم ")]
        public string TermName { get; set; }

        [Display(Name = "اسم البلد ")]
        public string CountryName { get; set; }

        [Display(Name = "اسم الصف الدراسى  ")]
        public string GradeName { get; set; }


        [Display(Name = "اسم المادة الدراسية  ")]
        public string SubjectName { get; set; }
        public Subject Subject { get; set; }
        public string PhotoUrl { get; set; }

        [Display(Name = "الثمن")]
        public decimal Price { get; set; }

        [Display(Name = "نسبة الخصم")]
        public decimal DiscountPercentage { get; set; } = 0.0m;


        [Display(Name = "حالة المادة  ")]
        public bool IsPuplished { get; set; }

    }


}
