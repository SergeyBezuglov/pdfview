using Microsoft.Extensions.Caching.Memory;

using PIMS.Infrastructure.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Cached.Base
{
    /// <summary>
    /// Базовый кэшированный репозиторий.
    /// </summary>
    public class BaseCachedRepository
    {
        /// <summary>
        /// Кэш памяти.
        /// </summary>
        protected readonly IMemoryCache _memoryCache;
        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="BaseCachedRepository"/> .
        /// </summary>
        /// <param name="memoryCache">Кэш памяти.</param>
        public BaseCachedRepository(IMemoryCache memoryCache)
        {            
            _memoryCache = memoryCache;
        }
        /// <summary>
        /// Форматирует ключ.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Возвращает значение строки.</returns>
        public static string FormatKey(string key,string value)
        {
            return $"{key}-{value}";
        }
        /// <summary>
        /// Срок годности.
        /// </summary>
        /// <param name="entry">Вход.</param>
        /// <param name="expirationTime">Срок годности.</param>
        public void SetExpirationTime(ICacheEntry entry, TimeSpan expirationTime)
        {
            entry.SetAbsoluteExpiration(expirationTime);
        }
    }
}
