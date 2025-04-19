using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.RoleManagerModel;
using Demo.DataAccess.Models.UserManagerModel;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
    {

        public IEnumerable<UserManager> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<UserManager>().ToList();
            else
                return _dbContext.Set<UserManager>().AsNoTracking().ToList();
        }

        public IEnumerable<UserManager> GetAll(Expression<Func<UserManager, bool>> Predicate)
        {
            return _dbContext.Set<UserManager>().Where(Predicate).ToList();
        }
        //Get By Id
        public UserManager? GetById(string id) => _dbContext.Set<UserManager>().Find(id);

        //Update
        public void Update(UserManager entity)
        {
            _dbContext.Set<UserManager>().Update(entity);
        }

        //Delete
        public void Remove(UserManager entity)
        {
            _dbContext.Set<UserManager>().Remove(entity);
        }

    }
}
