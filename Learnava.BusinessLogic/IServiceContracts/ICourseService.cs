

using Learnava.DataAccess.Data.Entities;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();

        Task<Course?> GetCourseByIdAsync(int? id,string? included = null);

        Task<Course> AddCourseAsync(Course course);

        Task<Course> UpdateCourseAsync(Course course);
    }
}
