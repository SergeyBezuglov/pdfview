using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserAggregate.ReadOnlyModels
{
    /// <summary>
    /// Вид пользователя.
    /// </summary>
    public class UserView
    {
        /// <summary>
        /// Свойство дата обновления должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public required DateTime? UpdateDate { get; set; }
        /// <summary>
        /// Свойство имя пользователя должно быть передано или установлено при создании экземпляра объекта.
        /// </summary>
        public required string UserName { get; set; }
        /// <summary>
        /// Свойство имя должно быть передано или установлено при создании экземпляра объекта..
        /// </summary>
        public required string FirstName { get; set; }
    }
}
