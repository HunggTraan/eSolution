using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Data.Configurations
{
    public class ShiftTypeConfiguration : IEntityTypeConfiguration<ShiftType>
    {
        public void Configure(EntityTypeBuilder<ShiftType> builder)
        {
            builder.ToTable("ShiftTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.StartIn).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.StartOut).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.EndIn).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.EndOut).IsRequired(true).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
