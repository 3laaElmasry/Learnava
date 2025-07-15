using Learnava.DataAccess.Data.Entities;

namespace Learnava.web.VMModels
{
    public class VideoIndexVM
    {
        public Course? course {  get; set; }
        public IEnumerable<Video> Videos { get; set; } = Enumerable.Empty<Video>();
    }
}
