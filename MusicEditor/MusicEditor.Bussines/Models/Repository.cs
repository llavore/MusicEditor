using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicEditor.Bussines.Models
{
    public interface IRepository<T> where T:class{
        TContext ContextDb<TContext>() where TContext : DbContext;
        List<T> GetAll();
        T seleccionarUno(Expression<Func<T,bool>>predicate);

        List<T> seleccionarMusica(Expression<Func<T, bool>> predicate);
        void Clear();
        
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _contexto;
        public Repository(DbContext contexto)
        {
            _contexto = contexto;
        }

        public void Clear()
        {
            var list = GetAll();
            _contexto.Set<T>().RemoveRange(list);
        }

        public TContext ContextDb<TContext>() where TContext : DbContext
        {
            return _contexto as TContext;
        }

        public List<T> GetAll()
        {
            return _contexto.Set<T>().ToList();
        }

        public List<T> seleccionarMusica(Expression<Func<T, bool>> predicate)
        {
            return _contexto.Set<T>().Where<T>(predicate).ToList();
        }

        public T seleccionarUno(Expression<Func<T,bool>> predicate )
        {
           

            return _contexto.Set<T>().Where<T>(predicate).FirstOrDefault();
        }
    }
}
