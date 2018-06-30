using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using CapaLogica;

namespace CapaUsuario
{
    public partial class Form3 : Form
    {
        private bool IsNuevo;
        private DataTable datadetalle;

        public Form3()
        {
            InitializeComponent();
        }

        //Llenado del combo box
        private void llenarCombo()
        {
            this.folioscb.DataSource = UsuarioStruct.Mostrar();
            folioscb.ValueMember = "folioCred";
            folioscb.DisplayMember = "folioCred";
        }

        //Llenado del combo box
        private void llenarCombo2()
        {
            this.comboLibros.DataSource = LibroStruct.Mostrar();
            comboLibros.ValueMember = "claveLibro";
            comboLibros.DisplayMember = "claveLibro";
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            llenarCombo();
            llenarCombo2();
        }

        private void folioscb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (folioscb.Text == string.Empty)
                nombretxt.Text = string.Empty;

            string result;
            result=UsuarioStruct.Datos(folioscb.Text);
            nombretxt.Text = result;
        }

        private void folioscb_TextChanged(object sender, EventArgs e)
        {
            if (folioscb.Text == string.Empty)
                nombretxt.Text = string.Empty;

            string result;
            result = UsuarioStruct.Datos(folioscb.Text);
            nombretxt.Text = result;
        }

        private void comboLibros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLibros.Text == string.Empty)
                titulolibro.Text = string.Empty;

            string result;
            result = LibroStruct.Titulos(comboLibros.Text);
            titulolibro.Text = result;
        }

        private void comboLibros_TextChanged(object sender, EventArgs e)
        {
            if (comboLibros.Text == string.Empty)
                titulolibro.Text = string.Empty;

            string result;
            result = LibroStruct.Titulos(comboLibros.Text);
            titulolibro.Text = result;
        }
    }
}
