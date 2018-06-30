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
    public partial class Form2 : Form
    {
        string numeracion;
        DateTime Hoy = DateTime.Today;
        public Form2()
        {
            InitializeComponent();
        }

        //Banderas para saber si es insercion o edicion
        private bool isNuevo = false;
        private bool isEditar = false;

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

        //Limpiar Controles
        private void Limpiar()
        {
            this.folioutxt.Text = string.Empty;
            this.folioauxtxt.Text = string.Empty;
            this.nombreutxt.Text = string.Empty;
            this.fechareg.Value = DateTime.Now;
        }

        //Habilitar controles del formulario
        private void Habilitar(bool valor)
        {
            this.nombreutxt.ReadOnly = !valor;
            this.fechareg.Enabled = valor;
        }

        //Panel de Generos control de botones
        private void Botones()
        {
            if (this.isNuevo || this.isEditar)
            {
                this.Habilitar(true);
                this.nuevobtn.Enabled = false;
                this.guardarbtn.Enabled = true;
                this.editarbtn.Enabled = false;
                this.cancelarbtn.Enabled = true;
            }

            else
            {
                this.Habilitar(false);
                this.nuevobtn.Enabled = true;
                this.guardarbtn.Enabled = false;
                this.editarbtn.Enabled = true;
                this.cancelarbtn.Enabled = false;
            }
        }

        //Mostrar los Usuarios Registrados en la base de datos
        private void MostrarUsuarios()
        {
            this.listadousuarios.DataSource = UsuarioStruct.Mostrar();
        }

        //Obtener secuencia del usuario
        private void Secuencia()
        {
            UsuarioStruct.Secuencia(Hoy.ToString("yyyy"));
            numeracion = UsuarioStruct.Secuencia(Hoy.ToString("yyyy"));
            if (numeracion == "")
            {
                numeracion = "00000";
                folioutxt.Text = Hoy.ToString("yyyy") + numeracion;
            }
            else
            {
                int aux3 = Convert.ToInt32(numeracion) + 1;

                if (aux3 >= 1 && aux3 <= 9)
                {
                    numeracion = "0000" + Convert.ToString(aux3);
                    folioutxt.Text = Hoy.ToString("yyyy") + numeracion;
                }

                if (aux3 >= 10 && aux3 <= 99)
                {
                    numeracion = "000" + Convert.ToString(aux3);
                    folioutxt.Text = Hoy.ToString("yyyy") + numeracion;
                }

                if (aux3 >= 100 && aux3 <= 999)
                {
                    numeracion = "00" + Convert.ToString(aux3);
                    folioutxt.Text = Hoy.ToString("yyyy") + numeracion;
                }

                if (aux3 >= 1000 && aux3 <= 9999)
                {
                    numeracion = "0" + Convert.ToString(aux3);
                    folioutxt.Text = Hoy.ToString("yyyy") + numeracion;
                }

                if (aux3 >= 10000 && aux3 <= 99999)
                {
                    numeracion = Convert.ToString(aux3);
                }
            }
        }
        private void nuevobtn_Click(object sender, EventArgs e)
        {
            this.isNuevo = true;
            this.isEditar = false;
            this.Botones();
            this.Limpiar();
            this.Secuencia();
            this.Habilitar(true);
        }

        private void editarbtn_Click(object sender, EventArgs e)
        {
            if (!this.folioauxtxt.Text.Equals(""))
            {
                this.isEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Seleccionar el registro a modificar");
            }
        }

        private void eliminarbtn_Click(object sender, EventArgs e)
        {
            if (folioauxtxt.Text != string.Empty)
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Esta seguro de eliminar el registros de la base de datos?", "Libreria Publica", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (Opcion == DialogResult.OK)
                    {
                        string codigo;
                        string respuesta = "";

                        codigo = folioauxtxt.Text;
                        respuesta = UsuarioStruct.Eliminar(codigo);
                        if (respuesta.Equals("KK"))
                        {
                            this.MensajeOK("Se elimino correctamente el registro");
                        }
                        else
                        {
                            this.MensajeError(respuesta);
                        }
                        this.MostrarUsuarios();
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

        private void guardarbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if ((isNuevo && folioutxt.Text == string.Empty) || this.nombreutxt.Text == string.Empty)
                {
                    MensajeError("Datos ingresados erroneamente, favor de revisar");
                    
                    if (nombreutxt.Text == string.Empty)
                    {
                        MessageBox.Show("Nombre de usuario no definido");
                    }
                }
                else
                {
                    if (this.isNuevo)
                    {
                        respuesta = UsuarioStruct.Insertar(this.folioutxt.Text.Trim(), this.nombreutxt.Text.Trim(), Convert.ToDateTime(fechareg.Value.ToString("dd/MM/yyyy")));
                    }
                    else
                    {
                        respuesta = UsuarioStruct.Editar(this.folioauxtxt.Text.Trim(), this.nombreutxt.Text.Trim(), Convert.ToDateTime(fechareg.Value.ToString("dd/MM/yyyy")));
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
                    this.Botones();
                    this.Limpiar();
                    this.MostrarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void cancelarbtn_Click(object sender, EventArgs e)
        {
            this.isNuevo = false;
            this.isEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
            Botones();
            Habilitar(false);
            this.fechareg.Value = DateTime.Now;
        }

        private void listadousuarios_DoubleClick(object sender, EventArgs e)
        {
            folioauxtxt.Text = Convert.ToString(this.listadousuarios.CurrentRow.Cells["folioCred"].Value);
            nombreutxt.Text = Convert.ToString(this.listadousuarios.CurrentRow.Cells["nombre"].Value);
            fechareg.Text = Convert.ToString(this.listadousuarios.CurrentRow.Cells["fechaRegistro"].Value);
        }
    }
}
