using PIMS.Domain.Common.Models.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain.UserAggregate.Specifications
{
    /// <summary>
    /// Поиск по спецификации имени пользователя.
    /// </summary>
    public class SearchByUserNameSpecification: BaseSpecification<User>
    {
        /// <summary>
        ///Конструктор принимает аргумент string UserName и вызывает конструктор базового класса
        /// с переданной стрелочной функцией для получения имени пользователя.
        /// </summary>
        public SearchByUserNameSpecification(string UserName):base(user=>user.UserName==UserName) { }
    }
}
