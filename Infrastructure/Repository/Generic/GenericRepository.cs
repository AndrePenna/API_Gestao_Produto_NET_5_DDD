using Domain.Interfaces.Generic;
using Entities.Notification;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generic
{
    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public GenericRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<Return> Add(T Objeto)
        {
            try
            {
                using (var data = new ContextBase(_OptionsBuilder))
                {
                    await data.Set<T>().AddAsync(Objeto);
                    await data.SaveChangesAsync();
                }
                return new Return(true, string.Empty);
            }
            catch (Exception)
            {
                return new Return(false, "Erro ao tentar persistir na base de dados!");
            }
            
        }

        public async Task<Return> Delete(T Objeto)
        {
            try {
                using (var data = new ContextBase(_OptionsBuilder))
                {
                    data.Set<T>().Remove(Objeto);
                    await data.SaveChangesAsync();
                }
                return new Return(true, string.Empty);
            }
            catch (Exception)
            {
                return new Return(false, "Erro ao tentar persistir na base de dados!");
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> List(int numeroPagina, int quantidadePorPagina)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                var retorno = await data.Set<T>().ToListAsync();
                if (retorno.Count <= quantidadePorPagina)
                    return retorno;

                return retorno.Skip(numeroPagina * quantidadePorPagina).Take(quantidadePorPagina).ToList();
            }
        }

        public async Task<Return> Update(T Objeto)
        {
            try {
                using (var data = new ContextBase(_OptionsBuilder))
                {
                    data.Set<T>().Update(Objeto);
                    await data.SaveChangesAsync();
                }
                return new Return(true, string.Empty);
            }
            catch (Exception)
            {
                return new Return(false, "Erro ao tentar persistir na base de dados!");
            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion


    }
}
