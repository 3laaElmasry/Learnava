

using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using System.Linq.Expressions;

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

        public async Task<Student?> GetStudentAsync(Expression<Func<Student, bool>> studentFilter = null, string? included = null)
        {
            var studentFromDb = await _studentRepository
                .GetAsync(studentFilter,included);

            return studentFromDb;
        }
    }
}
