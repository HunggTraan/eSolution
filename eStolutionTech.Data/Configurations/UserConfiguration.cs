using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.DoB).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Code).HasDefaultValue("00000");
            builder.Property(x => x.DepartmentId).HasDefaultValue("1");
            builder.Property(x => x.JobTitleId).HasDefaultValue("1");
        }
    }
}
