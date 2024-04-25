using PIMS.Domain.Common.Models.Specifications;
using PIMS.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserAggregate.Specifications
{
    /// <summary>
    ///  Поиск по спецификации идентификатора.
    /// </summary>
    public class SearchByIdSpecification : BaseSpecification<User>
    {
        /// <summary>
        /// Конструктор принимает аргумент UserId userId и вызывает конструктор базового класса
        /// с переданной стрелочной функцией для получения идентификатора пользователя.
        /// </summary>
        public SearchByIdSpecification(UserId userId) : base(user => user.Id == userId) { }
    }
}
