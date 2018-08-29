using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Drossey.Data.Core;
using Drossey.Data.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Drossey.Admin;
using Drossey.Data.Core.Dto;
using Drossey.Admin.Services;
using Drossey.Data.Core.Enum;

namespace Drossey.Areas.admin.ViewComponents
{
    public class SearchSubjectTeachersViewComponent : ViewComponent
    {
        public IUnitOfWorkAsync _unitOfWork;
        protected readonly IMapper _mapper;

        public SearchSubjectTeachersViewComponent(IUnitOfWorkAsync unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //
        public async Task<IViewComponentResult> InvokeAsync(long subjectId)
        {

            ViewBag.subjectId = subjectId;

            var teachers = _unitOfWork.TeacherSubjectRepository.All().Include(u=>u.Teacher).Where(u=>u.SubjectId==subjectId);
            //
            IQueryable<SubjectTeacherDto> codesList = teachers
                                          .Select(u => new SubjectTeacherDto
                                          {
                                              Id = u.Id,
                                              Name= u.Teacher.Name,
                                              TeacherId = u.TeacherId,
                                              IsMajor = u.IsMajor,
                                              SubjectId=u.SubjectId
                                          }
                                              );

            return View(await codesList.ToListAsync());
           
        
        }
    }
}