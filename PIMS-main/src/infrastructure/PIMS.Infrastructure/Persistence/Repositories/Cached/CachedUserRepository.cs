using Microsoft.Extensions.Caching.Memory;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ReadOnlyModels;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Infrastructure.Persistence.Repositories.Cached.Base;
using PIMS.Infrastructure.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Cached
{
    /// <summary>
    /// Кэшированный репозиторий пользователей.
    /// </summary>
    public class CachedUserRepository : BaseCachedRepository,IUserRepository
    {
        /// <summary>
        /// Украшенный.
        /// </summary>
        private readonly IUserRepository _decorated;
        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="CachedUserRepository"/> .
        /// </summary>
        /// <param name="decorated">Украшенный.</param>
        /// <param name="memoryCache">Кэш памяти.</param>
        public CachedUserRepository(IUserRepository decorated, IMemoryCache memoryCache):base(memoryCache)
        {
            _decorated = decorated;             
        }

        /// <summary>
        /// Добавляет <see cref="Task"/>.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Возвращает значение задачи (Task).</returns>
        public async Task Add(User user)
        {
           await _decorated.Add(user); 
        }

        /// <summary>
        /// Пользователь по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Возвращает значение пользователя (User)?.</returns>
        public async Task<User?> GetUserById(UserId id)
        {
            return await _memoryCache.GetOrCreateAsync(BaseCachedRepository.FormatKey("UserById", id.Value.ToString()), async entry =>
            {
                SetExpirationTime(entry, TimeSpan.FromMinutes(2));
                return await _decorated.GetUserById(id);
            });
        }

        /// <summary>
        /// Пользователь по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Возвращает значение пользователя (User)?.</returns>
        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _memoryCache.GetOrCreateAsync(BaseCachedRepository.FormatKey("UserByUserName", userName), async entry =>
            {
                SetExpirationTime(entry, TimeSpan.FromMinutes(2));
                return await _decorated.GetUserByUserName(userName);
            });
        }

        /// <summary>
        /// Представление пользователя по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Возвращение значения представления пользователя (UserView)?.</returns>
        public async Task<UserView?> GetUserViewByUserName(string userName)
        {
            return await _memoryCache.GetOrCreateAsync(BaseCachedRepository.FormatKey("UserViewByUserName", userName), async entry =>
            {
                SetExpirationTime(entry, TimeSpan.FromMinutes(2));
                return await _decorated.GetUserViewByUserName(userName);
            });
        }
    }
}
