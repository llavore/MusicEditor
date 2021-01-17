using MusicEditor.Bussines.APIs;
using MusicEditor.Bussines.Models;
using MusicEditor.Forms.Forms;
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
        private Boolean _activated;

        public FormGestion(string path, MusicAPI api)
        {
            InitializeComponent();
            this.Text = Nombres.FormGestion;
            _path = path;
            _api = api;
            _activated = false;

        }
        private void configuracionInicial() {
            txtPathFolder.Text = _path;

            if(_api.totalMusica() > 0) { 
               
                gridMusicaCorrecta.LoadData<Music>(_api.ObtenerTodosCorrectos());
                gridMusicaCorrecta.GridStyleMusicaCorrecta();

                gridMusicaIncorrecta.LoadData<Music>(_api.ObtenerTodosIncorrectos());
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
                    configuracionInicial();
                }
            }
        }

        private void gridMusicaIncorrecta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewButtonCell btn = (DataGridViewButtonCell)gridMusicaIncorrecta.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (btn is DataGridViewButtonCell)
            {
                if (btn.Value.ToString() == "Modificar")
                {
                    //int indexColumn = gridMusicaIncorrecta.Columns.
                    string path = "";
                    DataRow dr = _api.ObtenerMusica(path);
                    FormMusic formMusic = new FormMusic(dr, _api);
                    formMusic.ShowDialog();
                }

                if (btn.Value.ToString() == "Eliminar")
                {
                    MessageHelper.InfoMessage(Mensajes.FuncionNoImplementada);
                }

            }
        }

        private void gridMusicaCorrecta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell btn = gridMusicaCorrecta.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (btn is DataGridViewButtonCell)
            {
                if (btn.Value.ToString() == "Modificar") 
                {
                    string path = gridMusicaCorrecta.Rows[e.RowIndex].Cells[5].Value.ToString();
                    OpenFormMusic(path);
                }
                else if (btn.Value.ToString() == "Eliminar")
                {
                    MessageHelper.InfoMessage(Mensajes.FuncionNoImplementada);
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageHelper.InfoMessage(Mensajes.FuncionNoImplementada);
        }

        private void gridMusicaIncorrecta_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewCell btn = gridMusicaIncorrecta.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (btn is DataGridViewButtonCell)
            {
                if (btn.Value.ToString() == "Modificar")
                {
                    string path = gridMusicaIncorrecta.Rows[e.RowIndex].Cells[7].Value.ToString();
                    OpenFormMusic(path);

                }
                else if(btn.Value.ToString() == "Eliminar")
                {
                    MessageHelper.InfoMessage(Mensajes.FuncionNoImplementada);
                }

            }
        }

        private void FormGestion_Activated(object sender, EventArgs e)
        {
            if (_activated)
            {
                gridMusicaCorrecta.LoadData(_api.ObtenerTodosCorrectos());
                gridMusicaCorrecta.GridStyleMusicaCorrecta();

                gridMusicaIncorrecta.LoadData(_api.ObtenerTodosIncorrectos());
                gridMusicaIncorrecta.GridStyleMusicaIncorrecta();

                _activated = false;
            }
        }

        private void OpenFormMusic(string path) {
            _activated = true;

            DataRow dr = _api.ObtenerMusica(path);
            FormMusic formMusic = new FormMusic(dr, _api);
            formMusic.ShowDialog();
        }
    }
}
