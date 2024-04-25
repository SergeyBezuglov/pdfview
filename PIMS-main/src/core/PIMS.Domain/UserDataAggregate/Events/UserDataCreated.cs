using PIMS.Domain.Common.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserDataAggregate.Events
{
    /// <summary>
    /// Пользовательские данные созданы.
    /// </summary>
    public record UserDataCreated( UserData UserData) : IDomainEvent;    
}
