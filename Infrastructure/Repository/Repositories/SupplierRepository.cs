using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplier
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public SupplierRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public bool DocumentSupplierExist(string document)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return data.Supplier.Any(x => x.Document == document);
            }
        }

        public bool IdSupplierExist(int supplierId)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return data.Supplier.Any(x => x.Id == supplierId);
            }
        }
    }
}
