using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Data.Configurations
{
    public class ShiftSettingConfiguration : IEntityTypeConfiguration<ShiftSetting>
    {
        public void Configure(EntityTypeBuilder<ShiftSetting> builder)
        {
            builder.ToTable("ShiftSettings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.TimeIn).IsRequired(true);
            builder.Property(x => x.TimeOut).IsRequired(true);
            builder.Property(x => x.ExceedTimeIn).IsRequired(true);
            builder.Property(x => x.ExceedTimeOut).IsRequired(true);
        }
    }
}
