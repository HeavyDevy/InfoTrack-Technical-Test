using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScraperWeb.Domain.Entities;

namespace Persistance.EFMaps
{   
    public class SearchesMap : IEntityTypeConfiguration<Search>
    {
        public void Configure(EntityTypeBuilder<Search> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(2083)
                .IsUnicode(false);

            entity.Property(e => e.Keywords)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);


        }


    }
}
