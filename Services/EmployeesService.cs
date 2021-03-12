using CashCrusadersFranchising.Api.Data;
using CashCrusadersFranchising.Api.Data.Entities;
using CashCrusadersFranchising.API.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users = CashCrusadersFranchising.Api.Data.Users;

namespace CashCrusadersFranchising.API.Service.Domain.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly CashCrusadersFranchisingDBContext _dbcontext;
        public Managers managers;
        
        public EmployeesService(CashCrusadersFranchisingDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
            
        }

        public async Task<IEnumerable<Employees>> GetEmployeesByManager(string name)
        {
            
            await GetManager(managers,name);
            var employees = await _dbcontext.Employees.AsQueryable().Where(x => x.ManagerId >= managers.Id).ToListAsync();
            return employees;
        }

        public async Task<Managers> GetManager(Managers model , string name)
        {
            model = await _dbcontext.Managers.AsQueryable().Where(x => x.Name == name).FirstOrDefaultAsync();
            managers = model;
            return  model;
        }
    }
}
