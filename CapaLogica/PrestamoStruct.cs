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
        public static string Insertar(string folioCred, DataTable dtDetalle)
        {
            PrestamoData Prestamo = new PrestamoData();
            Prestamo.FolioCred = folioCred;
            List<DetallePrestamo> detalle = new List<DetallePrestamo>();

            foreach (DataRow raw in dtDetalle.Rows)
            {
                DetallePrestamo detail = new DetallePrestamo();
                detail.IdPrestamo = Convert.ToInt32(raw["idPrestamo"].ToString());
                detail.ClaveLibro = Convert.ToString(raw["claveLibro"].ToString());
                detalle.Add(detail);
            }
            return Prestamo.Insertar(Prestamo, detalle);
        }

        //Metodo para Eliminar detalle del prestamo
        public static string Eliminar(int idprestamo,string clavel)
        {
            DetallePrestamo Detalle = new DetallePrestamo();
            Detalle.IdPrestamo = idprestamo;
            Detalle.ClaveLibro = clavel;
            return Detalle.Eliminar(Detalle);
        }

        public static DataTable MostrarDetalle(int varaux)
        {
            PrestamoData Prestamo = new PrestamoData();
            Prestamo.IdPrestamo = varaux;
            return Prestamo.MostrarDetalles(varaux);
        }
    }
}
