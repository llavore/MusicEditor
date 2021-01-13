using MusicEditor.Bussines.APIs;
using MusicEditor.Helpers;
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
    public partial class FormGestion : Form
    {
        private string _path;
        private IMusicApi _api;

        public FormGestion(string path)
        {
            InitializeComponent();
            _path = path;
            _api = new MusicAPI(_path);
        }
        private void configuracionInicial() {
            txtPathFolder.Text = _path;

            if(_api.totalMusica() > 0) { 
                gridMusicaCorrecta.LoadData(_api.ObtenerTodosCorrectos());
                gridMusicaCorrecta.GridStyleMusicaCorrecta();

                gridMusicaIncorrecta.LoadData(_api.ObtenerTodosIncorrectos());
                gridMusicaIncorrecta.GridStyleMusicaIncorrecta();

                
            }
            else
            {
                gridMusicaCorrecta.ClearColumns().DataSource = null;
                gridMusicaIncorrecta.ClearColumns().DataSource = null;
                MessageHelper.InfoMessage("Lo sentimos pero no hemos encontrado canciones para mostrar.");

            }

        }
        private void FormGestion_Load(object sender, EventArgs e)
        {
            configuracionInicial();
        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {

               bool result = MessageHelper.QuestionMessage("Seguro que quieres cambiar de carpeta? Los datos modificados actuales de perderan");
                if (result && dialog.ShowDialog() == DialogResult.OK)
                {
                    _path = dialog.SelectedPath;
                    _api = new MusicAPI(_path);
                    configuracionInicial();
                }
            }
        }
    }
}
