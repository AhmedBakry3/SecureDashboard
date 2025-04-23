using Demo.DataAccess.Models.RoleManagerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        void Add(ApplicationRole entity);
        IEnumerable<ApplicationRole> GetAll(bool WithTracking = false);
        IEnumerable<ApplicationRole> GetAll(Expression<Func<ApplicationRole, bool>> Predicate);

        ApplicationRole? GetById(string id);
        void Remove(ApplicationRole entity);
        void Update(ApplicationRole entity);
    }
}
