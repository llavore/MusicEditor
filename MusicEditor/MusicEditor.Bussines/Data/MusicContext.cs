using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Data
{
    public class MusicContext : DataSet
    {
        public DataSet Music;
        private string _selectedPath;
        public static readonly string CategoryUnkown = "UKN";

        public virtual MusicasDataTable Musicas { get; set; }

        public MusicContext(String selectedPath)
        {
            this.DataSetName = "Music";
            Musicas = new MusicasDataTable();
            this.Tables.Add(Musicas);
            _selectedPath = selectedPath;
            LoadData();
        }

        private void LoadData() {
            var allFiles = Directory.GetFiles(_selectedPath, "*.mp3", SearchOption.AllDirectories);
            int countMusicaIncorrecta = 1;
            if (allFiles.Length > 0)
            {
                var musicList = allFiles.ToList();
                FileInfo file;
                Regex r = new Regex(@"([A-Z]{3}[0-9]{3}([ ])-([ ])([\S, ]+)([ ])-([ ])([\S, ]+).mp3)");
                foreach (var path in musicList)
                {
                    file = new FileInfo(path);
                    
                    //Console.WriteLine(file.Name +( r.IsMatch(file.Name) ? " correcte" : " incorrecte"));

                    var items = Musicas.NewRow(); 
                    items[MusicasDataTable.PATH] = file.FullName;
                    items[MusicasDataTable.STATE] = DataRowState.Unchanged.ToString();
                    if (r.IsMatch(file.Name))
                    {
                        var NameSeparate = file.Name.Split('-');
                        items[MusicasDataTable.CATEGORY] = NameSeparate[0].Substring(0, 3).Trim();
                        items[MusicasDataTable.GROUP] = NameSeparate[2].Split('.')[0].Trim();
                        items[MusicasDataTable.NUMBER] = NameSeparate[0].Substring(3, 4).Trim();
                        items[MusicasDataTable.TITLE] = NameSeparate[1].Trim().Trim();
                    }
                    else
                    {
                        items[MusicasDataTable.CATEGORY] = CategoryUnkown;
                        items[MusicasDataTable.GROUP] = "---";
                        items[MusicasDataTable.NUMBER] = countMusicaIncorrecta.ToString();
                        items[MusicasDataTable.TITLE] = "---";
                        countMusicaIncorrecta++;
                    }
                    Musicas.Rows.Add(items);
                }
            }

        }

    }
}
