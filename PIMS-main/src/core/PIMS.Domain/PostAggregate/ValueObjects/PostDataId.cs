using PIMS.Domain.Common.Interfaces.Base;
using PIMS.Domain.Common.Models;
using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.DivisionAggregate.ValueObjects;
using PIMS.Domain.OrganizationAggregate.ValueObjects;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.PostAggregate.ValueObjects
{
    /// <summary>
    /// Идентификатор данных публикации.
    /// </summary>
    public class PostDataId : OrdinaryValueObject, IOrdinaryValueObject<Guid>
    {
        /// <summary>
        /// Конструктор вызывает конструктор базового класса.
        /// </summary>
        private PostDataId() : base()
        {

        }
        /// <summary>
        /// Конструктор принимает аргумент Guid value и вызывает конструктор базового класса с переданным значением value с помощью ключевого слова base(value).
        /// </summary>
        private PostDataId(Guid value) : base(value)
        {

        }
        /// <summary>
        /// Конструктор принимает аргумент OrdinaryValueObject valueObject и вызывает конструктор базового класса с переданным значением valueObject
        /// с помощью ключевого слова base(valueObject).
        /// </summary>
        private PostDataId(OrdinaryValueObject valueObject) : base(valueObject) { }
        /// <summary>
        /// Создает уникальную базу.
        /// </summary>
        public static PostDataId CreateUnique()
        {
            return new(CreateUniqueBase());
        }
    }
}
