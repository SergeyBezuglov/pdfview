using FluentAssertions;

using PIMS.Application.Authentication.Common;
using PIMS.Domain.Common.Models.ActiveDirectory;
using PIMS.Application.UnitTests.TestUtils.Constants;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.UnitTests.TestUtils.Users.Extensions
{
    /// <summary>
    /// Расширения пользователя.
    /// </summary>
    public static partial class UserExtensions
    {
		/// <summary>
		/// Проверяет данные пользователя, созданного командой регистрации с помощью форм
		/// </summary>
		/// <param name="user"></param>
		/// <param name="command"></param>
		public static void ValidationCreatedForm(this AuthenticationResult user, Application.Authentication.Commands.RegisterUsingForms.RegisterCommand command)
        {
            user.User.Should().NotBeNull();
            user.Token.Should().NotBeNull();
            user.User!.UserName.Should().Be(command.UserName);
            user.User.Password.Should().Be(command.Password);
            user.UserData.Should().NotBeNull();
            user.UserData!.FirstName.Should().Be(command.FirstName);
            user.UserData!.MiddleName.Should().Be(command.MiddleName);
            user.UserData!.LastName.Should().Be(command.LastName);
            user.UserData!.Email.WorkEmail.Should().Be(command.Email);


        }
		public static void ValidationCreatedAD(this AuthenticationResult user, Application.Authentication.Commands.RegisterUsingAD.RegisterCommand command)
		{
			user.User.Should().NotBeNull();
			user.Token.Should().NotBeNull();
			user.User!.UserName.Should().Be(command.User.UserName);
			user.UserData.Should().NotBeNull();
			user.UserData!.FirstName.Should().Be(command.User.FirstName);
			user.UserData!.MiddleName.Should().Be(command.User.MiddleName);
			user.UserData!.LastName.Should().Be(command.User.LastName);
			user.UserData!.Email.WorkEmail.Should().Be(command.User.Email);
		}

		public static void ValidationGetAuthResultAD(this AuthenticationResult user, ActiveDirectoryUser adUser)
		{
			user.User.Should().NotBeNull();
			user.Token.Should().NotBeNull();
			user.User!.UserName.Should().Be(adUser.UserName);
			user.UserData.Should().NotBeNull();
			user.UserData!.FirstName.Should().Be(adUser.FirstName);
			user.UserData!.MiddleName.Should().Be(adUser.MiddleName);
			user.UserData!.LastName.Should().Be(adUser.LastName);
			user.UserData!.Email.WorkEmail.Should().Be(adUser.Email);
			user.UserData!.Phone.CityPhoneNumber.Should().Be(adUser.PhoneNumber);
		}
		public static void ValidationGetAuthResultForms(this AuthenticationResult user)
		{
			user.User.Should().NotBeNull();
			user.Token.Should().NotBeNull();
			user.User!.UserName.Should().Be(Constants.Constants.User.UserName);
			user.UserData.Should().NotBeNull();
			user.UserData!.FirstName.Should().Be(Constants.Constants.UserData.FirstName);
			user.UserData!.MiddleName.Should().Be(Constants.Constants.UserData.MiddleName);
			user.UserData!.LastName.Should().Be(Constants.Constants.UserData.LastName);
			user.UserData!.Email.WorkEmail.Should().Be(Constants.Constants.UserData.Email);
			user.UserData!.Phone.CityPhoneNumber.Should().Be(Constants.Constants.UserData.Phone);
		}
	}
}
