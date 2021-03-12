using CashCrusadersFranchising.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashCrusadersFranchising.API.Service.Domain.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly CashCrusadersFranchisingDBContext _dbcontext;
        public SuppliersService(CashCrusadersFranchisingDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public Suppliers Add(Suppliers model)
        {
            try
            {

                _dbcontext.Suppliers.Add(model);
                _dbcontext.SaveChanges();
                _dbcontext.Dispose();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return model;
        }

        public async Task<Suppliers> Get(string Id)
        {
            var suppliers = new Suppliers();
            try
            {
                suppliers = await _dbcontext.Suppliers.FindAsync(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return suppliers;
        }

        public async Task<IEnumerable<Suppliers>> GetAllSuppliers()
        {
            var suppliers = new List<Suppliers>();
            try
            {
                suppliers = await _dbcontext.Set<Suppliers>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return suppliers;
        }

        public async Task<int> Update(string Id, Suppliers model)
        {
            var suppliers = _dbcontext.Suppliers.FindAsync(Id);
            if (suppliers != null)
            {
                _dbcontext.Entry(suppliers).CurrentValues.SetValues(model);
            }

            return await Task.Run(() =>
            {
                _dbcontext.Update(suppliers);
                return _dbcontext.SaveChanges();
            });
        }

       
    }
}
