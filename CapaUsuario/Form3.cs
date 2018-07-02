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

        //Mensaje de confirmacion
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Libreria Publica", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Libreria Publica", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            crearTabla();
            Habilitar(false);
            Botones();
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

        //Saber si un pasillo esta repetido
        private string UsuarioRepetido()
        {

            string folio = PrestamoStruct.RepeticionUsuarios(folioscb.Text);

            if (folio != "")

                folio = "El usuario ya cuenta con expediente activo";

            else
                folio = "";
            return folio;
        }

        private void Limpiar()
        {
            comboLibros.Text = string.Empty;
            folioscb.Text = string.Empty;
            titulolibro.Text = string.Empty;
            nombretxt.Text = string.Empty;
            secuenciatxt.Text = string.Empty;
            crearTabla();
        }

        //Habilitar controles del formulario
        private void Habilitar(bool valor)
        {
            this.comboLibros.Enabled = valor;
            this.folioscb.Enabled = valor;
        }

        //Habilitar Botones
        private void Botones()
        {
            if (this.IsNuevo)
            {
                this.Habilitar(true);
                this.crearbtn.Enabled = false;
                this.guardarbtn.Enabled = true;
                this.cancelarbtn.Enabled = true;
                this.agregarbtn.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.crearbtn.Enabled = true;
                this.guardarbtn.Enabled = false;
                this.cancelarbtn.Enabled = false;
                this.agregarbtn.Enabled = false;
            }
        }

        //Obtener secuencia del usuario
        private void Secuencia()
        {
            PrestamoStruct.Secuencia();
            int numeracion = PrestamoStruct.Secuencia();
            numeracion += 1;
            secuenciatxt.Text=Convert.ToString(numeracion);
        }

        private void crearbtn_Click(object sender, EventArgs e)
        {
            IsNuevo = true;
            this.Botones();
            this.Limpiar();
            Habilitar(true);
            llenarCombo();
            llenarCombo2();
            Secuencia();
        }

        private void cancelarbtn_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.Botones();
            Limpiar();
            Habilitar(false);
        }

        private void crearTabla()
        {
            this.datadetalle = new DataTable("Detalle");

            this.datadetalle.Columns.Add("idPrestamo", System.Type.GetType("System.Int32"));
            this.datadetalle.Columns.Add("idDetallePrestamo", System.Type.GetType("System.Int32"));
            this.datadetalle.Columns.Add("claveLibro", System.Type.GetType("System.String"));
            this.datadetalle.Columns.Add("titulo", System.Type.GetType("System.String"));

            this.prestamodetalle.DataSource = this.datadetalle;
        }

        private void agregarbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.titulolibro.Text == string.Empty)
                {
                    MensajeError("No se han seleccionado libros ");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow raw in datadetalle.Rows)
                    {
                        if (Convert.ToString(raw["claveLibro"]) == Convert.ToString(this.comboLibros.Text))
                        {
                            registrar = false;
                            this.MensajeError("El libro ya se ha registrado previamente");
                        }
                    }
                    if (registrar)
                    {
                        //Agregar detalle al listado
                        DataRow row = this.datadetalle.NewRow();
                        row["idPrestamo"] = Convert.ToString(this.secuenciatxt.Text);
                        row["claveLibro"] = Convert.ToString(this.comboLibros.Text);
                        row["titulo"] = Convert.ToString(this.titulolibro.Text);
                        this.datadetalle.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void guardarbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "", usuario="",respuestadetalle="";
                usuario = UsuarioRepetido();
                if (this.titulolibro.Text == string.Empty || this.nombretxt.Text == string.Empty || usuario != string.Empty)
                {
                    MensajeError("Datos ingresados erroneamente, favor de revisar");
                    if (this.titulolibro.Text == string.Empty)
                    {
                        MessageBox.Show("Libro definido");
                    }
                    if (this.nombretxt.Text == string.Empty)
                    {
                        MessageBox.Show("Usuario no identificado");
                    }

                    if (usuario != string.Empty)
                    {
                        MessageBox.Show("El Usuario ya cuenta con expediente activo");
                    }
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        respuesta = PrestamoStruct.Insertar(Convert.ToInt32(secuenciatxt.Text),folioscb.Text);
                        respuestadetalle = DetallePrestamoStruct.Insertar(datadetalle);
                    }

                    if (respuesta.Equals("KK")&&respuestadetalle.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOK("Registro guardado exitosamente");
                        }
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                    this.IsNuevo = false;
                    this.Botones();
                    this.Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
