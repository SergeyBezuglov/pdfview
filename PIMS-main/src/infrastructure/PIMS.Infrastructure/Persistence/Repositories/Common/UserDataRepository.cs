using Microsoft.EntityFrameworkCore;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain.UserAggregate.Specifications;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Domain.UserDataAggregate;
using PIMS.Domain.UserDataAggregate.Specifications;
using PIMS.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Common
{
    /// <summary>
    /// Хранилище пользовательских данных.
    /// </summary>
    public class UserDataRepository:IUserDataRepository
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly PIMSDbContext _dbContext;

        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="UserDataRepository"/> .
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        public UserDataRepository(PIMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Добавляет <see cref="Task"/>.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Возвращает значение задачи (Task).</returns>
        public async Task Add(UserData user)
        {

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();            
        }
        /// <summary>
        /// Фактические данные пользователя по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращение значения данных пользователя (UserData)?.</returns>
        public async Task<UserData?> GetActualUserDataByUserId(UserId userId)
        {
            return await _dbContext.UserData.AsNoTracking().
                Where(new GetActualUserDataByUserIdSpecification(userId)).FirstOrDefaultAsync();
        }
    }
}
