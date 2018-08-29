using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drossey.Data.Core.Models;

namespace Drossey.Areas.admin.Models
{
    public class SearchModel : Pager
    {
        
    }
    public class SearchGradeModel : Pager
    {
        public long CountryId { get; set; }

    }


    public class SearchTermModel : Pager
    {
        public long GradeId { get; set; }

    }

   

    public class SearchSubjectModel : Pager
    {
        public long CountryId { get; set; }
        public long GradeId { get; set; }

        public long TermId { get; set; }


    }
    public class DeleteTeacherSubjectModel 
   {  
        public long Id { get; set; }
        public long SubjectId { get; set; }
        public bool IsMajor { get; set; }

        public long TeacherId { get; set; }
       
    }


    public class SearchBookModel : SearchSubjectModel
    {
        public long SubjectId { get; set; }
    }
    public class SearchLessonModel : SearchBookModel
    {  
        public long BookId { get; set; }
    }
    public class SearchLiveLessonModel : SearchLessonModel
    {
        public long LessonId { get; set; }
    }

    public class SearchQuizQuestionModel : Pager
    {
        public long QuizId { get; set; }
    }



}
