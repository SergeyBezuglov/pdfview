using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.EventLogAggregate;
using PIMS.Domain.EventLogAggregate.ValueObjects;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация журнала событий.
    /// </summary>
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        /// <summary>
        /// Задача (TODO): Добавить сводку
        /// </summary>
        /// <param name="builder">Строитель.</param>
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            ConfigureEventLogTable(builder);            
        }
        /// <summary>
        /// Настраивает таблицу журнала событий.
        /// </summary>
        /// <param name="builder">Строитель.</param>
        private void ConfigureEventLogTable(EntityTypeBuilder<EventLog> builder)
        {
            builder.ToTable("EventLog"); 

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever().HasConversion(id => id.Value,
                value => EventLogId.Create(value));

            builder.Property(m => m.Level).HasMaxLength(128);
        }
    }
}
