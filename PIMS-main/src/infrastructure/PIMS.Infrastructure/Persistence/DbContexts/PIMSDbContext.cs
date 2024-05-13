using Microsoft.EntityFrameworkCore;
using PIMS.Domain;
using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.EventLogAggregate;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;
using PIMS.Infrastructure.Persistence.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.DbContexts
{
    /// <summary>
    /// Контекст базы данных PIMS.
    /// </summary>
    public class PIMSDbContext:DbContext
    {
        /// <summary>
        /// Перехватчик событий домена публикации.
        /// </summary>
        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="PIMSDbContext"/> .
        /// </summary>
        /// <param name="options">Варианты.</param>
        /// <param name="publishDomainEventsInterceptor">Перехватчик событий домена публикации.</param>
        public PIMSDbContext(DbContextOptions<PIMSDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)

         : base(options)

        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
        }
        /// <summary>
        /// Пользователей.
        /// </summary>
        /// <value>Значение набора баз данных (DbSet).</value>
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Данные пользователя.
        /// </summary>
        /// <value>Значение набора баз данных (DbSet).</value>
        public DbSet<UserData> UserData { get; set; } = null!;
        /// <summary>
        /// Журнал событий.
        /// </summary>
        /// <value>Значение набора баз данных (DbSet).</value>
        public DbSet<EventLog> EventLog { get; set; } = null!;

        public DbSet<PdfDocument> PdfDocuments { get; set; }

        /// <summary>
        /// Создание модели.
        /// </summary>
        /// <param name="modelBuilder">Создатель моделей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                Ignore<IList<IDomainEvent>>().
                ApplyConfigurationsFromAssembly(typeof(PIMSDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
        /// <summary>
        /// По настройке.
        /// </summary>
        /// <param name="optionsBuilder">Конструктор опции.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
