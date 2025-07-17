
using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.Save();
            return enrollment;

        }

        public Task<bool> DeleteEnrollmentAsync(int enrollmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(Expression<Func<Enrollment, bool>>? enrollmentFilter = null, string? included = null)
        {
            var enrollmentsFromDb = await _enrollmentRepository.GetAllAsync(enrollmentFilter, included);
            return enrollmentsFromDb;
        }

        public async Task<bool> IsUserEnrolledAsync(int courseId, int studentid)
        {
           var enroll = await _enrollmentRepository
                .GetAsync(e => e.CourseId == courseId &&  e.StudentId == studentid);

            return enroll is null ? false : true;
        }
    }
}