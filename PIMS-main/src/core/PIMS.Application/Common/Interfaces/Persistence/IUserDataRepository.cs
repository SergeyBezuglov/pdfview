using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Persistence
{
    /// <summary>
    /// Интерфейс хранилища пользовательских данных.
    /// </summary>
    public interface IUserDataRepository
    {
        /// <summary>
        /// Добавление данных о пользователе.
        /// </summary>
        /// <param name="user">Пользователя.</param>
        /// <returns>Возвращение результата выполнения.</returns>
        Task Add(UserData user);
        /// <summary>
        /// Получает фактические данные пользователя по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Пользовательский идентификатор.</param>
        /// <returns>Возвращение результата выполнения.</returns>
        Task<UserData?> GetActualUserDataByUserId(UserId userId);
    }
}
