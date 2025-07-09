

namespace Learnava.DataAccess.Data.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;         
        public DateTime UploadedAt { get; set; }

        public int Position { get; set; } = 1;

        public int CourseId { get; set; }


        public Course? Course { get; set; }
    }

}
