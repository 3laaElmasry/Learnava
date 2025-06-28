

using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;

namespace Learnava.BusinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> Create(string userId)
        {
            var newStudent = new Student()
            {
                ApplicationUserId = userId,
            };
            await _studentRepository.AddAsync(newStudent);
            await _studentRepository.Save();
            return newStudent;
        }
    }
}
