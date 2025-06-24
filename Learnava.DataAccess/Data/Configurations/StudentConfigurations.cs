

using Learnava.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnava.DataAccess.Data.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ApplicationUser)
                .WithOne()
                .HasForeignKey<Student>(s => s.ApplicationUserId)
                .IsRequired();


        }
    }
}
