using MusicEditor.Bussines.Helpers;
using MusicEditor.Bussines.APIs;
using MusicEditor.Bussines.Data;
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
        public enum MusicRowState { 
            correct = 0,
            incorrect = 1
        }
        
        public int number { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string group { get; set; }
        public MusicRowState state { get; set; }
        public string path { get; set; }
        
        public MusicView(DataRow row, MusicRowState state)
        {
            path = row.Get(MusicasDataTable.PATH);
            category = row.Get(MusicasDataTable.CATEGORY);
            number = row.Value(MusicasDataTable.NUMBER);
            this.state = state;
            if (state is MusicRowState.correct)
            {
                title = row.Get(MusicasDataTable.TITLE);
                group = row.Get(MusicasDataTable.GROUP);
            }
            else
            {
                title = null;
                group = null;
            }
        }

        public bool isCorrecte()
        {
            return (state is MusicRowState.correct) ? true : false;
        }
    }
}
