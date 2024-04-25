using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using PIMS.Application.UnitTests.TestUtils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIMS.Domain.UserDataAggregate;
using PIMS.Domain.UserDataAggregate.Enums;

namespace PIMS.Application.UnitTests.Authentication.Queries.TestUtils
{
	public static class LoginQueryUtils
	{
		public static User PrepareUserForLoginQuery(DateTime updatedDate, out UserData userData)
		{
			var userId = UserId.CreateUnique();
			var userDataId = UserDataId.CreateUnique();
			return User.Create(userId, Constants.User.UserName, "",
				userData = CreateUserData(userId, userDataId,updatedDate), updatedDate);
		}
		public static UserData CreateUserData(UserId userId, UserDataId userDataId, DateTime updatedDate)
		{
			return UserData.Create(userId, userDataId, updatedDate, Constants.UserData.FirstName, Constants.UserData.MiddleName, Constants.UserData.LastName,
			 Email.CreateEmail(Constants.UserData.Email ?? "", ""), Phone.CreatePhone(Constants.UserData.Phone ?? "", "", ""), WorkingHours.WorkingHoursEnum.Normal);
		}
	}
}
