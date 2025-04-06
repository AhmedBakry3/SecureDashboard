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
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(IDepartmentRepository departmentRepository , IEmployeeRepository employeeRepository , ApplicationDbContext applicationDbContext) 
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _dbContext = applicationDbContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
