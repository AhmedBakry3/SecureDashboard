using Demo.DataAccess.Models.RoleManagerModel;
using Demo.DataAccess.Models.UserManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserManager> GetAll(bool WithTracking = false);
        IEnumerable<UserManager> GetAll(Expression<Func<UserManager, bool>> Predicate);

        UserManager? GetById(string id);
        void Remove(UserManager entity);
        void Update(UserManager entity);
    }
}
