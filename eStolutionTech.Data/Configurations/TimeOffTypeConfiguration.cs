using eStolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStolutionTech.Data.Configurations
{
    public class TimeOffTypeConfiguration : IEntityTypeConfiguration<TimeOffType>
    {
        public void Configure(EntityTypeBuilder<TimeOffType> builder)
        {
            builder.ToTable("TimeOffTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.RequestUnit).IsRequired(true);
            builder.Property(x => x.EndDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.StartDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Unpaid).HasDefaultValue(true);
        }
    }
}
