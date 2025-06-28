

using Learnava.DataAccess.Data.Entities;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface IStudentService
    {
        Task<Student> Create(string userId);
    }
}
