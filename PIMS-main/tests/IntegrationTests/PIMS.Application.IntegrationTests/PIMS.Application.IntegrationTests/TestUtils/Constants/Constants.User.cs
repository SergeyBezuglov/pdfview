using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.IntegrationTests.TestUtils.Constants
{
    public static partial class Constants
    {
		public static class User
		{
			public const string UserName = "petrov";
			public const string Password = "qwerty_123";
			public static readonly UserId UserId = UserId.Create(new Guid("5F9DE7E4-44CB-4ED0-A11F-0E7975E9A4F3"));
		}
		public static class AnotherUser
		{
			public const string UserName = "ivanov";
			public const string Password = "qwerty_123";
			public static readonly UserId UserId = UserId.Create(new Guid("5F9DE7E4-44CB-4ED0-A11F-0E7975E9A4F3"));
		}
		public static class UserData
		{
			public static readonly UserDataId UserId = UserDataId.Create(new Guid("784A1C85-9294-4BC7-BCFB-D1F7A8188CAD"));
			public const string FirstName = "Петр";
			public const string MiddleName = "Петрович";
			public const string LastName = "Петров";
			public const string Email = "petrov43453453245@mail.ru";
			public const string Phone = "123123456";
		}
		public static class ActiveDirectoryUser
		{
			public const string UserName = "petrov";
			public const string FirstName = "Петр";
			public const string MiddleName = "Петрович";
			public const string LastName = "Петров";
			public const string Email = "petrov43453453245@mail.ru";
			public const string Phone = "89008877766";
			public const string OrganizationName = "UmbrellaCorp";
		}
	}
}
