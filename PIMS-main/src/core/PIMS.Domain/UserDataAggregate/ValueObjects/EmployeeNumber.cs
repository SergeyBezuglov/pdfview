using PIMS.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserDataAggregate.ValueObjects
{
    /// <summary>
    /// Табельный номер сотрудника.
    /// </summary>
    public class EmployeeNumber : OrdinaryValueObject
    {
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="EmployeeNumber"/> .
        /// </summary>
        private EmployeeNumber() : base()
        {

        }
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="EmployeeNumber"/> .
        /// </summary>
        /// <param name="value">Значение.</param>
        private EmployeeNumber(Guid value) : base(value)
        {

        }
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="EmployeeNumber"/> .
        /// </summary>
        /// <param name="ordinaryValueObject">Обычный объект значения.</param>
        private EmployeeNumber(OrdinaryValueObject ordinaryValueObject) : base(ordinaryValueObject) { }
        /// <summary>
        /// Создает уникальное.
        /// </summary>
        /// <returns>Возвращает табельный номер сотрудника (EmployeeNumber).</returns>
        public static EmployeeNumber CreateUnique()
        {
            return new(CreateUniqueBase());
        }

    }
}
