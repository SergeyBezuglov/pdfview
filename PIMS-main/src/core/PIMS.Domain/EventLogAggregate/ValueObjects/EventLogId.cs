using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.EventLogAggregate.ValueObjects
{
    /// <summary>
    /// Идентификатор журнала событий.
    /// </summary>
    public class EventLogId : AggregateRootId<int>, IOrdinaryValueObject<int>
    {
        /// <summary>
        /// Свойство значение может быть установлено при создании экземпляра объекта и имеет ограниченный доступ к set.
        /// </summary>
        public override int Value { get; protected set; }
        /// <summary>
        /// Конструктор вызывает конструктор базового класса.
        /// </summary>
        private EventLogId() : base()
        { }
        /// <summary>
        /// Конструктор принимает аргумент int value и вызывает конструктор базового класса с переданным значением value с помощью ключевого слова base(value).
        /// </summary>
        private EventLogId(int value) : base(value)
        {

        }
        /// <summary>
        /// Создает уникальное значение и возвращает.
        /// </summary>
        public static EventLogId CreateUnique(int value)
        {
            return new(value);
        }
        /// <summary>
        /// Создает значение и возвращает.
        /// </summary>
        public static EventLogId Create(int value)
        {
            return new(value);
        }

    }
}
