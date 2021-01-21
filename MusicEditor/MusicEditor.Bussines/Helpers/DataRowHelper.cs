using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Bussines.Helpers
{
    static class DataRowHelper
    {
        public static string Get(this DataRow row, string column) {
            return row[column].ToString();
        }
        public static int Value(this DataRow row, string column)
        {
            return Int32.Parse(row[column].ToString());
        }

    }
}
