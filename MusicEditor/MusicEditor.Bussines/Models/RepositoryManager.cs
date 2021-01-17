using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Models
{
    public interface IRepositoryManager : IDisposable {
        int SaveChanges();

        IRepository<Music> Musicas { get; }
    }
    public class RepositoryManager : IRepositoryManager
    {
        private MusicContext _context;
        private IRepository<Music> _musica;

        public RepositoryManager(IRepository<Music> musica,
                                  MusicContext musicContext) {
            _musica = musica;
            _context = musicContext;
        }

        public IRepository<Music> Musicas => _musica;

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
