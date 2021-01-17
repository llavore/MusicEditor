
using MusicEditor.Bussines.APIs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Forms.Models
{
    // Use only in FormMusic
    public class MusicView
    {
        public int number { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string group { get; set; }
        public bool state { get; set; }
        public string path { get; set; }
        private string incluirCeros(String num, int quantitat )
        {
            if (num.Length < quantitat)
            {
                num = incluirCeros("0" + num, quantitat);
            }
            return num;
        }

        public MusicView(DataRow row)
        {
            path = row.ItemArray[(int)MusicColumns.Path].ToString();
            state = Boolean.Parse(row.ItemArray[(int)MusicColumns.State].ToString());
            if (state)
            {
                category = row.ItemArray[(int)MusicColumns.Category].ToString();
                number = Int16.Parse(row.ItemArray[(int)MusicColumns.Number].ToString());
                title = row.ItemArray[(int)MusicColumns.Title].ToString();
                group = row.ItemArray[(int)MusicColumns.Group].ToString();
            }
            else
            {
                category = null;
                number = -1;
                title = null;
                group = null;
            }
        }

        public string[] toArray() {
            string[] array = new string[6];
            array[0] = (category != null) ? category : "---";
            array[1] = number.ToString() ;
            array[2] = (title != null ) ? title : "---"; 
            array[3] = (group != null) ? group : "---";
            array[4] = Boolean.TrueString;
            array[5] = path;
            return array;
        }
    }
}
