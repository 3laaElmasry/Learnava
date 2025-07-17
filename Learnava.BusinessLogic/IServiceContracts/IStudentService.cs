

using Learnava.DataAccess.Data.Entities;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface IStudentService
    {
        Task<Student> Create(string userId);

        Task<Student?> GetStudentAsync(Expression<Func<Student, bool>> studentFilter, string? included = null);

    }
}
