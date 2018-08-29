namespace Drossey.Models
{
    public class LibrarySearchModel
    {

        public long CountryId { get; set; } = 0;
        public long GradeId { get; set; } = 0;
        public long TermId { get; set; } = 0;
        public long SubjectId { get; set; } = 0;
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } =8;



    }
}
