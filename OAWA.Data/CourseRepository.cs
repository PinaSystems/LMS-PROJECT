using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OAWA.Data.Dtos;
using OAWA.Data.Helpers;

namespace OAWA.Data
{
    public class CourseRepository : DataRepository, ICourseRepository
    {
        private readonly IMapper _mapper;
        public CourseRepository(DataContext context, IMapper mapper):base(context)
        {
            _mapper= mapper;   
        }
        public async Task<PagedList<CourseForViewDto>> GetCourses(CourseParams courseParams)
        {
            var courses= await _context.Courses.ToListAsync();
            var CourseForViewDto= new List<CourseForViewDto>();
            _mapper.Map(courses,CourseForViewDto);
            return await PagedList<CourseForViewDto>.CreateAsyncWithDtos(CourseForViewDto.AsQueryable(), courseParams.Page, courseParams.Per_Page);
        }

        public async Task<CourseForViewDto> GetCourse(long courseId)
        {
            var courses= await _context.Courses.FirstOrDefaultAsync(item => item.Id.Equals(courseId));
            var CourseForViewDto= new CourseForViewDto();
            _mapper.Map(courses,CourseForViewDto);
            return CourseForViewDto;
        }
    }
}