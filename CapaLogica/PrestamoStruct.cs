using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class PrestamoStruct
    {
        public static int Secuencia()
        {
            PrestamoData PD = new PrestamoData();

            return PD.Numeracion();
        }

        public static string RepeticionUsuarios(string folio)
        {
            PrestamoData PD = new PrestamoData();
            PD.FolioCred = folio;

            return PD.BusquedaUsuario(folio);
        }

        //Metodo para llamar a la funcion Insertar que esta en la capa de datos
        public static string Insertar(int idprestamo, string cusuario)
        {
            PrestamoData PD = new PrestamoData();
            PD.IdPrestamo = idprestamo;
            PD.FolioCred = cusuario;

            return PD.Insertar(PD);
        }
    }
}
