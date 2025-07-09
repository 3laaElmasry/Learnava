

using Learnava.DataAccess.Data;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;

namespace Learnava.DataAccess.Repository
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        public VideoRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
