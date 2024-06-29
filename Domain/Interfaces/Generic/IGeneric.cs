using Entities.Entities;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generic
{
    public interface IGeneric<T> where T : class
    {
        Task<Return> Add(T Objeto);
        Task<Return> Update(T Objeto);
        Task<Return> Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List(int numeroPagina, int quantidadePorPagina);
    }
}
