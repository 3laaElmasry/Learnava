

using Learnava.DataAccess.Data;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;

namespace Learnava.DataAccess.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
