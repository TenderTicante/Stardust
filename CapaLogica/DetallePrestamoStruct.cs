using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaLogica
{
    public class DetallePrestamoStruct
    {
        public static string Insertar(DataTable dtDetalle)
        {
            List<DetallePrestamo> detalle = new List<DetallePrestamo>();

            DetallePrestamo Detalle = new DetallePrestamo();
            foreach (DataRow raw in dtDetalle.Rows)
            {
                DetallePrestamo detail = new DetallePrestamo();
                detail.IdPrestamo = Convert.ToInt32(raw["idPrestamo"].ToString());
                detail.ClaveLibro = Convert.ToString(raw["claveLibro"].ToString());
                detalle.Add(detail);
            }
            return Detalle.InsertarDetalle(detalle);
        }
    }
}
