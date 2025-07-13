

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

        public Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEnrollmentAsync(int enrollmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Enrollment?> GetEnrollmentByIdAsync(int? id, string? included = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(Expression<Func<Enrollment, bool>>? enrollmentFilter = null, string? included = null)
        {
            var enrollmentsFromDb = await _enrollmentRepository.GetAllAsync(enrollmentFilter, included);
            return enrollmentsFromDb;
        }

        public Task<Enrollment> UpdateEnrollmentAsync(Enrollment enrollment)
        {
            throw new NotImplementedException();
        }
    }
}
