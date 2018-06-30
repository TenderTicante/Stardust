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
        string aux;
        string aux2;
        string numeracion;

        private void Form1_Load(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
            Form2 Usuario = new Form2();
            Usuario.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
            Form3 Prestamo = new Form3();
            Prestamo.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panelEjemplares.Visible = false;
            Form4 Devolucion = new Form4();
            Devolucion.ShowDialog();
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
            MostrarLibros();
            BotonesLibrosPanel();
            Habilitar(false);
            llenarCombo();
        }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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
            idgenerotxt.ReadOnly = true;
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

                    if (this.descripciongentxt.Text == string.Empty)
                    {
                        MensajeError("No se indico un genero, favor de revisar");
                    }
                }
                else
                {
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

        //Habilitar controles del formulario
        private void Habilitar(bool valor)
        {
            this.autxt.ReadOnly = !valor;
            this.titxt.ReadOnly = !valor;
            this.pasillotxt.ReadOnly = !valor;
            this.añotxt.ReadOnly = !valor;
            this.cbgen.Enabled = valor;
        }

        //Habilitar Botones

        private void BotonesLibrosPanel()
        {
            if (this.isNuevo || this.isEditar)
            {
                this.Habilitar(true);
                this.nuevolibro.Enabled = false;
                this.guardarlibro.Enabled = true;
                this.editarlibro.Enabled = false;
                this.cancelarlibro.Enabled = true;
            }

            else
            {
                this.Habilitar(false);
                this.nuevolibro.Enabled = true;
                this.guardarlibro.Enabled = false;
                this.editarlibro.Enabled = true;
                this.cancelarlibro.Enabled = false;
            }
        }
        //Mostrar los Libros Registrados en la base de datos
        private void MostrarLibros()
        {
            this.listadolibros.DataSource = LibroStruct.Mostrar();
        }

        //Obtener secuencia del genero
        private string Secuencia()
        {
            LibroStruct.Secuencia(cbgen.Text);
            numeracion = LibroStruct.Secuencia(cbgen.Text);
            if (numeracion == "")
            {
                numeracion = "0000";
            }
            else
            {
                int aux3 = Convert.ToInt32(numeracion) + 1;

                if (aux3 >= 1 && aux3 <= 9)
                {
                    numeracion = "000" + Convert.ToString(aux3);
                }

                if (aux3 >= 10 && aux3 <= 99)
                {
                    numeracion = "00" + Convert.ToString(aux3);
                }

                if (aux3 >= 100 && aux3 <= 999)
                {
                    numeracion = "0" + Convert.ToString(aux3);
                }

                if (aux3 >= 1000 && aux3 <= 9999)
                {
                    numeracion = Convert.ToString(aux3);
                }
            }
            return numeracion;
        }

        //Saber si un pasillo esta repetido
        private string PasilloRepetido() {

            string pasillo = LibroStruct.RepeticionPasillos(pasillotxt.Text);

            if (pasillo != "")

                pasillo = "Pasillo Repetido, favor de corregirlo";

            else
                pasillo = "";
            return pasillo;
        }

        //boton nuevo libro
        private void nuevolibro_Click(object sender, EventArgs e)
        {
            this.isNuevo = true;
            this.isEditar = false;
            this.BotonesLibrosPanel();
            this.LimpiarCamposLibro();
            this.Habilitar(true);
        }

        //boton editar libro
        private void editarlibro_Click(object sender, EventArgs e)
        {
            if (!this.claveltxt.Text.Equals(""))
            {
                this.isEditar = true;
                this.BotonesLibrosPanel();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Seleccionar el registro a modificar");
            }
        }

        //boton eliminar libro
        private void eliminarlibro_Click(object sender, EventArgs e)
        {
            if (altlibtxt.Text != string.Empty)
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Esta seguro de eliminar el registros de la base de datos?", "Libreria Publica", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (Opcion == DialogResult.OK)
                    {
                        string codigo;
                        string respuesta = "";

                        codigo = altlibtxt.Text;
                        respuesta = LibroStruct.Eliminar(codigo);
                        if (respuesta.Equals("KK"))
                        {
                            this.MensajeOK("Se elimino correctamente el registro");
                        }
                        else
                        {
                            this.MensajeError(respuesta);
                        }
                        this.MostrarLibros();
                        this.LimpiarCamposLibro();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            else this.MensajeError("No hay registros para eliminar, seleccione uno del listado");
        }

        private void guardarlibro_Click(object sender, EventArgs e)
        {
            string aux4 = PasilloRepetido();
            try
            {
                string respuesta = "";
                if ((isNuevo&& aux4 != string.Empty)||this.pasillotxt.Text == string.Empty || cbgen.Text == string.Empty||autxt.Text==string.Empty||titxt.Text==string.Empty)
                {
                    MensajeError("Datos ingresados erroneamente, favor de revisar");
                    if (aux4 != string.Empty)
                    {
                        MessageBox.Show("Clave de Pasillo Repetida");
                    }
                    if (this.pasillotxt.Text == string.Empty)
                    {
                        MessageBox.Show("Clave de Pasillo Vacia");
                    }
                    if (cbgen.Text == string.Empty)
                    {
                        MessageBox.Show("Genero de libro no definido");
                    }
                    if (autxt.Text == string.Empty)
                    {
                        MessageBox.Show("Autor de libro no definido");
                    }
                    if (titxt.Text == string.Empty)
                    {
                        MessageBox.Show("Titulo de libro no definido");
                    }
                }
                else
                {
                    if (this.isNuevo)
                    {
                        respuesta = LibroStruct.Insertar(this.claveltxt.Text.Trim(), this.titxt.Text.Trim(), this.autxt.Text.Trim(), this.pasillotxt.Text.Trim(), this.cbgen.Text.Trim(), this.añotxt.Text.Trim());
                    }
                    else
                    {
                        respuesta = LibroStruct.Editar(this.altlibtxt.Text.Trim(), this.titxt.Text.Trim(), this.autxt.Text.Trim(), this.pasillotxt.Text.Trim(), this.cbgen.Text.Trim(), this.añotxt.Text.Trim());
                    }

                    if (respuesta.Equals("KK"))
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
                    this.BotonesLibrosPanel();
                    this.LimpiarCamposLibro();
                    this.MostrarLibros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void cancelarlibro_Click(object sender, EventArgs e)
        {
            this.isNuevo = false;
            this.isEditar = false;
            this.BotonesLibrosPanel();
            this.LimpiarCamposLibro();
            this.Habilitar(false);
        }

        //Seleccion del registro del libro
        private void listadolibros_DoubleClick(object sender, EventArgs e)
        {
            altlibtxt.Text = Convert.ToString(this.listadolibros.CurrentRow.Cells["claveLibro"].Value);
            titxt.Text = Convert.ToString(this.listadolibros.CurrentRow.Cells["titulo"].Value);
            autxt.Text = Convert.ToString(this.listadolibros.CurrentRow.Cells["autor"].Value);
            pasillotxt.Text = Convert.ToString(this.listadolibros.CurrentRow.Cells["pasillo"].Value);
            cbgen.Text = Convert.ToString(this.listadolibros.CurrentRow.Cells["idGenero"].Value);
            añotxt.Text = Convert.ToString(this.listadolibros.CurrentRow.Cells["añoPublicacion"].Value);
        }

        
        private void pasillotxt_TextChanged(object sender, EventArgs e)
        {
            aux = pasillotxt.Text;
            claveltxt.Text = aux2 + aux + Secuencia();
        }

        private void cbgen_SelectedValueChanged(object sender, EventArgs e)
        {
            aux2 = cbgen.Text;
            claveltxt.Text = aux2 + aux + Secuencia();
        }
    }
}
