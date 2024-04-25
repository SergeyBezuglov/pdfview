using PIMS.Domain.Common.Models.ActiveDirectory;
using PIMS.Application.IntegrationTests.TestUtils.Constants;

namespace PIMS.Application.IntegrationTests.Authentication.Queries.TestUtils
{
	public static class LoginQueryUtils
	{
		public static ActiveDirectoryUser CreateADUser()
		{
			return new Domain.Common.Models.ActiveDirectory.ActiveDirectoryUser(Constants.User.UserName, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Constants.UserData.Email);
		}
		public static ActiveDirectoryUser CreateAnotherADUser()
		{
			return new Domain.Common.Models.ActiveDirectory.ActiveDirectoryUser(Constants.AnotherUser.UserName, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName, Constants.UserData.Email);
		}
	}
}
