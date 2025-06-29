

using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Learnava.BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            var coursedFromDb = await _courseRepository.GetAllAsync(includeProperties: "Instructor");
            return coursedFromDb;
        }
    }
}