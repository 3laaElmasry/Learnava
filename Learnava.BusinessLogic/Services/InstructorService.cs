

using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using Microsoft.AspNetCore.Components.Forms;

namespace Learnava.BusinessLogic.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<Instructor> Create(string userID)
        {
            var newInstructor = new Instructor()
            {
                ApplicationUserId = userID,

            };

            await _instructorRepository.AddAsync(newInstructor);
            await _instructorRepository.Save();
            return newInstructor;
        }
    }
}
