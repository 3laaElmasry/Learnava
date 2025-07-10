

using Learnava.DataAccess.Data.Entities;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.IServiceContracts
{
    public interface IVideoService
    {
        Task<IEnumerable<Video>> GetVideosAsync(Expression<Func<Video,bool>>? filter = null, string? included = null);
        Task<Video> GetVideoByIdAsync(int videoId);

        Task<Video> UpdateVideoAsync(int videoId, Video newVideo);

        Task<bool> DeleteVideoAsync(int videoId);

        Task<Video> GetVideoAsync(Expression<Func<Video, bool>>? filter = null, string? included = null);
    }
}
