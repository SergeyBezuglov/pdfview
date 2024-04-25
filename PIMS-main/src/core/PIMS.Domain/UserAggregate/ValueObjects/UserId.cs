using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.Common.Models;
using PIMS.Domain.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserAggregate.ValueObjects
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public class UserId : AggregateRootId<Guid>, IOrdinaryValueObject<Guid>
    {
        /// <summary>
        /// Свойство значение имеет ограниченный доступ к set.
        /// </summary>
        public override Guid Value { get; protected set; }
        /// <summary>
        /// Конструктор вызывает конструктор базового класса.
        /// </summary>
        private UserId() : base()
        { }
        /// <summary>
        /// Конструктор принимает аргумент Guid value и вызывает конструктор базового класса с переданным значением value с помощью ключевого слова base(value).
        /// </summary>
        private UserId(Guid value) : base(value)
        {

        }
        /// <summary>
        /// Создает уникальный Guid.
        /// </summary>
        public static UserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }
        /// <summary>
        /// Создает уникальное значение.
        /// </summary>
        public static UserId Create(Guid value)
        {
            return new(value);
        }

    }
}
