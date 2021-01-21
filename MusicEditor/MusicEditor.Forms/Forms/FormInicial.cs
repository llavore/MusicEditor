
using MusicEditor.Ressources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicEditor.Forms
{
    public partial class FormInicial : Form
    {
        public FormInicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog()) {
                if (dialog.ShowDialog() == DialogResult.OK) {
                    String SelectedPath = dialog.SelectedPath;
                    AplicationContext.AplicationContext.LoadData(SelectedPath);
                    var formGestion = new FormGestion();
                    formGestion.ShowDialog();
                }
            }
        }

        private void FormInicial_Load(object sender, EventArgs e)
        {
            this.Text = Nombres.FormInicial;
        }
    }
}
