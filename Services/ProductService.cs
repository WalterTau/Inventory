using CashCrusadersFranchising.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashCrusadersFranchising.API.Service.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly CashCrusadersFranchisingDBContext _dbcontext;
        public ProductService(CashCrusadersFranchisingDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public Product Add(Product model)
        {
            try
            {

                _dbcontext.Product.Add(model);
                _dbcontext.SaveChanges();
                _dbcontext.Dispose();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return model;
        }

        public async Task<Product> Get(string Id)
        {
           var product = await _dbcontext.Product.FindAsync(Id);
           return product;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var result = new List<Product>();
            try
            {
                result = await _dbcontext.Set<Product>().ToListAsync();
             
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }

            return  result;

        }

        public async Task<int> Update(string Id , Product model)
        {
            var product = _dbcontext.Product.FindAsync(Id);
            if (product != null)
            {
                _dbcontext.Entry(product).CurrentValues.SetValues(model);
            }

            return await Task.Run(() =>
            {
                _dbcontext.Update(product);
                return _dbcontext.SaveChanges();
            });

        }

     
    }
}
