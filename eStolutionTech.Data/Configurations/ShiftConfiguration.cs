using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Data.Configurations
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shifts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProjectId).IsRequired(true);
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Date).IsRequired(true);
            builder.Property(x => x.WorkingHours).IsRequired(true);
            builder.Property(x => x.Comment).IsRequired(true);
            builder.Property(x => x.StartIn).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.StartOut).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.EndIn).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.EndOut).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
