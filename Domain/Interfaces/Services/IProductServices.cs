using Domain.Interfaces.Generic;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProductServices : IGeneric<Product>
    {
        List<Product> ListProdutoSupplier(int numeroPagina, int quantidadePorPagina);
    }
}
