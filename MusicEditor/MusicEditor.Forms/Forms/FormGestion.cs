using MusicEditor.Bussines.APIs;
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

        public FormGestion()
        {
            InitializeComponent();
            this.Text = Nombres.FormGestion;
            _api = new MusicAPI(AplicationContext.AplicationContext._repositoryManager);
            LabelModificacies.Text = "Cambios realizados: " + _api.hasChange();
            _activated = false;
        }

        private void configuracionInicial() {
            txtPathFolder.Text = _path;

            if(_api.totalMusica() > 0) { 
               
                gridMusicaCorrecta.LoadData(_api.ObtenerTodosCorrectos());
                gridMusicaCorrecta.GridStyleMusicaCorrecta();

                gridMusicaIncorrecta.LoadData(_api.ObtenerTodosIncorrectos());
                gridMusicaIncorrecta.GridStyleMusicaIncorrecta();
                LabelModificacies.Text = "Cambios realizados: " + _api.hasChange();
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
                    AplicationContext.AplicationContext.LoadData(_path);
                    _api = new MusicAPI(AplicationContext.AplicationContext._repositoryManager);
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
                    int columnPath = gridMusicaCorrecta.Columns["Path"].Index;
                    string path = gridMusicaCorrecta.Rows[e.RowIndex].Cells[columnPath].Value.ToString();
                    OpenFormMusic(path);
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
                    int columnPath = gridMusicaCorrecta.Columns["Path"].Index;
                    string path = gridMusicaCorrecta.Rows[e.RowIndex].Cells[columnPath].Value.ToString();
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
            //MessageHelper.InfoMessage(Mensajes.FuncionNoImplementada);
            try
            {
                _api.SaveAll();
                LabelModificacies.Text = "Cambios realizados: " + _api.hasChange();
                MessageHelper.InfoMessage(Mensajes.ModificacionesGuardadas);
            }
            catch (Exception t)
            {
                LabelModificacies.Text = "Cambios realizados: " + _api.hasChange();
                MessageHelper.InfoMessage(t.Message);
            }
        }

        private void gridMusicaIncorrecta_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewCell btn = gridMusicaIncorrecta.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (btn is DataGridViewButtonCell)
            {
                if (btn.Value.ToString() == "Modificar")
                {
                    int columnPath = gridMusicaIncorrecta.Columns["Path"].Index;
                    string path = gridMusicaIncorrecta.Rows[e.RowIndex].Cells[columnPath].Value.ToString();
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
                configuracionInicial();
                _activated = false;
            }
        }

        private void OpenFormMusic(string path) {
            _activated = true;
            FormMusic formMusic = new FormMusic(_api.ObtenerMusicaFormatView(path));
            formMusic.ShowDialog();
        }
    }
}
