
using Learnava.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnava.DataAccess.Data.Configurations
{
    public class VideoConfigurations : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(v => v.Url)
                .IsRequired();

            builder.Property(v => v.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(v => v.Course)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
