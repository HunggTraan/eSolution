using eSolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eSolutionTech.Data.Configurations
{
    public class TimeOffRequestConfiguration : IEntityTypeConfiguration<TimeOffRequest>
    {
        public void Configure(EntityTypeBuilder<TimeOffRequest> builder)
        {
            builder.ToTable("TimeOffRequests");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.TimeOffType).IsRequired(true);
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.FromDate).IsRequired(true);
            builder.Property(x => x.ToDate).IsRequired(true);
            builder.HasOne(x => x.User).WithMany(x => x.TimeOffRequests).HasForeignKey(x => x.UserId);
        }
    }
}
