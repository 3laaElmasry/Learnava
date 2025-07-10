
using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.Services
{
    public class VideoService : IVideoService
    {
        public Task<bool> DeleteVideoAsync(int videoId)
        {
            throw new NotImplementedException();
        }

        public Task<Video> GetVideoAsync(Expression<Func<Video, bool>>? filter = null, string? included = null)
        {
            throw new NotImplementedException();
        }

        public Task<Video> GetVideoByIdAsync(int videoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Video>> GetVideosAsync(Expression<Func<Video, bool>>? filter = null, string? included = null)
        {
            throw new NotImplementedException();
        }

        public Task<Video> UpdateVideoAsync(int videoId, Video newVideo)
        {
            throw new NotImplementedException();
        }
    }
}
