using PIMS.Domain.Common.Models;
using PIMS.Domain.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserDataAggregate.ValueObjects
{
    /// <summary>
    /// Почта (email).
    /// </summary>
    public class Email : ValueObject
    {

        /// <summary>
        /// Рабочая электронная почта.
        /// </summary>
        /// <value>Строка.</value>
        public string WorkEmail { get; private set; } = string.Empty;
        /// <summary>
        /// Домашняя электронная почта.
        /// </summary>
        /// <value>Строка.</value>
        public string HomeEmail { get; private set; } = string.Empty;
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="Email"/> .
        /// </summary>
        private Email() : base()
        {

        }
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="Email"/> .
        /// </summary>
        /// <param name="workEmail">Рабочая электронная почта.</param>
        /// <param name="homeEmail">Домашняя электронная почта.</param>
        private Email(string workEmail, string homeEmail = "")
        {
            WorkEmail = workEmail;
            HomeEmail = homeEmail;
        }

        /// <summary>
        /// Создает электронное почту.
        /// </summary>
        /// <param name="workEmail">Рабочая электронная почта.</param>
        /// <param name="homeEmail">Домашняя электронная почта.</param>
        /// <returns>Возвращает электронную почту (Email).</returns>
        public static Email CreateEmail(string workEmail, string homeEmail = "")
        {
            return new(workEmail, homeEmail);
        }
        /// <summary>
        /// Компоненты равенства.
        /// </summary>
        /// <returns>Возвращает список объектов</returns>
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return WorkEmail;
            yield return HomeEmail;
        }
    }
}
