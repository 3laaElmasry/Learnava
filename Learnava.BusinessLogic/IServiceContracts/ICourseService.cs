

using Learnava.DataAccess.Data.Entities;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(Expression<Func<Course, bool>>? courseFilter = null, string ? included = null);

        Task<Course?> GetCourseByIdAsync(int? id,string? included = null);

        Task<Course> AddCourseAsync(Course course);

        Task<Course> UpdateCourseAsync(Course course);

        Task<bool> DeleteCourseAsync(int courseId);
    }
}
