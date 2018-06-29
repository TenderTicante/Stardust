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
    public class LibroStruct
    {
        //Metodo para llamar a la funcion Insertar que esta en la capa de datos
        public static string Insertar(string clavel, string titulo, string autor, string pasillo, string idgen, string añop)
        {
            LibroData LD = new LibroData();
            LD.ClaveLibro = clavel;
            LD.Titulo = titulo;
            LD.Autor = autor;
            LD.Pasillo = pasillo;
            LD.IdGenero = idgen;
            LD.AñoPuublicacion = añop;

            return LD.Insertar(LD);
        }

        //Metodo para llamar a la funcion Editar que esta en la capa de datos

        public static string Editar(string clavel, string titulo, string autor, string pasillo, string idgen, string añop)
        {
            LibroData LD = new LibroData();
            LD.ClaveLibro = clavel;
            LD.Titulo = titulo;
            LD.Autor = autor;
            LD.Pasillo = pasillo;
            LD.IdGenero = idgen;
            LD.AñoPuublicacion = añop;

            return LD.Editar(LD);
        }

        //Metodo para llamar a la funcion Eliminar que esta en la capa de datos

        public static string Eliminar(string clavel)
        {
            LibroData LD = new LibroData();
            LD.ClaveLibro = clavel;

            return LD.Eliminar(LD);
        }

        //Metodo para llamar a la funcion Mostrar que esta en la capa de datos

        public static DataTable Mostrar()
        {

            return new LibroData().Mostrar();
        }

        public static string Secuencia(string genero)
        {
            LibroData LD = new LibroData();
            LD.IdGenero = genero;

            return LD.Numeracion(genero);
        }

        public static string RepeticionPasillos(string pasillos)
        {
            LibroData LD = new LibroData();
            LD.Pasillo = pasillos;

            return LD.RepeticionPasillos(pasillos);
        }
    }
}
