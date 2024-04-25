using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.Common.Models;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PIMS.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация пользователя.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Задача (TODO): Добавить сводку
        /// </summary>
        /// <param name="builder">Строитель.</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUserTable(builder);
            //ConfigurationUserDataTable(builder);
        }


        //private void ConfigurationUserDataTable(EntityTypeBuilder<User> builder)
        //{
        //    builder.OwnsMany(m => m.UserData, ud =>
        //    {

        //        ud.ToTable("UserData");
        //        ud.WithOwner().HasForeignKey("UserId");

        //        ud.HasKey(nameof(UserData.Id), "UserId");
        //        ud.Property(m => m.Id).
        //        HasColumnName("UserDataId").
        //        ValueGeneratedNever().
        //        HasConversion(id => id.Value,
        //        value => UserDataId.Create(value));

        //        ud.Property(m => m.FirstName).HasMaxLength(255);
        //        ud.Property(m => m.MiddleName).HasMaxLength(255);
        //        ud.Property(m => m.LastName).HasMaxLength(255);

        //        ud.OwnsOne(m => m.Email);
        //        ud.OwnsOne(m => m.Phone); 
        //        //ud.Ignore(m => m.DivisionId);
        //        //ud.Ignore(m => m.PostId);
        //        ud.OwnsOne(m => m.EmployeeNumber);

        //    });
        //    builder.Metadata.FindNavigation(nameof(User.UserData))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        //}
        /// <summary>
        /// Configures the user table.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void ConfigureUserTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");


            builder.HasKey(m => m.Id);
            builder.Property(m=>m.Id).ValueGeneratedNever().HasConversion(id=>id.Value,
                value=>UserId.Create(value));
        
          
            builder.Property(m => m.UserName).HasMaxLength(255);
            builder.Property(m => m.Password).HasMaxLength(50);


            builder.HasMany<UserData>("UserData").WithOne("User").HasForeignKey(m => m.UserId);            
        }
    }
}
