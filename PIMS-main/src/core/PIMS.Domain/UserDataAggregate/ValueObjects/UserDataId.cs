using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.Common.Models;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.OrganizationAggregate.ValueObjects;
using PIMS.Domain.PostAggregate.ValueObjects;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserDataAggregate.ValueObjects
{
    /// <summary>
    /// Идентификатор данных пользователя.
    /// </summary>
    public class UserDataId : AggregateRootId<Guid>, IOrdinaryValueObject<Guid>
    {
        /// <summary>
        /// Значение.
        /// </summary>
        /// <value>Руководство.</value>
        public override Guid Value { get; protected set; }
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="UserDataId"/> .
        /// </summary>
        private UserDataId() : base()
        { }
        /// <summary>
        ///Предотвращает создание экземпляра класса по умолчанию <see cref="UserDataId"/> .
        /// </summary>
        /// <param name="value">Значение.</param>
        private UserDataId(Guid value) : base(value)
        {

        }
        /// <summary>
        /// Создает уникальное.
        /// </summary>
        /// <returns>Возвращает идентификатор пользовательских данных (UserDataId).</returns>
        public static UserDataId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        /// <summary>
        /// Создает <see cref="UserDataId"/>.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Возвращает идентификатор пользовательских данных (UserDataId).</returns>
        public static UserDataId Create(Guid value)
        {
            return new(value);
        }
    }
}
