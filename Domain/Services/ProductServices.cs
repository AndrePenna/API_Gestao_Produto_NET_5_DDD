using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProduct _IProduct;
        private readonly ISupplier _ISupplier;

        public ProductServices(IProduct iProduct, ISupplier iSupplier)
        {
            _IProduct = iProduct;
            _ISupplier = iSupplier;
        }

        public async Task<Return> Add(Product product)
        {
            try
            {
                if (product.FabricationDate > product.ExpirationDate)
                    return new Return(false, "Erro! A data de fabricação não deve ser maior que a data de validade");

                Supplier supplier = null;
                if (product.SupplierId.HasValue)
                {
                    supplier = await _ISupplier.GetEntityById(product.SupplierId.Value);
                    if (supplier == null || product.SupplierId == 0)
                        return new Return(false, "Erro! Fornecedor não encontrado ou igual a 0.");
                }

                return await _IProduct.Add(product);
            }
            catch (Exception)
            {
                return new Return(false, $"Erro inesperado!");
            }
        }

        public async Task<Return> Delete(Product product)
        {
            return await _IProduct.Delete(product);
        }

        public async Task<Product> GetEntityById(int Id)
        {
            var product = await _IProduct.GetEntityById(Id);

            if (product == null) return null;

            if (product.SupplierId.HasValue)
                product.Supplier = await _ISupplier.GetEntityById(product.SupplierId.Value);

            return product;
        }

        public async Task<List<Product>> List(int numeroPagina, int quantidadePorPagina)
        {
            var teste = await _IProduct.List(numeroPagina, quantidadePorPagina);
            return teste;
        }

        public List<Product> ListProdutoSupplier(int numeroPagina, int quantidadePorPagina)
        {
            var teste = _IProduct.ListProdutoSupplier(numeroPagina, quantidadePorPagina);
            return teste;
        }

        public async Task<Return> Update(Product product)
        {
            if (product.FabricationDate > product.ExpirationDate)
                return new Return(false, "Erro! A data de fabricação não deve ser maior que a data de validade");

            Supplier supplier = null;
            if (product.SupplierId.HasValue)
            {
                supplier = await _ISupplier.GetEntityById(product.SupplierId.Value);
                if (supplier == null || product.SupplierId == 0)
                    return new Return(false, "Erro! Fornecedor não encontrado ou igual a 0.");
            }

            return await _IProduct.Update(product);
        }
    }
}
