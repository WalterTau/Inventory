using CashCrusadersFranchising.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashCrusadersFranchising.API.Service.Domain.Services
{
    public class PurchaseOrdersService : IPurchaseOrdersService
    {
        private readonly CashCrusadersFranchisingDBContext _dbcontext;
        public PurchaseOrdersService(CashCrusadersFranchisingDBContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public  PurchaseOrder Add(PurchaseOrder model)
        {
            //find price per product
            if (model.ProductId > 0)
            {
                var pricePerProduct = model.Products.Find(x => x.Id == model.ProductId).Price;
                
                if (model.VAT > 0) {

                    // Calculate subtotal
                    var vatAmount = (pricePerProduct * model.Qty) * model.VAT;

                    // Before SubTotal before VAT
                    model.Subtotal = pricePerProduct * model.Qty;

                    // Calculate Total Include VAT
                    model.Total = model.Subtotal + vatAmount;
                   }
            }
         

            try
            {

                _dbcontext.PurchaseOrders.Add(model);
                _dbcontext.SaveChanges();
                _dbcontext.Dispose();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return model;
        }

        public async Task<PurchaseOrder> Get(string Id)
        {
            var purchasedorder = new PurchaseOrder();
            try
            {
                purchasedorder = await _dbcontext.PurchaseOrders.FindAsync(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return purchasedorder;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchase()
        {
            var purchasedorders = new List<PurchaseOrder>();
            try
            {
                purchasedorders = await _dbcontext.Set<PurchaseOrder>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }
            return purchasedorders;
        }

        public async Task<int> Update(string Id , PurchaseOrder model)
        {
           
            var purchaseOrder = _dbcontext.PurchaseOrders.FindAsync(Id);
            if (purchaseOrder != null) 
            {
                _dbcontext.Entry(purchaseOrder).CurrentValues.SetValues(model);
            }
          
            return await Task.Run(() =>
            {
                _dbcontext.Update(purchaseOrder);
                return _dbcontext.SaveChanges();
            });
        }
    }
}
