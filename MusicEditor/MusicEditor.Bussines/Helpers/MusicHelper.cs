using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicEditor.Bussines.Helpers
{
    class MusicHelper
    {
        public static string IncluirCeros(String num, int quantitat)
        {
            if (num.Length < quantitat)
            {
                num = IncluirCeros("0" + num, quantitat);
            }
            return num;
        }

        
    }
}