using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserDataAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Authentication;

/// <summary>
///  Генератор Jwt токена пользователя и пользовательских данных.
/// </summary>
public interface IJwtTokenGenerator
{
    string GenerateToken(User user, UserData userData);
}
