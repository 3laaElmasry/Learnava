using Learnava.DataAccess.Data.Entities;
using System;

using System.Linq.Expressions;


namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(Expression<Func<Enrollment, bool>>? enrollmentFilter = null, string? included = null);

        Task<Enrollment?> GetEnrollmentByIdAsync(int? id, string? included = null);

        Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);

        Task<Enrollment> UpdateEnrollmentAsync(Enrollment enrollment);

        Task<bool> DeleteEnrollmentAsync(int enrollmentId);
    }
}
