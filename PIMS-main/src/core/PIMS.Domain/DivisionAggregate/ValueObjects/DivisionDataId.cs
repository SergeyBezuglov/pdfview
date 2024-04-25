using PIMS.Domain.Common.Models;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.OrganizationAggregate.ValueObjects;
using PIMS.Domain.PostAggregate.ValueObjects;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.DivisionAggregate.ValueObjects
{
    /// <summary>
    /// Идентификатор данных подразделения.
    /// </summary>
    public class DivisionDataId :  OrdinaryValueObject
    {
        /// <summary>
        /// Конструктор принимает аргумент Guid value и вызывает конструктор базового класса с переданным значением value с помощью ключевого слова base(value).
        /// </summary>
        private DivisionDataId(Guid value):base(value)
        {
            
        }
        /// <summary>
        /// Конструктор принимает аргумент OrdinaryValueObject valueObject и вызывает конструктор базового класса с переданным объектом valueObject 
        /// с помощью ключевого слова base(valueObject).
        /// </summary>
        private DivisionDataId(OrdinaryValueObject valueObject) : base(valueObject) { }
        /// <summary>
        /// Создает уникальную базу.
        /// </summary>
        public static DivisionDataId CreateUnique()
        {
            return new(CreateUniqueBase());
        }

    }
}
