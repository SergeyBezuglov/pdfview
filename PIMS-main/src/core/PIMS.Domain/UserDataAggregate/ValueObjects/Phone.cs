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
    /// Телефон.
    /// </summary>
    public class Phone : ValueObject
    {
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="Phone"/> .
        /// </summary>
        private Phone() : base() { }
        /// <summary>
        /// Городской номер телефона.
        /// </summary>
        /// <value>Строка.</value>
        public string CityPhoneNumber { get; private set; } = string.Empty;
        /// <summary>
        /// Номер мобильного телефона.
        /// </summary>
        /// <value>Строка? .</value>
        public string? MobilePhoneNumber { get; private set; } = null;


        /// <summary>
        /// Внутренний номер телефона.
        /// </summary>
        /// <value>Строка? .</value>
        public string? InnerPhoneNumber { get; private set; } = null;

        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="Phone"/> .
        /// </summary>
        /// <param name="cityPhoneNumber">Городской номер телефона.</param>
        /// <param name="mobilePhoneNumber">Номер мобильного телефона.</param>
        /// <param name="innerPhoneNumber">Внутренний номер телефона.</param>
        private Phone(string cityPhoneNumber, string mobilePhoneNumber = "", string innerPhoneNumber = "")
        {
            CityPhoneNumber = cityPhoneNumber;
            MobilePhoneNumber = mobilePhoneNumber;
            InnerPhoneNumber = innerPhoneNumber;
        }

        /// <summary>
        /// Создает телефон.
        /// </summary>
        /// <param name="cityPhoneNumber">Городской номер телефона.</param>
        /// <param name="mobilePhoneNumber">Номер мобильного телефона.</param>
        /// <param name="innerPhoneNumber">Внутренний номер телефона.</param>
        /// <returns>Возвращает телефон.</returns>
        public static Phone CreatePhone(string cityPhoneNumber, string mobilePhoneNumber = "", string innerPhoneNumber = "")
        {
            return new(cityPhoneNumber, mobilePhoneNumber, innerPhoneNumber);
        }
        /// <summary>
        /// Компоненты равенства.
        /// </summary>
        /// <returns>Возвращает список объектов.</returns>
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return CityPhoneNumber;
            yield return MobilePhoneNumber ?? "";
            yield return InnerPhoneNumber ?? "";
        }
    }
}
