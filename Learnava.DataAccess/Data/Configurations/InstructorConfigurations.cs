using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Learnava.DataAccess.Data.Configurations
{
    public class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.ApplicationUser)
                .WithOne()
                .HasForeignKey<Instructor>(i => i.ApplicationUserId)
                .IsRequired();

        }
    }
}
