using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly Lazy<IEmployeeRepository>  _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IUserRepository> _userManagerRepository;
        private readonly Lazy<IRoleRepository> _roleManagerRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext) 
        {
            _dbContext = applicationDbContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_dbContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_dbContext));
            _userManagerRepository = new Lazy<IUserRepository>(() => new UserRepository(_dbContext));
            _roleManagerRepository = new Lazy<IRoleRepository>(() => new RoleRepository(_dbContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public IUserRepository UserManagerRepository => _userManagerRepository.Value;

        public IRoleRepository roleManagerRepository => _roleManagerRepository.Value;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
