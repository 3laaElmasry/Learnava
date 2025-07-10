
using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using System.Linq.Expressions;

namespace Learnava.BusinessLogic.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<bool> DeleteVideoAsync(int videoId)
        {
            var videoFromDb = await _videoRepository.GetAsync(v => v.Id == videoId);

            if (videoFromDb == null)
            {
                return false;
            }
            _videoRepository.Remove(videoFromDb);
            await _videoRepository.Save();
            return true;
        }

        public async Task<Video?> GetVideoByIdAsync(int videoId)
        {
            var videoFromDb = await _videoRepository.GetAsync(v => v.Id == videoId);
            if(videoFromDb == null)
            {
                return null;
            }
            return videoFromDb;
        }

        public async Task<IEnumerable<Video>> GetVideosAsync(Expression<Func<Video, bool>>? filter = null, string? included = null)
        {
            var videosFromDb = await _videoRepository.GetAllAsync(filter, included);

            return videosFromDb;
        }

        public async Task<Video?> UpdateVideoAsync(int videoId, Video newVideo)
        {
            var videoFromDb = await _videoRepository.GetAsync(v => v.Id == videoId);
            if( videoFromDb == null)
            {
                return null;
            }
            videoFromDb.Title = newVideo.Title;
            videoFromDb.Url = newVideo.Url;
            videoFromDb.Position = newVideo.Position;
            
            _videoRepository.Update(newVideo);
            await _videoRepository.Save();
            return videoFromDb;
        }
    }
}
