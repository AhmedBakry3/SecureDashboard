using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.RoleManagerModel;
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
    public class RoleRepository(ApplicationDbContext _dbContext) : IRoleRepository
    {

        public IEnumerable<ApplicationRole> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<ApplicationRole>().ToList();
            else
                return _dbContext.Set<ApplicationRole>().AsNoTracking().ToList();
        }

        public IEnumerable<ApplicationRole> GetAll(Expression<Func<ApplicationRole, bool>> Predicate)
        {
            //if (roleSearchName.HasValue)
            //{
            //    Predicate = r => r.RoleName == roleSearchName.Value;
            //}

            return _dbContext.Set<ApplicationRole>().Where(Predicate).ToList();
        }


        //Get By Id
        public ApplicationRole? GetById(string id) => _dbContext.Set<ApplicationRole>().Find(id);

        //Update
        public void Update(ApplicationRole entity)
        {
            _dbContext.Set<ApplicationRole>().Update(entity);
        }

        //Delete
        public void Remove(ApplicationRole entity)
        {
            _dbContext.Set<ApplicationRole>().Remove(entity);
        }

        //Insert
        public void Add(ApplicationRole entity)
        {
            _dbContext.Set<ApplicationRole>().Add(entity);
        }

    }
}
