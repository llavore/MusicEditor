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

namespace MusicEditor.Forms.Forms
{
    public partial class FormMusic : Form
    {
        readonly private MusicView _music;
        readonly private IMusicApi _api;
        
        public FormMusic(DataRow row, IMusicApi api)
        {
            InitializeComponent();
            _music = new MusicView(row);
            _api = api;
        }

        private void FormMusic_Load(object sender, EventArgs e)
        {
            txtPath.Text = _music.path;
            txtPath.ReadOnly = true;
            List<string> listaCategorias = _api.ObtenerCategorias();
            List<string> listaGrupos = _api.ObtenerGrupos();
            if (_music.state)
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
            string categorySelected = cmbCategoria.SelectedItem.ToString();
            int numeroTotalCategoria = _api.totalMusicaCategoria(categorySelected);
            NumBoxNumero.Maximum = numeroTotalCategoria+1;
            NumBoxNumero.Minimum = 0;
            
            if (_music.category == categorySelected && _music.state)
            {
                NumBoxNumero.Value = _music.number;
            }
            else 
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
                _api.ModificarMusica(_music.toArray());
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
