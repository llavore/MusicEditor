
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicEditor.Helpers
{
    public enum ColumnNamesManagement { 
        Modificar = 1,
        Eliminar = 2
    }


    public static class DataGridViewExtensions
    {
        public static DataGridView GridStyleMusicaCorrecta(this DataGridView dataGrid) {
            int width = dataGrid.Width/9;
           
            dataGrid.Columns[0].Width = width/2+3;
            dataGrid.Columns[1].Width = width/2+3;
            dataGrid.Columns[2].Width = width * 2 + width / 2;
            dataGrid.Columns[3].Width = width * 2 + width / 2;
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[5].Visible = false;
            dataGrid.Columns[6].Width = width;
            dataGrid.Columns[7].Width = width;
            
            return dataGrid;
        }

        public static DataGridView GridStyleMusicaIncorrecta(this DataGridView dataGrid)
        {
            int width = dataGrid.Width / 10;

           
            dataGrid.Columns[0].Visible = false;
            dataGrid.Columns[1].Visible = false;
            dataGrid.Columns[2].Visible = false;
            dataGrid.Columns[3].Visible = false;
            dataGrid.Columns[4].Visible = false;
            dataGrid.Columns[5].Width = (width * 7) - 2;
            dataGrid.Columns[6].Width = width * 1;
            dataGrid.Columns[7].Width = width * 1;
            


            return dataGrid;
        }

        private static DataGridViewColumn AddColumn<TModel>(Expression<Func<TModel, object>> property) {
            
            
            DataGridViewColumn column = new DataGridViewColumn();
            column.Name = property.Name;
            column.HeaderText = property.Name;
            return column;
        }

        public static DataGridView addColumnButton(this DataGridView dataGrid, string NameButton) {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "";
            btnColumn.Text = NameButton;
            btnColumn.UseColumnTextForButtonValue = true;
            dataGrid.Columns.Add(btnColumn);
            return dataGrid;
        }

        public static DataGridView addColumnComboBox(this DataGridView dataGrid, List<string> lista)
        {
            DataGridViewComboBoxColumn ComboBoxcolumn = new DataGridViewComboBoxColumn();
            var list11 = new List<string>() { "10", "30", "80", "100" };
            ComboBoxcolumn.DataSource = lista;
            ComboBoxcolumn.DisplayIndex = 0;
            dataGrid.Columns.Add(ComboBoxcolumn);
            return dataGrid;
        }

        public static DataGridView ClearColumns(this DataGridView dataGrid)
        {
            if(dataGrid.Columns.Count > 0){
                dataGrid.Columns.Clear();
            }
            return dataGrid;
        }

        public static DataGridView LoadData(this DataGridView dataGrid, DataTable dades) {
            dataGrid.ClearColumns();
            BindingSource bs = new BindingSource();
            bs.DataSource = dades;
            dataGrid.DataSource = bs;
            dataGrid.addColumnButton("Modificar").addColumnButton("Eliminar");
            dataGrid.ClearSelection();
            return dataGrid;
        }

        public static DataGridView LoadDataWithList(this DataGridView dataGrid, DataView dades, List<string> lista)
        {
            dataGrid.ClearColumns();
            BindingSource bs = new BindingSource();
            bs.DataSource = dades;
            dataGrid.DataSource = bs;
            dataGrid.addColumnComboBox(lista).addColumnButton("Modificar").addColumnButton("Eliminar");
            dataGrid.ClearSelection();
            return dataGrid;
        }

        public static DataGridView LoadData(this DataGridView dataGrid, DataView dades)
        {
            dataGrid.ClearColumns();
            BindingSource bs = new BindingSource();
            bs.DataSource = dades;
            dataGrid.DataSource = bs;
            dataGrid.addColumnButton("Modificar").addColumnButton("Eliminar");
            dataGrid.ClearSelection();
            return dataGrid;
        }

        public static DataGridView LoadData<TModel>(this DataGridView dataGrid, List<TModel> datos)
        {
            dataGrid.ClearColumns();
            BindingSource bs = new BindingSource();
            bs.DataSource = datos;
            dataGrid.DataSource = bs;
            dataGrid.addColumnButton("Modificar").addColumnButton("Eliminar");
            dataGrid.ClearSelection();
            return dataGrid;
        }
    }
}
