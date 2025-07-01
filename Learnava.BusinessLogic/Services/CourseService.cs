

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

        async Task<Course?> ICourseService.GetCourseByIdAsync(int? id)
        {
            var courseFromDb = await _courseRepository.GetAsync(c => c.Id == id, includeProperties: "Instructor");
            return courseFromDb;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            var coursedFromDb = await _courseRepository.GetAllAsync(includeProperties: "Instructor");
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