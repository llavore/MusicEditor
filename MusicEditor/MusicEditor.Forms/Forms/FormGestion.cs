using MusicEditor.Bussines.APIs;
using MusicEditor.Forms.Helpers;
using MusicEditor.Helpers;
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
    public partial class FormGestion : Form
    {
        private string _path;
        private IMusicApi _api;

        public FormGestion(string path)
        {
            InitializeComponent();
            this.Text = Nombres.FormGestion;
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
                MessageHelper.InfoMessage(Mensajes.SinFicherosMusica);

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

               bool result = MessageHelper.QuestionMessage(Mensajes.ConfirmacionSalirSinGuardar);
                if (result && dialog.ShowDialog() == DialogResult.OK)
                {
                    _path = dialog.SelectedPath;
                    _api = new MusicAPI(_path);
                    configuracionInicial();
                }
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            MessageHelper.InfoMessage(Mensajes.FuncionNoImplementada);
        }
    }
}
