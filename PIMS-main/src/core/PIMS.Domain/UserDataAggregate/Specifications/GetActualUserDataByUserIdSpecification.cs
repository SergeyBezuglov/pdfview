using PIMS.Domain.Common.Models.Specifications;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserDataAggregate.Specifications
{
    /// <summary>
    /// Получение фактических данных пользователя по спецификации идентификатора пользователя.
    /// </summary>
    public class GetActualUserDataByUserIdSpecification : BaseSpecification<UserData>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetActualUserDataByUserIdSpecification"/> .
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public GetActualUserDataByUserIdSpecification(UserId userId) : base(user => user.UserId == userId) { }
    }
}
