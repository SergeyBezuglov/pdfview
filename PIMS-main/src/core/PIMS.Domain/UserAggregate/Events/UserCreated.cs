using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.UserDataAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserAggregate.Events
{
    /// <summary>
    /// Пользователь создал.
    /// </summary>
    public record UserCreated(User User,UserData UserData):IDomainEvent;
}
