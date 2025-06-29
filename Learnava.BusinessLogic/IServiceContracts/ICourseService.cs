

using Learnava.DataAccess.Data.Entities;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
    }
}
