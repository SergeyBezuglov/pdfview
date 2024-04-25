using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ReadOnlyModels;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Persistence
{
    /// <summary>
    /// Интерфейс репозитория пользователя.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получение пользователя по логину.
        /// </summary>
        /// <param name="userName">Логин.</param>
        Task<User?> GetUserByUserName(string userName);
        /// <summary>
        /// Пользователь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        Task<User?> GetUserById(UserId id);
        /// <summary>
        /// Получение модели представления о пользователе по логину.
        /// </summary>
        /// <param name="userName">Логин.</param>
        Task<UserView?> GetUserViewByUserName(string userName);
        /// <summary>
        /// Добавление данных о пользователе.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        Task Add(User user);
    }
}
