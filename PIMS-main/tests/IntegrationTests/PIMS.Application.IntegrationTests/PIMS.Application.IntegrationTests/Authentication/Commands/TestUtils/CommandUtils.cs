using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Application.IntegrationTests.TestUtils.Constants;
using PIMS.Domain.Common.Authentication.Configuration;
using static PIMS.Domain.Common.Authentication.Configuration.Enums;

namespace PIMS.Application.IntegrationTests.Authentication.Commands.TestUtils
{
    public static class CommandUtils
    {
        public static Application.Authentication.Commands.RegisterUsingAD.RegisterCommand CreateCommandADEmptyUserName()
        {
			var adUser = new Domain.Common.Models.ActiveDirectory.ActiveDirectoryUser(string.Empty, string.Empty, string.Empty, string.Empty);
			return new Application.Authentication.Commands.RegisterUsingAD.RegisterCommand(adUser);
        }
		public static Application.Authentication.Commands.RegisterUsingAD.RegisterCommand CreateCommandADEmptyFirstName()
		{
			var adUser = new Domain.Common.Models.ActiveDirectory.ActiveDirectoryUser(Constants.User.UserName, string.Empty, Constants.UserData.MiddleName, Constants.UserData.LastName);
			return new Application.Authentication.Commands.RegisterUsingAD.RegisterCommand(adUser);
		}
		public static Application.Authentication.Commands.RegisterUsingAD.RegisterCommand CreateCommandAD()
		{
			return new Application.Authentication.Commands.RegisterUsingAD.RegisterCommand(new Domain.Common.Models.ActiveDirectory.ActiveDirectoryUser(Constants.User.UserName, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Constants.UserData.Email) { });
		}

		public static Application.Authentication.Commands.RegisterUsingForms.RegisterCommand CreateCommandFormsEmptyUserName()
		{
			return new Application.Authentication.Commands.RegisterUsingForms.RegisterCommand(Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Constants.UserData.Email, string.Empty, Constants.User.Password);
		}
		public static Application.Authentication.Commands.RegisterUsingForms.RegisterCommand CreateCommandFormsEmptyLastName()
		{
			return new Application.Authentication.Commands.RegisterUsingForms.RegisterCommand(Constants.UserData.FirstName, Constants.UserData.MiddleName, string.Empty, Constants.UserData.Email, Constants.User.UserName, Constants.User.Password);
		}
		public static Application.Authentication.Commands.RegisterUsingForms.RegisterCommand CreateCommandForms()
		{
			return new Application.Authentication.Commands.RegisterUsingForms.RegisterCommand(Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Constants.UserData.Email, Constants.User.UserName, Constants.User.Password);
		}

		public static Application.Authentication.Commands.SelectAuthProvider.SelectAuthProviderCommand CreateSelectAuthCommandAD()
		{
			var authProviders = new List<AuthenticationModuleOption>();
			foreach (AuthenticationProviders authProvider in Enum.GetValues(typeof(AuthenticationProviders)))
			{
				authProviders.Add(new AuthenticationModuleOption() { Type = authProvider, Name = authProvider.ToString() });
			}
			return new Application.Authentication.Commands.SelectAuthProvider.SelectAuthProviderCommand(authProviders);
		}
		public static Application.Authentication.Commands.SelectAuthProvider.SelectAuthProviderCommand CreateSelectAuthCommandJWT()
		{
			var authProviders = new List<AuthenticationModuleOption>()
			{
				new AuthenticationModuleOption(){ Type= AuthenticationProviders.JWT, Name="JWT"}
			};
			return new Application.Authentication.Commands.SelectAuthProvider.SelectAuthProviderCommand(new List<AuthenticationModuleOption>(authProviders));
		}
	}
}
