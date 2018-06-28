using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaLogica;

namespace CapaUsuario
{
    public partial class Form1 : Form
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
            panelPrestamos.Visible = true;
            panelUsuario.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = true;
            panelPrestamos.Visible = false;
            panelUsuario.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
            panelPrestamos.Visible = false;
            panelUsuario.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
            panelPrestamos.Visible = true;
            panelUsuario.Visible = false;
        }

        //Banderas para saber si es insercion o edicion
        private bool isNuevo = false;
        private bool isEditar = false;

        public Form1()
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

        //Limpiar Controles del Panel de generos
        private void Limpiar()
        {
            this.idgenerotxt.Text = string.Empty;
            this.descripciongentxt.Text = string.Empty;

        }
        //Panel de Generos control de botones
        private void Botones()
        {
            if (this.isNuevo || this.isEditar)
            {
                this.nuevobtn.Enabled = false;
                this.guardarbtn.Enabled = true;
                this.editarbtn.Enabled = false;
                this.cancelarbtn.Enabled = true;
            }

            else
            {
                this.nuevobtn.Enabled = true;
                this.guardarbtn.Enabled = false;
                this.editarbtn.Enabled = true;
                this.cancelarbtn.Enabled = false;
            }
        }

        //Panel de Generos Listado
        private void MostrarColumnas()
        {
            this.listadogeneros.DataSource = GeneroStruct.Mostrar();
        }
        private void panelEjemplares_Paint(object sender, PaintEventArgs e)
        {
            MostrarColumnas();
            Botones();
            llenarCombo();
        }

        //Boton Alta de Genero
        private void nuevobtn_Click(object sender, EventArgs e)
        {
            this.isNuevo = true;
            this.isEditar = false;
            this.Botones();
            this.Limpiar();
            this.idgenerotxt.ReadOnly = false;
            this.descripciongentxt.ReadOnly = false;
        }

        //Cambios en el panel de genero
        private void editarbtn_Click(object sender, EventArgs e)
        {
            this.idgenerotxt.ReadOnly = true;
            this.descripciongentxt.ReadOnly = false;
            this.isEditar = true;
            this.Botones();
        }

        //Eliminacion de un genero
        private void eliminarbtn_Click(object sender, EventArgs e)
        {
            if (idgenerotxt.Text != string.Empty)
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Esta seguro de eliminar el registros de la base de datos?", "Libreria Publica", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (Opcion == DialogResult.OK)
                    {
                        string codigo;
                        string respuesta = "";

                        codigo = idgenerotxt.Text;
                        respuesta = GeneroStruct.Eliminar(codigo);
                        if (respuesta.Equals("OK"))
                        {
                            this.MensajeOK("Se elimino correctamente el registro");
                        }
                        else
                        {
                            this.MensajeError(respuesta);
                        }
                        this.MostrarColumnas();
                        this.Limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            else this.MensajeError("No hay registros para eliminar, seleccione uno del listado");
        }

        //Cancelar la edicion panel de generos
        private void cancelarbtn_Click(object sender, EventArgs e)
        {
            this.isNuevo = false;
            this.isEditar = false;
            this.Botones();
            this.Limpiar();
        }

        //Guardar Generos
        private void guardarbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";

                if (this.idgenerotxt.TextLength > 2 || this.idgenerotxt.Text == string.Empty)
                {
                    MensajeError("La clave de genero contiene mas de 2 caracteres, favor de revisar");
                }
                if (this.descripciongentxt.Text == string.Empty)
                {
                    MensajeError("No se indico un genero, favor de revisar");
                }

                if (this.isNuevo)
                {
                    respuesta = GeneroStruct.Insertar(this.idgenerotxt.Text.Trim(), this.descripciongentxt.Text.Trim());
                    isNuevo = false;
                }
                else
                {
                    respuesta = GeneroStruct.Editar(this.idgenerotxt.Text.Trim(), this.descripciongentxt.Text.Trim());
                }

                if (respuesta.Equals("OK"))
                {
                    if (this.isNuevo)
                    {
                        this.MensajeOK("Registro guardado exitosamente");
                    }
                    else
                    {
                        this.MensajeOK("Se actualizo el registro correctamente");
                    }
                }
                else
                {
                    this.MensajeError(respuesta);
                }
                this.isNuevo = false;
                this.isEditar = false;
                this.Botones();
                this.Limpiar();
                this.MostrarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        //Seleccion catalago de generos
        private void listadogeneros_DoubleClick(object sender, EventArgs e)
        {
            this.idgenerotxt.Text = Convert.ToString(this.listadogeneros.CurrentRow.Cells["idgenero"].Value);
            this.descripciongentxt.Text = Convert.ToString(this.listadogeneros.CurrentRow.Cells["descripcion"].Value);
        }

        //Libros

        //Llenado del combo box
        private void llenarCombo()
        {
            this.cbgen.DataSource = GeneroStruct.Mostrar();
            cbgen.ValueMember = "idGenero";
            cbgen.DisplayMember = "idGenero";
        }

        //Limpiar Controles del Formulario
        private void LimpiarCamposLibro()
        {
            this.claveltxt.Text = string.Empty;
            this.autxt.Text = string.Empty;
            this.titxt.Text = string.Empty;
            this.pasillotxt.Text = string.Empty;
            this.añotxt.Text = string.Empty;
            this.cbgen.SelectedIndex = -1;
        }

        //Obtener secuencia del genero
        private void Secuencia()
        {
            LibroStruct.Secuencia(cbgen.Text);
            string numeracion=LibroStruct.Secuencia(cbgen.Text);
            if (numeracion == "")
            {
                numeracion = "0000";
                MessageBox.Show(numeracion);
            }
            else
            {
                MessageBox.Show(numeracion);
            }
        }

        //Saber si un pasillo esta repetido
        private void PasilloRepetido() {
            string pasillo=LibroStruct.RepeticionPasillos(pasillotxt.Text);
            if (pasillo != null || pasillo != "")
                MessageBox.Show("Pasillo Repetido, favor de corregirlo");
        }
        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guardarlibro_Click(object sender, EventArgs e)
        {
            Secuencia();
            PasilloRepetido();
        }
    }
}
