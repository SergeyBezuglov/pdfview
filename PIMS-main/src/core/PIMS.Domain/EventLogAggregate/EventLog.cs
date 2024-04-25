using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.EventLogAggregate.ValueObjects;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.EventLogAggregate
{
    /// <summary>
    /// Журнал событий.
    /// </summary>
    public class EventLog : AggregateRootInt<EventLogId>
    {
        /// <summary>
        /// Конструктор вызывает конструктор базового класса с переданным значением EventLogId.CreateUnique(0) с помощью ключевого слова base(EventLogId.CreateUnique(0)).
        /// </summary>
        private EventLog() : base(EventLogId.CreateUnique(0))
        {
        }
        /// <summary>
        /// Свойство сообщение может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public string? Message { get; private set; } = String.Empty;
        /// <summary>
        /// Свойство шаблон сообщения может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public string? MessageTemplate { get; private set; } = String.Empty;
        /// <summary>
        /// Свойство получение уровня может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public string? Level { get; private set; } = String.Empty;
        /// <summary>
        /// Свойство отметка времени может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public DateTime TimeStamp { get; private set; } = DateTime.UtcNow;
        /// <summary>
        /// Свойство исключение может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public string? Exception { get; private set; } = String.Empty;
        /// <summary>
        /// Свойство характеристики может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public string Properties { get; private set; } = String.Empty;
        /// <summary>
        /// Свойство информация о пользователе может быть установлено при создании экземпляра объекта и имеет закрытый доступ к set.
        /// </summary>
        public string? UserInfo { get; private set; } = String.Empty;
    }
}
