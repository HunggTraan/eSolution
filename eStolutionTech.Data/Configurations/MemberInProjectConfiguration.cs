using eStolutionTech.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStolutionTech.Data.Configurations
{
    public class MemberInProjectConfiguration : IEntityTypeConfiguration<MemberInProject>
    {
        public void Configure(EntityTypeBuilder<MemberInProject> builder)
        {
            builder.ToTable("MemberInProject");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProjectId).IsRequired(true);
            builder.Property(x => x.MemberId).IsRequired(true);
        }
    }
}
