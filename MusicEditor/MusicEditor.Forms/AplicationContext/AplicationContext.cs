using MusicEditor.Bussines.APIs;
using MusicEditor.Bussines.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Forms.AplicationContext
{
    class AplicationContext
    {
        public static MusicContext _context;
        public static IRepositoryManager _repositoryManager;
       
        public static void LoadData(String path) {
            _context = new MusicContext(path);
            _repositoryManager = new RepositoryManager(_context,
                                                   new Repository<MusicasDataTable>(_context));
        }

    }
}
