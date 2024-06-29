using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IProduct _IProduct;
        private ISupplier _ISupplier;

        public SupplierServices(ISupplier supplier, IProduct iProduct)
        {
            _ISupplier = supplier;
            _IProduct = iProduct;
        }

        public async Task<Return> Add(Supplier supplier)
        {
            if(_ISupplier.DocumentSupplierExist(supplier.Document))
                return new Return(false, "Erro! Fornecedor com documento já cadastrado.");

            return await _ISupplier.Add(supplier);
        }

        public async Task<Return> Delete(Supplier supplier)
        {
            if (_IProduct.SupplierHasProduct(supplier.Id))
                return new Return(false, "Erro! Fornecedor contem produtos atrelados.");

            return await _ISupplier.Delete(supplier);
        }

        public async Task<Supplier> GetEntityById(int Id)
        {
            return await _ISupplier.GetEntityById(Id);
        }

        public async Task<List<Supplier>> List(int numeroPagina, int quantidadePorPagina)
        {
            var retorno = await _ISupplier.List(numeroPagina, quantidadePorPagina);
            return retorno;
        }

        public async Task<Return> Update(Supplier supplier)
        {
            if (!_ISupplier.IdSupplierExist(supplier.Id))
                return new Return(false, "Erro! Fornecedor não existe em nossa base.");

            return await _ISupplier.Update(supplier);
        }
    }
}
