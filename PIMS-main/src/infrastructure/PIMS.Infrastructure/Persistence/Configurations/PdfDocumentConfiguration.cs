using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Domain;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    namespace PIMS.Infrastructure.Persistence.Configurations
    {
        public class PdfDocumentConfiguration : IEntityTypeConfiguration<PdfDocument>
        {
            public void Configure(EntityTypeBuilder<PdfDocument> builder)
            {
                ConfigurePDF(builder);
            }
            public void ConfigurePDF(EntityTypeBuilder<PdfDocument> builder)
            {
                builder.ToTable("PdfDocuments");

                builder.HasKey(pd => pd.Id);
                builder.Property(pd => pd.Id).ValueGeneratedOnAdd(); // Автоматическая генерация ID

                builder.Property(pd => pd.Title).HasMaxLength(255);
                builder.Property(pd => pd.Author).HasMaxLength(255);
                builder.Property(pd => pd.Publisher).HasMaxLength(255);
                builder.Property(pd => pd.FilePath).HasMaxLength(255);
                builder.Property(p => p.Year).HasColumnType("int") .IsRequired(false); // Делаем поле необязательным, если это уместно
                builder.Property(pd => pd.Content).HasColumnType("nvarchar(max)"); // Убедитесь, что тип подходит для вашего SQL Server

                // Если вы планируете использовать полнотекстовый поиск, возможно, вам понадобится настройка Full-Text Index на этом поле через миграции
            }
        }
    }
}
