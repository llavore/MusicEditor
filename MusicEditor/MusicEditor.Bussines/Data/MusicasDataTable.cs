using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Data
{
    public class MusicasDataTable : DataTable
    {
        /*public static readonly int CATEGORY = 0;
        public static readonly int NUMBER = 1;
        public static readonly int TITLE = 2;
        public static readonly int GROUP = 3;
        public static readonly int STATE = 4;
        public static readonly int PATH = 5;*/

        public static readonly string CATEGORY = "Category";
        public static readonly string NUMBER = "Number";
        public static readonly string TITLE = "Title";
        public static readonly string GROUP = "Group";
        public static readonly string STATE = "State";
        public static readonly string PATH = "Path";


        protected int positionDb;

        public MusicasDataTable() {
            
            this.TableName = "MusicasCorrectas";
            
            DataColumn[] columnKey = new DataColumn[2];
            DataColumn keyPath = new DataColumn();
            keyPath.ColumnName = PATH;

            DataColumn Number = new DataColumn();
            Number.DataType = System.Type.GetType("System.Int32");
            Number.ColumnName = NUMBER;

            Columns.Add(CATEGORY);
            Columns.Add(Number);
            Columns.Add(TITLE);
            Columns.Add(GROUP);
            Columns.Add(STATE);
            Columns.Add(keyPath);

            columnKey[0] = keyPath;
            
            PrimaryKey = columnKey;
        }

    }
}
