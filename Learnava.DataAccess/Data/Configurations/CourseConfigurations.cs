

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
        InstructorId = "e4d92a7b-15a4-4f68-a0f4-de7c0ea63064"
    },
    new Course()
    {
        Id = -2,
        CreatedAt = new DateTime(2024, 6, 1),
        Title = "Algorithms",
        Description = "Learn Algorithms and apply them in practice",
        InstructorId = "c0241e9f-63de-4005-9508-be5d2c1fb8e7"
    },
    new Course()
    {
        Id = -3,
        CreatedAt = new DateTime(2024, 6, 1),
        Title = "OOP",
        Description = "Master Object-Oriented Programming fundamentals",
        InstructorId = "03b35f2f-b6c6-48f8-8616-1ff545e6a864"
    }
});

        }
    }
}
