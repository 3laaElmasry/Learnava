

using Learnava.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnava.DataAccess.Data.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Description)
               .HasMaxLength(200)
               .IsRequired();

            builder.Property(c => c.CreatedAt)
               .IsRequired();


            builder.HasOne(c => c.Instructor)
                .WithMany()
                .HasForeignKey(c => c.InstructorId)
                .IsRequired();

          
        }
    }
}
