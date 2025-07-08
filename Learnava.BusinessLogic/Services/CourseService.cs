

using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course?> GetCourseByIdAsync(int? id, string? included = null)
        {
            var courseFromDb = await _courseRepository.GetAsync(c => c.Id == id, includeProperties: included);
            return courseFromDb;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(Expression<Func<Course, bool>>? courseFilter = null, string? included = null)
        {
            var coursedFromDb = await _courseRepository.GetAllAsync(filter: courseFilter,includeProperties: included);
            return coursedFromDb;
        }

        async Task<Course> ICourseService.AddCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course);
            await _courseRepository.Save();
            return course;
        }

        async Task<Course> ICourseService.UpdateCourseAsync(Course course)
        {
            _courseRepository.Update(course);
            await _courseRepository.Save();
            return course;
        }
    }
}