using WebShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DAL.Data.Repositories.SiteConfig
{
    public class SiteConfigRepository : ISiteConfigRepository
    {
        private readonly WebShopContext _context;
        public SiteConfigRepository(WebShopContext context)
        {
            _context = context;
        }

    }
}
