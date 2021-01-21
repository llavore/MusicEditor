using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Data
{
    public interface IRepositoryManager : IDisposable
    {
        int SaveChanges();
        IRepository<MusicasDataTable> Musica { get; }
       
        
    }
    public class RepositoryManager : IRepositoryManager
    {
        private MusicContext _context;
        private IRepository<MusicasDataTable> _musica;
      
        public RepositoryManager(MusicContext musicContext,
                                 IRepository<MusicasDataTable> musica)
        {
            _musica = musica;
            _context = musicContext;
        }


        public IRepository<MusicasDataTable> Musica => _musica;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
