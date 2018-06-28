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
    public class GeneroStruct
    {
        //Metodo para llamar a la funcion Insertar que esta en la capa de datos
        public static string Insertar(string idgenero, string descripcion)
        {
            GeneroData GD = new GeneroData();

            GD.IdGenero = idgenero;
            GD.Descripcion = descripcion;

            return GD.Insertar(GD);
        }

        //Metodo para llamar a la funcion Editar que esta en la capa de datos
        public static string Editar(string idgenero, string descripcion)
        {
            GeneroData GD = new GeneroData();

            GD.IdGenero = idgenero;
            GD.Descripcion = descripcion;

            return GD.Editar(GD);
        }

        //Metodo para llamar a la funcion Eliminar que esta en la capa de datos
        public static string Eliminar(string idgenero)
        {
            GeneroData GD = new GeneroData();

            GD.IdGenero = idgenero;

            return GD.Eliminar(GD);
        }

        //Metodo para llamar a la funcion Mostrar que esta en la capa de datos
        public static DataTable Mostrar()
        {
            return new GeneroData().Mostrar();
        }
    }
}
