using Learnava.DataAccess.Data.Entities;
using System;

using System.Linq.Expressions;


namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(Expression<Func<Enrollment, bool>>? enrollmentFilter = null, string? included = null);

        Task<bool> IsUserEnrolledAsync(int courseId, int studentid);

        Task<Enrollment> CreateAsync(Enrollment enrollment);

        Task<bool> DeleteEnrollmentAsync(int enrollmentId);


        Task<Enrollment?> GetEnrollmentAsync(Expression<Func<Enrollment, bool>> enrollmentFilter, string? included = null);

    }
}