using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicEditor.Forms.Helpers
{
    public static class ComboBoxExtensions
    {
        public static ComboBox CargarCombo(this ComboBox cmb, List<string> lista, string selected)
        {
            if (lista != null) {
                if (lista.Count>0) {
                    cmb.Items.AddRange(lista.ToArray());
                    cmb.SelectedItem = selected;
                }
            }
            return cmb;

        }
    }
}
