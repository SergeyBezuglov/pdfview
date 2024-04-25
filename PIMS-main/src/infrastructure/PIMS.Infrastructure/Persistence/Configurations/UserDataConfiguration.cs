using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация пользовательских данных.
    /// </summary>
    public class UserDataConfiguration : IEntityTypeConfiguration<UserData>
    {
        /// <summary>
        /// Задача (TODO): Добавить сводку
        /// </summary>
        /// <param name="builder">Строитель.</param>
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.ToTable("UserData");


            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever().HasConversion(id => id.Value,
                value => UserDataId.Create(value));

            builder.Property(m => m.FirstName).HasMaxLength(255);
            builder.Property(m => m.MiddleName).HasMaxLength(255);
            builder.Property(m => m.LastName).HasMaxLength(255);

            builder.OwnsOne(m => m.Email);
            builder.OwnsOne(m => m.Phone);
            //ud.Ignore(m => m.DivisionId);
            //ud.Ignore(m => m.PostId);
            builder.OwnsOne(m => m.EmployeeNumber);

            
            builder.Property(m => m.UserId).HasConversion(id => id.Value,
             value => UserId.Create(value));

        }
    }
}
