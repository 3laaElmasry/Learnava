

using Learnava.DataAccess.Data;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;

namespace Learnava.DataAccess.Repository
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
