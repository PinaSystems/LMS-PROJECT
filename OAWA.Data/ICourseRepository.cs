using System.Threading.Tasks;
using OAWA.Data.Dtos;
using OAWA.Data.Helpers;

namespace OAWA.Data
{
    public interface ICourseRepository
    {
        Task<PagedList<CourseForViewDto>> GetCourses(CourseParams courseParams);
        Task<CourseForViewDto> GetCourse(long courseId);
    }
}