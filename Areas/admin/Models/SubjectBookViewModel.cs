namespace Drossey.Areas.admin.Models
{
    public class SubjectBookViewModel
    {
        public long Id { get; set; }
       
        public string Name { get; set; }

        //[Required(ErrorMessage = "ترتيب الوحدة مطلوب")]
        public int Order { get; set; } = 1;

        public long SubjectId { get; set; }






    }

    public class BookLessonViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        //[Required(ErrorMessage = "ترتيب الوحدة مطلوب")]
        public int Order { get; set; } = 1;

        public long BookId { get; set; }






    }


}
