using MusicEditor.Bussines.APIs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicEditor.Bussines.Helpers
{
    static class MusicAPIHelper
    {
        public static string IncluirCeros(  String num, int quantitatXifres)
        {
            if (num.Length < quantitatXifres)
            {
                num = IncluirCeros("0" + num, quantitatXifres);
            }
            return num;
        }

       

    }
}