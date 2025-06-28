

using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;

namespace Learnava.BusinessLogic.Services
{
    public class InstructorService : IInstructorService
    {
        public async Task<Instructor> Create(string userID)
        {
            return  new Instructor();
        }
    }
}
