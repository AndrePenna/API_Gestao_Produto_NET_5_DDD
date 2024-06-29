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
    public class ProductRepository : GenericRepository<Product>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public ProductRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public List<Product> ListProdutoSupplier(int numeroPagina, int quantidadePorPagina)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                var retorno = (from p in data.Product
                               select new Product
                               {
                                   Id = p.Id,
                                   Description = p.Description,
                                   ExpirationDate = p.ExpirationDate,
                                   FabricationDate = p.FabricationDate,
                                   IsActive = p.IsActive,
                                   Supplier = p.SupplierId.HasValue ? (from sp in data.Supplier where sp.Id == p.SupplierId.Value select sp).FirstOrDefault() : null
                               }).ToList();

                if (retorno.Count <= quantidadePorPagina)
                    return retorno;

                return retorno.Skip(numeroPagina * quantidadePorPagina).Take(quantidadePorPagina).ToList();
            }
        }

        public bool SupplierHasProduct(int supplierId)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return data.Product.Any(x => x.SupplierId == supplierId);
            }
        }
    }
}
