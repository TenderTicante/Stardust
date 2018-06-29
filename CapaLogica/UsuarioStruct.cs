using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Importacion de los recursos de la capa de Datos
using CapaDatos;
using System.Data;

namespace CapaLogica
{
    public class UsuarioStruct
    {
        //Metodo para llamar a la funcion Insertar que esta en la capa de datos
        public static string Insertar(string folio, string nombre, DateTime fechareg)
        {
            UsuarioData UD = new UsuarioData();
            UD.FolioCred = folio;
            UD.Nombre = nombre;
            UD.FechaRegistro = fechareg;

            return UD.Insertar(UD);
        }

        //Metodo para llamar a la funcion Editar que esta en la capa de datos

        public static string Editar(string folio, string nombre, DateTime fechareg)
        {
            UsuarioData UD = new UsuarioData();
            UD.FolioCred = folio;
            UD.Nombre = nombre;
            UD.FechaRegistro = fechareg;

            return UD.Editar(UD);
        }

        //Metodo para llamar a la funcion Eliminar que esta en la capa de datos

        public static string Eliminar(string folio)
        {
            UsuarioData UD = new UsuarioData();
            UD.FolioCred = folio;

            return UD.Eliminar(UD);
        }

        //Metodo para llamar a la funcion Mostrar que esta en la capa de datos

        public static DataTable Mostrar()
        {

            return new UsuarioData().Mostrar();
        }

        public static string Secuencia(DateTime año)
        {
            UsuarioData UD = new UsuarioData();
            UD.FechaRegistro = año;

            return UD.Numeracion(año);
        }
    }
}
