
using Learnava.DataAccess.Data.Entities;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface IInstructorService
    {
        Task<Instructor> Create(string userID);
    }
}
