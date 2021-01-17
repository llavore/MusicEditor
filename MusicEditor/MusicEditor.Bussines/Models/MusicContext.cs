using System;
using System.Data.Entity;
using System.Linq;

namespace MusicEditor.Bussines.Models
{
    public class MusicContext : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'MusicContext' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'MusicEditor.Bussines.Models.MusicContext' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'MusicContext'  en el archivo de configuración de la aplicación.
        public MusicContext()
            : base("name=MusicContext")
        {
        }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Music> Musicas { get; set; }
    }

    
}