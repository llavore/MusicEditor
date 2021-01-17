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
        void LoadMusic();
        DataTable ObtenerTodos();
        DataView ObtenerTodosCorrectos();
        DataView ObtenerTodosIncorrectos();
        List<String> ObtenerGrupos();
        List<String> ObtenerCategorias();
        DataRow ObtenerMusica(string path);
        DataRow ObtenerMusica(string categoria, int numero);
        List<DataRow> SeleccionarMusicaAPartirDeUnNumero(string categoria, int numero);
        int totalMusicaCategoria(string categoria);

        DataRow NuevaLinea();

        int totalMusica();
        List<String[]> CantidadMusicaCategoria();

        void ModificarMusica(string[] liniaModificada);
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
        private DataTable _table;
        private String _path;

        public MusicAPI(string path)
        {
            _table = new DataTable();

            DataColumn[] columnKey = new DataColumn[2];
            DataColumn keyNumber= new DataColumn();
            keyNumber.DataType = System.Type.GetType("System.Int32"); 
            keyNumber.ColumnName = "Number";

            DataColumn keyCategory = new DataColumn();
            keyCategory.ColumnName = "Category";

            _table.Columns.Add(keyCategory);
            _table.Columns.Add(keyNumber);
            _table.Columns.Add("Title");
            _table.Columns.Add("Group");
            _table.Columns.Add("State");
            _table.Columns.Add("Path");

            columnKey[0] = keyCategory;
            columnKey[1] = keyNumber;

            _table.PrimaryKey = columnKey;
            _path = path;
            LoadMusic();
        }

        /*public void LoadMusic()
        {
            var allFiles = Directory.GetFiles(_path, "*.mp3", SearchOption.AllDirectories);
            int countMusicaIncorrecta = 1;
            if (allFiles.Length > 0)
            {
                var musicList = allFiles.ToList();
                FileInfo file;
                Regex r = new Regex(@"([A-Z]{3}[0-9]{3}([ ])-([ ])([\S, ]+)([ ])-([ ])([\S, ]+).mp3)");
                foreach (var path in musicList)
                {
                    file = new FileInfo(path);
                    DataRow row;
                    //Console.WriteLine(file.Name +( r.IsMatch(file.Name) ? " correcte" : " incorrecte"));

                    //String[] items = new String[6];
                    //items[(int)MusicColumns.Path] = file.FullName;

                    MusicView musicFile;

                    if (r.IsMatch(file.Name))
                    {
                        

                        var NameSeparate = file.Name.Split('-');
                        musicFile = new MusicView(NameSeparate[0].Substring(0, 3).Trim());

                        items[(int)MusicColumns.Category] = NameSeparate[0].Substring(0, 3).Trim();
                        items[(int)MusicColumns.Group] = NameSeparate[2].Split('.')[0].Trim();
                        items[(int)MusicColumns.Number] = NameSeparate[0].Substring(3, 4).Trim();
                        items[(int)MusicColumns.Title] = NameSeparate[1].Trim().Trim();
                        items[(int)MusicColumns.State] = Boolean.TrueString;

                    }
                    else
                    {
                        items[(int)MusicColumns.Category] = "---";
                        items[(int)MusicColumns.Group] = "---";
                        items[(int)MusicColumns.Number] = countMusicaIncorrecta.ToString();
                        items[(int)MusicColumns.Title] = "---";
                        items[(int)MusicColumns.State] = Boolean.FalseString;
                        countMusicaIncorrecta++;
                    }
                    row = _table.NewRow();
                    row.ItemArray = items;
                    _table.Rows.Add(row);
                }


            }

        }*/

        public void LoadMusic()
        {
            var allFiles = Directory.GetFiles(_path, "*.mp3", SearchOption.AllDirectories);
            int countMusicaIncorrecta = 1;
            if (allFiles.Length > 0)
            {
                var musicList = allFiles.ToList();


                FileInfo file;
                Regex r = new Regex(@"([A-Z]{3}[0-9]{3}([ ])-([ ])([\S, ]+)([ ])-([ ])([\S, ]+).mp3)");
                foreach (var path in musicList)
                {
                    file = new FileInfo(path);
                    DataRow row;
                    //Console.WriteLine(file.Name +( r.IsMatch(file.Name) ? " correcte" : " incorrecte"));

                    String[] items = new String[6];
                    items[(int)MusicColumns.Path] = file.FullName;

                    if (r.IsMatch(file.Name))
                    {


                        var NameSeparate = file.Name.Split('-');
                        items[(int)MusicColumns.Category] = NameSeparate[0].Substring(0, 3).Trim();
                        items[(int)MusicColumns.Group] = NameSeparate[2].Split('.')[0].Trim();
                        items[(int)MusicColumns.Number] = NameSeparate[0].Substring(3, 4).Trim();
                        items[(int)MusicColumns.Title] = NameSeparate[1].Trim().Trim();
                        items[(int)MusicColumns.State] = Boolean.TrueString;

                    }
                    else
                    {
                        items[(int)MusicColumns.Category] = "unkown";
                        items[(int)MusicColumns.Group] = "---";
                        items[(int)MusicColumns.Number] = countMusicaIncorrecta.ToString();
                        items[(int)MusicColumns.Title] = "---";
                        items[(int)MusicColumns.State] = Boolean.FalseString;
                        countMusicaIncorrecta++;
                    }
                    row = _table.NewRow();
                    row.ItemArray = items;
                    _table.Rows.Add(row);
                }


            }

        }



        public List<string> ObtenerGrupos()
        {

            List<string> grupos = _table.AsEnumerable().Where(x => x.ItemArray[(int)MusicColumns.State].ToString() == Boolean.TrueString)
                                  .Select(x => x.ItemArray[(int)MusicColumns.Group].ToString()).Distinct().ToList();
            return grupos;
        }

        public DataTable ObtenerTodos()
        {
            return _table;
        }

        public DataView ObtenerTodosCorrectos()
        {
            DataView dv = new DataView(_table);
            dv.RowFilter = "State = " + Boolean.TrueString;
            dv.Sort = "Category, Number ";
            return dv;
        }

        public DataView ObtenerTodosIncorrectos()
        {
            DataView dv = new DataView(_table);
            dv.RowFilter = "State = " + Boolean.FalseString;
            dv.Sort = "Category, Number ";
            return dv;
        }

        public int totalMusica()
        {
            return _table.AsEnumerable().Count();
        }

        public List<String[]> CantidadMusicaCategoria()
        {
            var list = _table.AsEnumerable().GroupBy(x => x.ItemArray[(int)MusicColumns.Category])
                .Select(x => new String[2] { x.Key.ToString(), x.ToList().Count().ToString() }).ToList();
            return list;
        }

        public DataRow ObtenerMusica(string path)
        {
            DataRow rowSelected = _table.AsEnumerable().Where(x => x.ItemArray[(int)MusicColumns.Path] == path)
                                    .FirstOrDefault();

            return rowSelected;
        }

        public DataRow ObtenerMusica(string categoria, int numero)
        {
            /* DataRow rowSelected = _table.AsEnumerable()
                                     .Where(x => x.ItemArray[(int)MusicColumns.Category] == categoria &&
                                      Int32.Parse(x.ItemArray[(int)MusicColumns.Number].ToString()) == 33)
                                     .FirstOrDefault();*/

            DataRow rowSelected = _table.AsEnumerable()
                                    .Where(x => x.ItemArray[0].ToString() == categoria &&
                                     Int32.Parse(x.ItemArray[1].ToString()) == numero)
                                    .FirstOrDefault();

            return rowSelected;
        }

        public List<string> ObtenerCategorias()
        {
            List<string> categorias = _table.AsEnumerable().Where(x => x.ItemArray[(int)MusicColumns.State].ToString() == Boolean.TrueString)
                                  .Select(x => x.ItemArray[(int)MusicColumns.Category].ToString()).Distinct().ToList();
            return categorias;
        }

        public int totalMusicaCategoria(string categoria)
        {
            var list = _table.AsEnumerable().Where(x => x.ItemArray[(int)MusicColumns.Category].ToString() == categoria)
                .Count();
            return list;
        }

        public void ModificarMusica(string[] liniaModificada)
        {
            bool state = Boolean.Parse(liniaModificada[(int)MusicColumns.State].ToString());
            if (state) {
                DataRow lineaOriginal = ObtenerMusica(liniaModificada[(int)MusicColumns.Path].ToString());
                DataRow lineaExistente = ObtenerMusica(liniaModificada[(int)MusicColumns.Category].ToString(),
                   Int32.Parse(liniaModificada[(int)MusicColumns.Number].ToString()));
                if (!Boolean.Parse(lineaOriginal[(int)MusicColumns.State].ToString()) && lineaExistente == null)
                {
                    lineaOriginal.ItemArray = liniaModificada;
                }
                else if (!Boolean.Parse(lineaOriginal[(int)MusicColumns.State].ToString()) && lineaExistente != null)
                {
                   
                    List<DataRow> listaMod = SeleccionarMusicaAPartirDeUnNumero(liniaModificada[(int)MusicColumns.Category].ToString(), Int32.Parse(liniaModificada[(int)MusicColumns.Number].ToString()));
                    listaMod.ForEach(
                        x => {
                            //Console.WriteLine(x.ItemArray[(int)MusicColumns.Category].ToString()+ " "+ x.ItemArray[(int)MusicColumns.Number].ToString());
                            var array = x.ItemArray.ToList();
                            array[1] = Int32.Parse(x.ItemArray[1].ToString()) + 1 ;
                            x.ItemArray = array.ToArray();
                        });
                    lineaExistente = null;
                    lineaOriginal.ItemArray = liniaModificada;
                }
            }
        }

        public DataRow NuevaLinea()
        {
            
            return _table.NewRow();
        }

        public  List<DataRow> SeleccionarMusicaAPartirDeUnNumero(string categoria, int numero)
        {
            DataView dv = _table.DefaultView;
            dv.Sort = "Category, Number ";
            _table = dv.ToTable();

            var rowSelected = _table.AsEnumerable()
                                      .Where(x => x.ItemArray[0].ToString() == categoria &&
                                       Int32.Parse(x.ItemArray[1].ToString()) >= numero)
                                      .Reverse().ToList();
           
            return rowSelected;
        }
    }
}
