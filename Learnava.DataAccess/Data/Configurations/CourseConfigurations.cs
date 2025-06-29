

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

            builder.HasData(new List<Course>()
{
    new Course()
    {
        Id = -1,
        CreatedAt = new DateTime(2024, 6, 1),
        Title = "Data Structures",
        Description = "Learn Data Structures and use them to solve real world problems",
        InstructorId = "4e291191-3c5a-47d1-848f-360b881a65a1"
    },
    new Course()
    {
        Id = -2,
        CreatedAt = new DateTime(2024, 6, 1),
        Title = "Algorithms",
        Description = "Learn Algorithms and apply them in practice",
        InstructorId = "72f17693-c0e5-4d86-a115-1f3b0a66b37e"
    },
    new Course()
    {
        Id = -3,
        CreatedAt = new DateTime(2024, 6, 1),
        Title = "OOP",
        Description = "Master Object-Oriented Programming fundamentals",
        InstructorId = "eda80011-f522-48b7-9ede-e338e726f705"
    }
});

        }
    }
}
