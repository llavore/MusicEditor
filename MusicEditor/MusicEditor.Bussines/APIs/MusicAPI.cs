using MusicEditor.Bussines.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.APIs
{

    public interface IMusicApi
    {
        Music GetOne(string category, int number);

        void LoadMusic(String path);
        DataTable ObtenerTodos();
        List<Music> ObtenerTodosCorrectos();
        List<Music> ObtenerTodosIncorrectos();
        List<String> ObtenerGrupos();
        List<String> ObtenerCategorias();
        DataRow ObtenerMusica(string path);
        DataRow ObtenerMusica(string categoria, int numero);
        List<DataRow> SeleccionarMusicaAPartirDeUnNumero(string categoria, int numero);
        int totalMusicaCategoria(string categoria);

        void ModificarMusica(string[] liniaModificada);
        DataRow NuevaLinea();

        int totalMusica();
        List<String[]> CantidadMusicaCategoria();

        void clear();

    }

    public enum MusicColumns
    {
        Category = 0,
        Number = 1,
        Title = 2,
        Group = 3,
        State = 4,
        Path = 5
    }
    public class MusicAPI : IMusicApi
    {
        private readonly IRepositoryManager _repositoryManager;

        public MusicAPI(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public List<string[]> CantidadMusicaCategoria()
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
          
        }

        public Music GetOne(string category, int number)
        {
            return _repositoryManager.Musicas
                   .seleccionarUno(x => x.Category == category && x.Number == number);
        }

        public void LoadMusic(String path)
        {
            _repositoryManager.Musicas.Clear();



            throw new NotImplementedException();
        }

        public void ModificarMusica(string[] liniaModificada)
        {
            throw new NotImplementedException();
        }

        public DataRow NuevaLinea()
        {
            throw new NotImplementedException();
        }

        public List<string> ObtenerCategorias()
        {
            throw new NotImplementedException();
        }

        public List<string> ObtenerGrupos()
        {
            throw new NotImplementedException();
        }

        public DataRow ObtenerMusica(string path)
        {
            throw new NotImplementedException();
        }

        public DataRow ObtenerMusica(string categoria, int numero)
        {
            throw new NotImplementedException();
        }

        public DataTable ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public List<Music> ObtenerTodosCorrectos()
        {
            return _repositoryManager.Musicas.seleccionarMusica(x => x.State == true);
        }

        public List<Music> ObtenerTodosIncorrectos()
        {
            return _repositoryManager.Musicas.seleccionarMusica(x => x.State == false);
        }

        public List<DataRow> SeleccionarMusicaAPartirDeUnNumero(string categoria, int numero)
        {
            throw new NotImplementedException();
        }

        public int totalMusica()
        {
            return _repositoryManager.Musicas.GetAll().Count;
        }

        public int totalMusicaCategoria(string categoria)
        {
            throw new NotImplementedException();
        }
    }
}
