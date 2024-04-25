using PIMS.Application.Common.Interfaces.Services.ActiveDirectory;
using PIMS.Domain.Common.Models.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.ActiveDirectory
{
    /// <summary>
    /// Поставщик пользователей активного каталога.
    /// </summary>
    public class ActiveDirectoryUserProvider : IActiveDirectoryUserProvider
    {
        /// <summary>
        /// Пользовать AD из идентификатора.
        /// </summary>
        /// <param name="identity">Идентификатор.</param>
        /// <param name="claims">Претензии.</param>
        /// <returns>Возвращение значения пользователя Active Directory (ActiveDirectoryUser).</returns>
        public ActiveDirectoryUser GetADUserFromIndentity(IIdentity identity, IEnumerable<Claim> claims)
        {            
            return new ActiveDirectoryUser(identity.Name ?? "","Иван","Иванович","Иванов","","","");
        }
    }
}
