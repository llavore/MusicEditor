
using MusicEditor.Ressources;
using Ninject;
using Ninject.Parameters;
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
                    String selectedPath = dialog.SelectedPath;
                    using (var form = Program.Ninject.Get<FormGestion>(new ConstructorArgument("path", selectedPath))) {
                        form.ShowDialog();
                        
                    }
                }
            }
        }

        private void FormInicial_Load(object sender, EventArgs e)
        {
            this.Text = Nombres.FormInicial;
        }
    }
}
