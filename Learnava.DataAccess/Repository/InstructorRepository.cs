

using Learnava.DataAccess.Data;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Learnava.DataAccess.Repository
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
