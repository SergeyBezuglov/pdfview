using PIMS.Domain.Common.Models.Base;
using PIMS.Domain.DivisionAggregate.ValueObjects;
using PIMS.Domain.PostAggregate.ValueObjects;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.Events;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate.Events;
using PIMS.Domain.UserDataAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PIMS.Domain.Common.Errors.Errors;
using static PIMS.Domain.UserDataAggregate.Enums.WorkingHours;

namespace PIMS.Domain.UserDataAggregate
{
    /// <summary>
    /// Пользовательские данные.
    /// </summary>
    public class UserData : AggregateRootGuid<UserDataId>
    {
#pragma warning disable CS8618
        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="UserData"/> .
        /// </summary>
        private UserData() : base()
        {

        }
#pragma warning restore CS8618

        /// <summary>
        /// Предотвращает создание экземпляра класса по умолчанию <see cref="UserData"/> .
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userDataId">Идентификатор пользовательских данных.</param>
        /// <param name="updatedDate">Обновленная дата.</param>
        /// <param name="firstName">Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="email">Электронная почта  (email).</param>
        /// <param name="phone">Телефон.</param>
        /// <param name="workingHours">Рабочее время.</param>
        /// <param name="preventUpdate">Если true, запретить обновление.</param>
        /// <param name="firedAt">Уволенный.</param>
        /// <param name="divisionId">Идентификатор подразделения.</param>
        /// <param name="postId">Идентификатор должности.</param>
        /// <param name="employeeNumber">Табельный номер сотрудника.</param>
        /// <param name="dateOfBirth">Дата рождения.</param>
        private UserData(UserId userId, UserDataId userDataId, DateTime updatedDate,
            string firstName, string middleName, string lastName, Email email, Phone phone,            
            WorkingHoursEnum workingHours = WorkingHoursEnum.Normal, bool preventUpdate = false, DateTime? firedAt = null, 
            DivisionDataId? divisionId = null, PostDataId? postId = null, EmployeeNumber? employeeNumber = null, DateTime? dateOfBirth = null) :
            base(userDataId ?? UserDataId.CreateUnique())
        {
            UserId = userId;
            PreventUpdate = preventUpdate;
            FiredAt = firedAt;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            WorkingHours = workingHours;
            //DivisionId = divisionId;
            //PostId = postId;
            EmployeeNumber = employeeNumber;
            DateOfBirth = dateOfBirth;
            UpdatedDate = updatedDate;


        }

        /// <summary>
        /// Первое имя.
        /// </summary>
        /// <value>Строка.</value>
        public string FirstName { get; private set; }
        /// <summary>
        /// Среднее имя.
        /// </summary>
        /// <value>Строка? .</value>
        public string? MiddleName { get; private set; }
        /// <summary>
        /// Последнее имя.
        /// </summary>
        /// <value>Строка.</value>
        public string LastName { get; private set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        /// <value>Значение совокупного корневого идентификатора (AggregateRootId).</value>
        public AggregateRootId<Guid> UserId { get; set; }

        /// <summary>
        /// Значение, указывающее, запрещено ли обновление.
        /// </summary>
        /// <value>Булевое значение.</value>
        public bool PreventUpdate { get; private set; }
        /// <summary>
        /// Уволенный.
        /// </summary>
        /// <value>Значение даты и время (DateTime)? .</value>
        public DateTime? FiredAt { get; private set; }
        /// <summary>
        /// Обновленная дата.
        /// </summary>
        /// <value>Значение даты и время (DateTime).</value>
        public DateTime UpdatedDate { get; private set; }

        /// <summary>
        /// Пользователя.
        /// </summary>
        /// <value>Значение пользовательский агрегат пользователя (UserAggregate.User.)</value>
        private UserAggregate.User User { get; }

        /// <summary>
        /// Электронную почту (email).
        /// </summary>
        /// <value>Значение электронной почты (Email).</value>
        public Email Email { get; private set; }
        /// <summary>
        /// Телефон.
        /// </summary>
        /// <value>Значение телефона (Phone).</value>
        public Phone Phone { get; private set; }

        //public DivisionId? DivisionId { get; private set; } = null;

        //public PostId? PostId { get; private set; } = null;

        /// <summary>
        /// Номер сотрудника.
        /// </summary>
        /// <value>Значение номера сотрудника (EmployeeNumber)? .</value>
        public EmployeeNumber? EmployeeNumber { get; private set; } = null;

        /// <summary>
        /// Дата рождения.
        /// </summary>
        /// <value>Значение даты и время (DateTime)? .</value>
        public DateTime? DateOfBirth { get; private set; } = null;
        /// <summary>
        /// Рабочее время.
        /// </summary>
        /// <value>Значение перечисления рабочих часов (WorkingHoursEnum).</value>
        public WorkingHoursEnum WorkingHours { get; private set; } = WorkingHoursEnum.Normal;


        /// <summary>
        /// Создает <see cref="UserData"/>.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="userDataId">Идентификатор пользовательских данных.</param>
        /// <param name="updatedDate">Обновленная дата.</param>
        /// <param name="firstName">Первое имя.</param>
        /// <param name="middleName">Среднее имя.</param>
        /// <param name="lastName">Последнее имя.</param>
        /// <param name="email">Электронная почта (email).</param>
        /// <param name="phone">Телефон.</param>
        /// <param name="workingHours">Рабочее время.</param>
        /// <param name="preventUpdate">Если это true, запретить обновление.</param>
        /// <param name="firedAt">Уволенный.</param>
        /// <param name="divisionId">Идентификатор подразделения.</param>
        /// <param name="postId">Идентификатор должности.</param>
        /// <param name="employeeNumber">Табельный номер сотрудника.</param>
        /// <param name="dateOfBirth">Дата рождения.</param>
        /// <returns>Возврат пользовательских данных (UserData).</returns>
        public static UserData Create(UserId userId, UserDataId userDataId, DateTime updatedDate,
            string firstName, string middleName, string lastName, Email email, Phone phone,
            WorkingHoursEnum workingHours = WorkingHoursEnum.Normal, bool preventUpdate = false, DateTime? firedAt = null, 
            DivisionDataId? divisionId = null, PostDataId? postId = null, EmployeeNumber? employeeNumber = null,
            DateTime? dateOfBirth = null)
        {
            UserData userData = new(userId, userDataId, updatedDate, firstName, middleName, lastName,
                email, phone, workingHours, preventUpdate, firedAt,  divisionId, postId, employeeNumber, dateOfBirth);
            userData.AddDomainEvent(new UserDataCreated(userData));
            return userData;
        }

    }
}
