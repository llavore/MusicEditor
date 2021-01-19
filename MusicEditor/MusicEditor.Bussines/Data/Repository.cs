using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Data
{

    public enum ClassesDb { 
       Music = 0
    }


    public interface IRepository<T> where T : DataTable
    {
        TContext ContextDb<TContext>(TContext) where TContext : DataSet;

        List<T> GetAll();
        T seleccionarUno();

        List<T> seleccionarMusica();
        void Clear();

    }

    public class Repository<T> : IRepository<T> where T : DataTable
    {
        protected readonly DataSet _dataset;
        public Repository(DataSet contexto)
        {
            _dataset = contexto;
        }

        public void Clear()
        {
            var list = GetAll();
            _contexto.Set<T>().RemoveRange(list);
        }

        public TContext ContextDb<TContext>() where TContext : D
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

        public T seleccionarUno(Expression<Func<T, bool>> predicate)
        {


            return _contexto.Set<T>().Where<T>(predicate).FirstOrDefault();
        }
    }

}
