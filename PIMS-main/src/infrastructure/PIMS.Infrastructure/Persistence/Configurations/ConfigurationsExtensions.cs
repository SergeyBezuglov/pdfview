using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Расширения конфигурации.
    /// </summary>
    public static class ConfigurationsExtensions
    {
        /// <summary>
        /// Внешний ключ.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Prefix">Префикс.</param>
        /// <returns>Строка.</returns>
        public static string CreateForeignKey<T>(string Prefix="Id")
            where T:class
        {
            return $"{typeof(T).Name}{Prefix}";
        }
        //public static void OwnsOneAggregateToEntityKey<T, Y, TId, YId>(OwnedNavigationBuilder<T, Y> builder)
        //  where T : AggregateRoot<TId>
        //  where Y : Entity<YId>
        //  where TId : OrdinaryValueObject
        //  where YId : TemporalValueObject
        //{
            
        //    builder.OwnsOne(x => x.Id, ownedBuilder => {
        //        ownedBuilder.Property(m => m.Value).HasColumnName("Id_Value");
        //        ownedBuilder.Property(m => m.SnapShotDate).HasColumnName("Id_SnapShotDate");
        //    });
        //    builder.Property(typeof(Guid), "Id_Value").ValueGeneratedNever();
        //    builder.Property(typeof(DateTime), "Id_SnapShotDate").ValueGeneratedNever();
        //    builder.HasKey("Id_Value", "Id_SnapShotDate");
        //}

        //public static void OwnsOneAggregateToValueObjectKey<T, TId>(EntityTypeBuilder<T> builder)
        // where T : AggregateRoot<TId>
        // where TId : OrdinaryValueObject

        //{
        //    builder.OwnsOne(x => x.Id, ownedBuilder => {
        //        ownedBuilder.Property(x => x.Value).HasColumnName("Id_Value");
        //    });
        //    builder.Property(typeof(Guid), "Id_Value").HasColumnName("Id_Value"); 
        //    builder.HasKey("Id_Value");
        //}


      
    }
}
