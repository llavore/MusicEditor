using MusicEditor.Bussines.APIs;
using MusicEditor.Forms.Helpers;
using MusicEditor.Forms.Models;
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
using static MusicEditor.Forms.Models.MusicView;

namespace MusicEditor.Forms.Forms
{
    public partial class FormMusic : Form
    {
        readonly private MusicView _music;
        readonly private IMusicApi _api;
        
        public FormMusic(MusicView row)
        {
            InitializeComponent();
            _music = row;
            _api = new MusicAPI(AplicationContext.AplicationContext._repositoryManager);
        }

        private void FormMusic_Load(object sender, EventArgs e)
        {
            txtPath.Text = _music.path;
            txtPath.ReadOnly = true;
            List<string> listaCategorias = _api.ObtenerCategorias();
            List<string> listaGrupos = _api.ObtenerGrupos();
            if (_music.state is MusicRowState.correct)
            {
                txtNombre.Text = _music.title;
                cmbCategoria.CargarCombo(listaCategorias, _music.category);
                cmbGrupo.CargarCombo(listaGrupos, _music.group);
            }
            else 
            {
                cmbCategoria.CargarCombo(listaCategorias, listaCategorias[0]);
                cmbGrupo.CargarCombo(listaGrupos, listaGrupos[0]);
            }
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            Decimal numeroActual = NumBoxNumero.Value;
            string categorySelected = cmbCategoria.SelectedItem.ToString();
            NumBoxNumero.Maximum = _api.totalMusicaCategoria(categorySelected) + 1;
            NumBoxNumero.Minimum = 1;

            if (_music.category == categorySelected)
            {
                NumBoxNumero.Value = _music.number;
            }
            else if(_music.category != categorySelected) 
            {
                NumBoxNumero.Value = NumBoxNumero.Maximum;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarInformacion())
            {
                _music.number = (int)NumBoxNumero.Value;
                _music.category = cmbCategoria.SelectedItem.ToString();
                _music.group = cmbGrupo.SelectedItem.ToString();
                _music.title = txtNombre.Text;
                _api.ModificarMusica(_music);
                this.Close();
            }
            else
            {
                MessageHelper.ErrorMessage("El campo nombre no debe quedar vacio.");
            }
        }

        private bool validarInformacion()
        {
            bool validacion = true;

            if ( txtNombre.Text.Length <= 0) { validacion = false; }
            
            return validacion;
        }
    }
}
