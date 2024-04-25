using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.UserAggregate.Events;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserAggregate
{

    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : AggregateRootGuid<UserId>
    {

        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="User"/> .
        /// </summary>
        private User() : base(UserId.CreateUnique())
        {



        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        /// <value>Строка.</value>
        public string UserName { get; private set; } = "";
        /// <summary>
        /// Пароль.
        /// </summary>
        /// <value>Строка.</value>
        public string Password { get; private set; } = "";

        /// <summary>
        /// Дата создания.
        /// </summary>
        /// <value>Значение даты и время (DateTime).</value>
        public DateTime CreatedDate { get; private set; }
        /// <summary>
        /// Обновленная дата.
        /// </summary>
        /// <value>Значение даты и время (DateTime)? .</value>
        public DateTime? UpdatedDate { get; private set; }
        /// <summary>
        /// Блокировка в.
        /// </summary>
        /// <value>Значение даты и время (DateTime)? .</value>
        public DateTime? BlockedAt { get; private set; } = null;
        /// <summary>
        /// Данные пользователя.
        /// </summary>
        /// <value>Список пользовательских данных.</value>
        private List<UserData> UserData { get; }
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="User"/> .
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="createdDate">Дата создания.</param>
        /// <param name="updatedDate">Дата обновления.</param>
        /// <param name="blockedAt">Заблокировано в.</param>
        private User(UserId userId, string userName, string password, DateTime createdDate,
            DateTime? updatedDate = null, DateTime? blockedAt = null) : base(userId ?? UserId.CreateUnique())
        {
            UserName = userName;
            Password = password;
            BlockedAt = blockedAt;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;            
        }
        /// <summary>
        /// Создает <see cref="User"/>.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="userData">Пользовательские данные.</param>
        /// <param name="createdDate">Дата создания.</param>
        /// <param name="updatedDate">Дата обновления.</param>
        /// <param name="blockedAt">Заблокировано в.</param>
        /// <returns>Возвращает пользователя (User).</returns>
        public static User Create(UserId userId, string userName, string password, UserData userData, DateTime createdDate, DateTime? updatedDate = null, DateTime? blockedAt = null)
        {
            var user = new User(userId, userName, password,  createdDate, updatedDate, blockedAt);
            user.AddDomainEvent(new UserCreated(user, userData));
            return user;
        }
        /// <summary>
        /// Преобразуется в строку.
        /// </summary>
        /// <returns>Возвращает строку.</returns>
        public override string ToString()
        {
            return $"Логин \"{UserName}\", Дата создания \"{CreatedDate.ToString()}\", ";
        }
    }
}
