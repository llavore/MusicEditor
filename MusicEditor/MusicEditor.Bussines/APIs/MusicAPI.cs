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

    public interface IMusicApi{
        void LoadMusic();
        DataTable ObtenerTodos();
        DataView ObtenerTodosCorrectos();
        DataView ObtenerTodosIncorrectos();
        List<String> ObtenerGrupos();

        int totalMusica();
    }

    public enum MusicColumns { 
    
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
            _table.Columns.Add("Category");
            _table.Columns.Add("Number");
            _table.Columns.Add("Title");
            _table.Columns.Add("Group");
            _table.Columns.Add("State");
            _table.Columns.Add("Path");
            _path = path;
            LoadMusic();
        }

        public void LoadMusic()
        {
            var allFiles = Directory.GetFiles(_path, "*.mp3",SearchOption.AllDirectories);
            if (allFiles.Length > 0)
            {
                var enumerator = allFiles.GetEnumerator();
                enumerator.MoveNext();
                var current = enumerator.Current;
                FileInfo file;
                Regex r = new Regex(@"([A-Z]{3}[0-9]{3}([ ])-([ ])([\S, ]+)([ ])-([ ])([\S, ]+).mp3)");
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    file = new FileInfo(current.ToString());
                    DataRow row;
                    //Console.WriteLine(file.Name +( r.IsMatch(file.Name) ? " correcte" : " incorrecte"));
                    String[] items = new String[6];

                    items[(int)MusicColumns.Path] = file.FullName;

                    if (r.IsMatch(file.Name))
                    {
                        var NameSeparate = file.Name.Split('-');
                        items[(int)MusicColumns.Category] = NameSeparate[0].Substring(0, 3);
                        items[(int)MusicColumns.Group] = NameSeparate[2].Split('.')[0].Trim();
                        items[(int)MusicColumns.Number] = NameSeparate[0].Substring(3, 4);
                        items[(int)MusicColumns.Title] = NameSeparate[1].Trim();
                        items[(int)MusicColumns.State] = Boolean.TrueString;

                    }
                    else
                    {

                        items[(int)MusicColumns.Category] = "---";
                        items[(int)MusicColumns.Group] = "---";
                        items[(int)MusicColumns.Number] = "---";
                        items[(int)MusicColumns.Title] = "---";
                        items[(int)MusicColumns.State] = Boolean.FalseString;


                    }
                    row = _table.NewRow();
                    row.ItemArray = items;
                    _table.Rows.Add(row);
                }
            }
        }

        public List<string> ObtenerGrupos()
        {
            
           List<string> grupos = _table.AsEnumerable().Where(x=> x.ItemArray[(int) MusicColumns.State].ToString() == Boolean.TrueString)
                                 .Select(x =>x.ItemArray[(int)MusicColumns.Group].ToString()).Distinct().ToList();
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

            return dv;
        }

        public DataView ObtenerTodosIncorrectos()
        {
            DataView dv = new DataView(_table);
            dv.RowFilter = "State = " + Boolean.FalseString;
            return dv;
        }

        public int totalMusica()
        {
            return _table.AsEnumerable().Count();
        }
    }
}
