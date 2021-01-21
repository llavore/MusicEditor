using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Data
{
    public interface IRepository<T> where T : class
    {
        TContext ContextDb<TContext>() where TContext : MusicContext;

    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MusicContext _contexto;
        public Repository(MusicContext contexto)
        {
            _contexto = contexto;
        }

        public TContext ContextDb<TContext>() where TContext : MusicContext
        {
            return _contexto as TContext;
        }


    }

}
