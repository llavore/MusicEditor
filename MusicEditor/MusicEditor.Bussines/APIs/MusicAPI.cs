using MusicEditor.Bussines.Data;
using MusicEditor.Bussines.Helpers;
using MusicEditor.Forms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MusicEditor.Forms.Models.MusicView;

namespace MusicEditor.Bussines.APIs
{

    public interface IMusicApi
    {
        DataTable ObtenerTodos();
        DataView ObtenerTodosCorrectos();
        DataView ObtenerTodosIncorrectos();
        List<String> ObtenerGrupos();
        List<String> ObtenerCategorias();
        MusicView ObtenerMusicaFormatView(string path);
        DataRow ObtenerMusica(string path);
        DataRow ObtenerMusica(string categoria, int numero);
        int totalMusicaCategoria(string categoria);
        int hasChange();
        int totalMusica();
        int CantidadMusicaDeUnaCategoria(string cat);

        void ModificarMusica(MusicView row);

        void SaveAll();
    }

    public class MusicAPI : IMusicApi
    {
        private readonly string CATEGORY = MusicasDataTable.CATEGORY;
        private readonly string NUMBER = MusicasDataTable.NUMBER;
        private readonly string TITLE = MusicasDataTable.TITLE;
        private readonly string GROUP = MusicasDataTable.GROUP;
        private readonly string STATE = MusicasDataTable.STATE;
        private readonly string PATH = MusicasDataTable.PATH;

        private IRepositoryManager _rm;
        private MusicasDataTable _table;
        private readonly string CATUKN = MusicContext.CategoryUnkown;

        public MusicAPI(IRepositoryManager rm)
        {
            _rm = rm;
            _table = _rm.Musica.ContextDb<MusicContext>().Musicas;
        }

        public List<string> ObtenerGrupos()
        {

            List<string> grupos = _table.AsEnumerable().Where(x => x.Get(CATEGORY) != CATUKN)
                                  .Select(x => x.Get(GROUP)).Distinct().ToList();
            return grupos;
        }

        private object TransformValueColumn(object v)
        {
            throw new NotImplementedException();
        }

        public DataTable ObtenerTodos()
        {
            return _table;
        }

        public DataView ObtenerTodosCorrectos()
        {
            DataView dv = new DataView(_table);
            dv.RowFilter = "Category <> '" + CATUKN +"'";
            dv.Sort = "Category, Number ";
            return dv;
        }

        public DataView ObtenerTodosIncorrectos()
        {
            DataView dv = new DataView(_table);
            dv.RowFilter = "Category = '" + CATUKN + "'";
            dv.Sort = "Category, Number ";
            return dv;
        }

        public int totalMusica()
        {
            return _table.AsEnumerable().Count();
        }

        public DataRow ObtenerMusica(string path)
        {
            DataRow rowSelected = _table.AsEnumerable().Where(x => x.Get(PATH) == path)
                                    .FirstOrDefault();

            return rowSelected;
        }

        public DataRow ObtenerMusica(string categoria, int numero)
        {
            return  _table.AsEnumerable()
                          .Where(x => x.Get(CATEGORY) == categoria &&
                                      x.Value(NUMBER) == numero)
                          .FirstOrDefault();
           
        }

        public List<string> ObtenerCategorias()
        {
            return _table.AsEnumerable().Where(x => x.Get(CATEGORY) != CATUKN).Select(x => x.Get(CATEGORY)).Distinct().ToList();
            
        }

        public int totalMusicaCategoria(string categoria)
        {
            return  _table.AsEnumerable().Where(x => x.Get(CATEGORY) == categoria).Count();
           
        }
        private void ModificarLinea(MusicView liniaModificada)
        {
            DataRow linea   = _table.NewRow();
            linea[CATEGORY] = liniaModificada.category;
            linea[NUMBER]   = liniaModificada.number;
            linea[TITLE]    = liniaModificada.title;
            linea[GROUP]    = liniaModificada.group;
            linea[STATE]    = DataRowState.Modified.ToString();
            linea[PATH]     = liniaModificada.path;
           
            _table.Rows.Remove(ObtenerMusica(liniaModificada.path));
            _table.Rows.Add(linea);
        }
        public void ModificarMusica(MusicView liniaModificada)
        {
            
            string state = liniaModificada.category;
            if (state != CATUKN) {
                DataRow lineaOrigen = ObtenerMusica(liniaModificada.path);
                DataRow lineaFinal = ObtenerMusica(liniaModificada.category, liniaModificada.number);

                var categoriaOrigen = lineaOrigen.Get(CATEGORY);
                var numeroOrigen = lineaOrigen.Value(NUMBER);

                string categoriaFinal = null;
                var numeroFinal = -1;
                if (lineaFinal != null) { 
                categoriaFinal = lineaFinal.Get(CATEGORY);
                numeroFinal = lineaFinal.Value(NUMBER);
                }
                Console.WriteLine(" ");
                Console.WriteLine("****inicio del proceso****");


                if (lineaFinal == null)
                {
                    Console.WriteLine("Actualizar Origen");
                    ModificarLinea(liniaModificada);
                    if (categoriaOrigen != CATUKN)
                    {
                        Console.WriteLine("Las lineas siguientes de:{{CAT: " + categoriaOrigen + " }{NUM: " + numeroOrigen + " }} "
                                            + " han sido actualizadas como number -1");
                        ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen + 1, CantidadMusicaDeUnaCategoria(categoriaOrigen), -1);
                    }

                }
                else if (categoriaOrigen == categoriaFinal)
                {
                    if (numeroOrigen > numeroFinal)
                    {
                        Console.WriteLine("Lineas entre {{CAT:" + categoriaOrigen + "}{NUM:" + numeroOrigen + "}}  i {{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} "
                            + "incluida: {{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} se ha actualizado el numero en number+1");
                        ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroFinal, numeroOrigen, +1);
                        Console.WriteLine("Actualizar Origen");
                    }
                    if (numeroOrigen < numeroFinal)
                    {
                        Console.WriteLine("Lineas entre {{CAT:" + categoriaOrigen + "}{NUM:" + numeroOrigen + "}}  i {{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} "
                            + "sin incluir ambas se ha actualizado el numero en number-1");
                        ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen, numeroFinal, -1);
                        Console.WriteLine("Actualizar Origen");
                    }
                    ModificarLinea(liniaModificada);
                }
                else if (categoriaOrigen != categoriaFinal)
                {
                    Console.WriteLine("Las lineas siguientes de:{{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} "
                                            + " han sido actualizadas como number +1");
                    ChangeNumberMusicaAPartirDeDosFilas(categoriaFinal, numeroFinal, CantidadMusicaDeUnaCategoria(categoriaFinal), +1);
                    Console.WriteLine("Las lineas siguientes de:{{CAT:" + categoriaOrigen + "}{NUM:" + numeroOrigen + "}} "
                                            + " han sido actualizadas como number -1");
                    ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen + 1, CantidadMusicaDeUnaCategoria(categoriaOrigen), -1);
                    Console.WriteLine("Actualizar Origen");
                    ModificarLinea(liniaModificada);
                }
                Console.WriteLine("****Final proceso****");
            }
        }

        public List<DataRow> ChangeNumberMusicaAPartirDeDosFilas(string categoria, int numero1, int numero2, int value)
        {

            var rowSelected = _table.AsEnumerable()
                                      .Where(x => x.ItemArray[0].ToString() == categoria &&
                                       Int32.Parse(x.ItemArray[1].ToString()) >= numero1 &&
                                       Int32.Parse(x[1].ToString()) <= numero2)
                                      .Reverse().ToList();

            int numeroSumar = value;
            rowSelected.ForEach(
                x => {
                    x["Number"] = Int32.Parse(x.ItemArray[1].ToString()) + numeroSumar;
                    x["State"] = DataRowState.Modified.ToString();
                });

            return rowSelected;
        }

        public int CantidadMusicaDeUnaCategoria(string cat)
        {
            return _table.AsEnumerable().Where(x => x.Get(CATEGORY) == cat).Count();        
        }

        public int hasChange()
        {
            return _table.AsEnumerable().Where(x => x.Get(STATE) == DataRowState.Modified.ToString()).Count();
        }

        public void SaveAll()
        {
            FileInfo file;
            var list = _table.AsEnumerable().Where(x => x.Get(STATE) == DataRowState.Modified.ToString()).ToList();
            list.ForEach(x => {
                file = new FileInfo(x.Get(PATH));
                Console.WriteLine(file.DirectoryName);
                string[] directoryName = file.DirectoryName.Split('\\');

                directoryName[directoryName.Length - 1] = x.Get(CATEGORY);
                string newDirectory = "";
                for (int i = 0; i < directoryName.Length; i++)
                {
                    newDirectory += directoryName[i] + "\\";
                }

                Console.WriteLine(file.DirectoryName + "\\" + x.Get(CATEGORY)
                 + MusicAPIHelper.IncluirCeros(x.Get(NUMBER), 3)
                 + " - " + x.Get(TITLE) + " - " + x.Get(GROUP) + ".mp3");
                System.IO.File.Move(file.FullName, newDirectory + "\\" + x.Get(CATEGORY)
                 + MusicAPIHelper.IncluirCeros(x.Get(NUMBER), 3)
                 + " - " + x.Get(TITLE) + " - " + x.Get(GROUP) + ".mp3");
                x[STATE] = DataRowState.Unchanged;
            });
        }

        public MusicView ObtenerMusicaFormatView(string path)
        {
            DataRow dr = ObtenerMusica(path);
            return new MusicView(dr, (dr.Get(CATEGORY) != CATUKN) ? MusicRowState.correct: MusicRowState.incorrect);
        }
    }
}
