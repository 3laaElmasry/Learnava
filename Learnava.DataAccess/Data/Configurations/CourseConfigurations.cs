

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
               .HasColumnType("nvarchar(max)")
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
