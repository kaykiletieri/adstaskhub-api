﻿using adstaskhub_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace adstaskhub_api.Data.Mappings
{
    public class PeriodMapping : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
           builder.ToTable("periods");

           builder.HasKey(x => x.Id);
           builder.Property(x => x.Number).IsRequired().HasColumnName("period_number");
        }
    }
}
