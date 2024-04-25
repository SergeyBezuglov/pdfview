using PIMS.Domain.Common.Models.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Services.ActiveDirectory
{
    /// <summary>
    /// Интерфейс провайдера пользователя на основе данных из Active Directory.
    /// </summary>
    public interface IActiveDirectoryUserProvider
    {
        /// <summary>
        /// Создает и возвращает объект ActiveDirectoryUser, содержит информацию о пользователе в Active Directory, полученные из объектов identity и claims.
        /// </summary>
        /// <param name="identity">Идентификатор.</param>
        /// <param name="claims">Претензии.</param>
        ActiveDirectoryUser GetADUserFromIndentity(IIdentity identity, IEnumerable<Claim> claims);
    }
}
