using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Models
{
    public class SearchMyBookModel
    {
        public long SubjectId { get; set; }
        public long CountryId { get; set; }
        public long GradeId { get; set; }
        public long TermId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Keyword { get; set; } = string.Empty;
        public long Id { get; set; }
        public string UserId { get; set; }
    }
}
