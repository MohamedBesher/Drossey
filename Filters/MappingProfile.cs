using AutoMapper;
using Drossey.Areas.admin.Models;
using Drossey.Data.Core.Dto;
using Drossey.Data.Core.Models;
using Drossey.Models;

namespace Drossey.Admin.Filters
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           

            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, CountryViewModel>();
            CreateMap<Grade, GradeViewModel>();
            CreateMap<GradeViewModel, Grade>();
            CreateMap<TermViewModel, Term>();
            CreateMap<Term, TermViewModel>();
            CreateMap<Subject, SubjectViewModel>();
            CreateMap<SubjectViewModel, Subject>();
            CreateMap<BookViewModel, Book>();
            CreateMap<Book, BookViewModel>();
            CreateMap<LessonViewModel, Lesson>();
            CreateMap<Lesson, LessonViewModel>();
            CreateMap<Lesson, MyLessonDto>();
            CreateMap<Teacher, TeacherViewModel>();
            CreateMap<TeacherViewModel, Teacher>();
            CreateMap<LessonLiveViewModel, LiveLesson>();
            CreateMap<LiveLesson, LessonLiveViewModel>();
            CreateMap<ContactUs, Models.ContactUsViewModel>();
            CreateMap<Models.ContactUsViewModel, ContactUs>();
            CreateMap<Seller, SellerViewModel>();
            CreateMap<SellerViewModel, Seller>();
            CreateMap<QuizViewModel, Quiz>();
            CreateMap<Quiz, QuizViewModel>();
            CreateMap<QuestionViewModel, Drossey.Data.Core.Models.Question>();
            CreateMap<Drossey.Data.Core.Models.Question, QuestionViewModel>();
            CreateMap<AnswerViewModel, Answers>();
            CreateMap<Answers, AnswerViewModel>();


            //CreateMap<TransactionViewModel, Transaction>();
            //CreateMap<CoiffeurServiceLevelViewModel, CoiffeurServiceLevel>();
            //CreateMap<CoiffeurServiceLevel, CoiffeurServiceLevelViewModel>();

        }
    }
}
