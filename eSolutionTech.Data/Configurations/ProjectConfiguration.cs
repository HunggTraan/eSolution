using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.StartDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.EndDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.StartIn).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.StartOut).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.EndIn).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.EndOut).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
