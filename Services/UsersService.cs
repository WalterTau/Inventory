using CashCrusadersFranchising.Api.Data;
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
    public class UsersService : IUsers
    {
        private readonly CashCrusadersFranchisingDBContext _dbcontext;
       
        public UsersService(CashCrusadersFranchisingDBContext dbcontext  )
        {
            this._dbcontext = dbcontext;
            
        }

        public async Task<IEnumerable<Users>> GetUsers() 
        {
            var result = _dbcontext.Set<Users>();
            
            return result.Where(x => x.Categories.StartsWith("U") || x.Categories.StartsWith("ul") || x.Categories.StartsWith("u gr"));
        }

   
        public async Task<IEnumerable<Users>> GetCategories(string searchPrefix)
        {
            var searchedResult = new List<Users>();
            try
            {
                var userCatgoryList = _dbcontext.Set<Users>();
             

                if (searchPrefix.Equals("u"))
                {
                    searchedResult = userCatgoryList.Where(x => x.Categories.StartsWith(searchPrefix)).ToList();
                }
                else
                {
                    if (searchPrefix.Equals("u l"))
                    {
                        searchedResult = userCatgoryList.Where(x => x.Categories.Equals("Users Activity Log")).ToList();
                    }

                    else
                         if (searchPrefix.Equals("u gr"))
                    {
                        searchedResult = userCatgoryList.Where(x => x.Categories.Equals("User Groups")).ToList();
                    }
                }
            } catch (DbUpdateException ex) 
            {
                Console.WriteLine(string.Format("{0}-{1}", "Error ", ex.InnerException));
            }

            return searchedResult; 
        }

      
    }
}
