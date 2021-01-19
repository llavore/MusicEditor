using MusicEditor.Bussines.Helpers;
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
        int totalMusicaCategoria(string categoria);

        int hasChange();

        DataRow NuevaLinea();

        int totalMusica();
        List<String[]> CantidadMusicaCategoria();
        int CantidadMusicaDeUnaCategoria(string cat);

        void ModificarMusica(Object[] liniaModificada);

        void SaveAll();
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

    public enum FileState
    {
        Original = 0,
        Modified = 1,
       
    }

    public class MusicAPI : IMusicApi
    {
       
        private DataSet ds;
        private DataTable _table;
        private String _path;
        public static readonly String CategoryUnkown = "UKN";

        public MusicAPI(string path)
        {

            ds = new DataSet();
            _table = new DataTable();

            ds.Tables.Add(_table);


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

            //_table.PrimaryKey = columnKey;
            _path = path;
            LoadMusic();
        }

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
                    items[(int)MusicColumns.State] = DataRowState.Unchanged.ToString();
                    if (r.IsMatch(file.Name))
                    {
                        var NameSeparate = file.Name.Split('-');
                        items[(int)MusicColumns.Category] = NameSeparate[0].Substring(0, 3).Trim();
                        items[(int)MusicColumns.Group] = NameSeparate[2].Split('.')[0].Trim();
                        items[(int)MusicColumns.Number] = NameSeparate[0].Substring(3, 4).Trim();
                        items[(int)MusicColumns.Title] = NameSeparate[1].Trim().Trim();
                    }
                    else
                    {
                        items[(int)MusicColumns.Category] = CategoryUnkown;
                        items[(int)MusicColumns.Group] = "---";
                        items[(int)MusicColumns.Number] = countMusicaIncorrecta.ToString();
                        items[(int)MusicColumns.Title] = "---";
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

            List<string> grupos = _table.AsEnumerable().Where(x => x.ItemArray[(int)MusicColumns.Category].ToString() != CategoryUnkown)
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
            dv.RowFilter = "Category <> '" + CategoryUnkown+"'";
            dv.Sort = "Category, Number ";
            return dv;
        }

        public DataView ObtenerTodosIncorrectos()
        {
            DataView dv = new DataView(_table);
            dv.RowFilter = "Category = '" + CategoryUnkown + "'";
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

            DataRow rowSelected = _table.AsEnumerable()
                                    .Where(x => x.ItemArray[0].ToString() == categoria &&
                                     Int32.Parse(x.ItemArray[1].ToString()) == numero)
                                    .FirstOrDefault();

            return rowSelected;
        }

        public List<string> ObtenerCategorias()
        {
            List<string> categorias = _table.AsEnumerable().Where( x=> x.ItemArray[(int)MusicColumns.Category].ToString() != CategoryUnkown)
                .Select(x => x.ItemArray[(int)MusicColumns.Category].ToString()).Distinct().ToList();
            return categorias;
        }

        public int totalMusicaCategoria(string categoria)
        {
            var list = _table.AsEnumerable().Where(x => x.ItemArray[(int)MusicColumns.Category].ToString() == categoria)
                .Count();
            return list;
        }
        private void ModificarLinea(Object[] liniaModificada)
        {
            DataRow nueva = NuevaLinea();
            var array = nueva.ItemArray.ToArray();
            array[0] = liniaModificada[0];
            array[1] = liniaModificada[1];
            array[2] = liniaModificada[2];
            array[3] = liniaModificada[3];
            array[4] = DataRowState.Modified.ToString();
            array[5] = liniaModificada[4];
            nueva.ItemArray = array;
            _table.Rows.Remove(ObtenerMusica(liniaModificada[(int)MusicColumns.Path-1].ToString()));
            _table.Rows.Add(nueva);
        }
        public void ModificarMusica(Object[] liniaModificada)
        {
            
            string state = liniaModificada[(int)MusicColumns.Category].ToString();
            if (state != CategoryUnkown) {
                DataRow lineaOrigen = ObtenerMusica(liniaModificada[(int)MusicColumns.Path-1].ToString());
                DataRow lineaFinal = ObtenerMusica(liniaModificada[(int)MusicColumns.Category].ToString(),
                Int32.Parse(liniaModificada[(int)MusicColumns.Number].ToString()));

                var categoriaOrigen = lineaOrigen[(int)MusicColumns.Category].ToString();
                
                var numeroOrigen = Int32.Parse(lineaOrigen[(int)MusicColumns.Number].ToString());

                string categoriaFinal = null;
                var numeroFinal = -1;
                if (lineaFinal != null) { 
                categoriaFinal = lineaFinal[(int)MusicColumns.Category].ToString();
                numeroFinal = Int32.Parse(lineaFinal[(int)MusicColumns.Number].ToString());
                }
                Console.WriteLine(" ");
                Console.WriteLine("****inicio del proceso****");

                
                if (lineaFinal == null)
                {
                    Console.WriteLine("Actualizar Origen");
                    ModificarLinea(liniaModificada);
                    if (categoriaOrigen != CategoryUnkown)
                    {
                        Console.WriteLine("Las lineas siguientes de:{{CAT: "+ categoriaOrigen+" }{NUM: "+ numeroOrigen+" }} "
                                            + " han sido actualizadas como number -1");
                        ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen + 1, CantidadMusicaDeUnaCategoria(categoriaOrigen),-1);
                    }

                }
                else if (categoriaOrigen == categoriaFinal)
                {
                    if (numeroOrigen > numeroFinal)
                    {
                        Console.WriteLine("Lineas entre {{CAT:" + categoriaOrigen + "}{NUM:" + numeroOrigen + "}}  i {{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} "
                            + "incluida: {{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} se ha actualizado el numero en number+1");
                        ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen + 1, numeroFinal,+1);
                        Console.WriteLine("Actualizar Origen");
                    }
                    if (numeroOrigen < numeroFinal)
                    {
                        Console.WriteLine("Lineas entre {{CAT:" + categoriaOrigen + "}{NUM:" + numeroOrigen + "}}  i {{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} "
                            + "sin incluir ambas se ha actualizado el numero en number-1");
                        ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen + 1, numeroFinal-1,-1);
                        Console.WriteLine("Actualizar Origen");  
                    }
                    ModificarLinea(liniaModificada);
                }
                else if (categoriaOrigen != categoriaFinal) 
                {
                    Console.WriteLine("Las lineas siguientes de:{{CAT:" + categoriaFinal + "}{NUM:" + numeroFinal + "}} "
                                            + " han sido actualizadas como number +1");
                    ChangeNumberMusicaAPartirDeDosFilas(categoriaFinal, numeroFinal, CantidadMusicaDeUnaCategoria(categoriaFinal),+1);
                    Console.WriteLine("Las lineas siguientes de:{{CAT:" + categoriaOrigen + "}{NUM:" + numeroOrigen + "}} "
                                            + " han sido actualizadas como number -1");
                    ChangeNumberMusicaAPartirDeDosFilas(categoriaOrigen, numeroOrigen + 1, CantidadMusicaDeUnaCategoria(categoriaOrigen),-1);
                    Console.WriteLine("Actualizar Origen");
                    ModificarLinea(liniaModificada);
                }
                Console.WriteLine("****Final proceso****");

                /*if (!Boolean.Parse(lineaOrigen[(int)MusicColumns.State].ToString()) && lineaFinal == null)
                {
                    lineaOrigen.ItemArray = liniaModificada.ToArray();
                }
                else if (!Boolean.Parse(lineaOrigen[(int)MusicColumns.State].ToString()) && lineaFinal != null)
                {
                    List<DataRow> listaMod = SeleccionarMusicaAPartirDeUnNumeroHaciaDelante(liniaModificada[(int)MusicColumns.Category].ToString(), 
                                                                                            Int32.Parse(liniaModificada[(int)MusicColumns.Number].ToString()),
                                                                                            CantidadMusicaDeUnaCategoria(liniaModificada[(int)MusicColumns.Category].ToString()));
                    listaMod.ForEach(x => { x[1] = Int32.Parse(x.ItemArray[1].ToString()) + 1 ;});
                    
                }*/
            }
        }

        public DataRow NuevaLinea()
        {
            
            return _table.NewRow();
        }

        public  List<DataRow> ChangeNumberMusicaAPartirDeDosFilas(string categoria, int numero1, int numero2,int value)
        {
            DataView dv = _table.DefaultView;
            dv.Sort = "Category, Number";
            _table = dv.ToTable();

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
            var listado = CantidadMusicaCategoria();
            int cantidadCatBuscada = 0;
            listado.ForEach(
                x => {
                    if (x[0] == cat) {
                        cantidadCatBuscada = Int32.Parse(x[1]);
                    }
                });
            return cantidadCatBuscada;
        }

        public int hasChange()
        {
            return _table.AsEnumerable().Where(x => x["State"].ToString() == DataRowState.Modified.ToString()).Count();
        }

        public void SaveAll()
        {
            FileInfo file;
            var list = _table.AsEnumerable().Where(x => x["State"].ToString() == DataRowState.Modified.ToString()).ToList();
            list.ForEach(x => {
                file = new FileInfo(x["Path"].ToString());
                Console.WriteLine(file.DirectoryName);
                Console.WriteLine(file.DirectoryName + "\\" + x["Category"].ToString()
                 + MusicHelper.IncluirCeros(x["Number"].ToString(), 999)
                 + " - " + x["Title"].ToString() + " - " + x["Group"].ToString() + ".mp3");
                System.IO.File.Move(file.FullName, file.DirectoryName + "\\" + x["Category"].ToString()
                 + MusicHelper.IncluirCeros(x["Number"].ToString(), 999)
                 + " - " + x["Title"].ToString() + " - " + x["Group"].ToString()+".mp3");
            });

        }
    }
}
