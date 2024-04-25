using Microsoft.EntityFrameworkCore;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain.UserAggregate;
using PIMS.Domain.UserAggregate.ReadOnlyModels;
using PIMS.Domain.UserAggregate.Specifications;
using PIMS.Domain.UserAggregate.ValueObjects;
using PIMS.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Common
{
    /// <summary>
    /// Пользовательский репозиторий.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly PIMSDbContext _dbContext;

        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="UserRepository"/> .
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        public UserRepository(PIMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Добавляет <see cref="Task"/>.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Возвращает значение задачи (Task).</returns>
        public async Task Add(User user)
        {

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Пользователь по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Возвращает значение пользователя (User)?.</returns>
        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _dbContext.Users.AsNoTracking().Where(new SearchByUserNameSpecification(userName)).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Пользователь по идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Возвращает значение пользователя (User)?.</returns>
        public async Task<User?> GetUserById(UserId userId)
        {
            return await _dbContext.Users.AsNoTracking().Where(new SearchByIdSpecification(userId)).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Представление пользователя по имени пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Возвращает значение представления пользователя (UserView)?.</returns>
        public async Task<UserView?> GetUserViewByUserName(string userName)
        {
            return await _dbContext.Users.AsNoTracking().Where(new SearchByUserNameSpecification(userName)).
                Join(_dbContext.UserData, m => m.Id, ud => ud.UserId, (u, ud) =>new {ud.UpdatedDate,ud.FirstName,u.UserName }).                
                Select(m=>
            new UserView() { 
                UpdateDate=m.UpdatedDate,
                FirstName=m.FirstName,
                UserName=m.UserName ?? ""            
            }).OrderByDescending(u=>u.UpdateDate).FirstOrDefaultAsync();
        }
        
    }
}
