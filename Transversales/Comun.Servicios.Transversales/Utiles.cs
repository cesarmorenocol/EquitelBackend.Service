using Comun.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Servicios.Transversales
{
    public static class Utiles
    {
        /// <summary>
        /// Permite analizar el post según los requerimientos del ejercicio
        /// </summary>
        public static Post AnalizarPost(Post post)
        {
            post.CuentaParrafos = ContarParrafos(post.Descripcion);
            post.CuentaPalabras = ContarPalabrasTerminaN(post.Descripcion);
            post.CuentaCaracteres = ContarCaracteresAlfanumericos(post.Descripcion);
            post.CuentaOraciones = ContarOraciones(post.Descripcion);
            return post;
        }

        /// <summary>
        /// Cuenta la cantidad de párrafos que tiene el post
        /// </summary>
        static int ContarParrafos(string postTexto)
        {
            string[] parrafos = postTexto.Split('\r');
            return parrafos.Count(c => c != "\n");
        }

        static int ContarPalabrasTerminaN(string postTexto)
        {
            // Quitar saltos de linea y finales de linea:
            postTexto = postTexto.Replace("\r", Constantes.esp).Replace("\n", Constantes.esp);
            // Reemplazar signos de puntuación base:
            postTexto = postTexto.Replace(".", Constantes.esp).Replace(",", Constantes.esp).Replace(";", Constantes.esp);
            string[] palabras = postTexto.Split(' ');
            return palabras.Count(c => c.ToLower().EndsWith("n"));
        }

        static int ContarCaracteresAlfanumericos(string postTexto)
        {
            // Quitar saltos de linea y finales de linea:
            postTexto = postTexto.Replace("\r", string.Empty).Replace("\n", string.Empty);
            // Reemplazar signos de puntuación base:
            postTexto = postTexto.Replace(".", string.Empty).Replace(",", string.Empty).Replace(";", string.Empty);
            // Eliminar los carcateres n:
            postTexto = postTexto.ToLower().Replace("n", string.Empty);
            // Reemplazar espacios
            postTexto = postTexto.Replace(" ", string.Empty);
            return postTexto.ToLower().Length;
        }

        static int ContarOraciones(string postTexto)
        {
            // Quitar saltos de linea y finales de linea:
            postTexto = postTexto.Replace("\r", Constantes.esp).Replace("\n", Constantes.esp);
            // Reemplazar los caracteres de puntuación por espacios:
            string[] oraciones = postTexto.Split('.')
                                          .Select(a => a.Trim().
                                                         Replace(".", Constantes.esp).Replace(",", Constantes.esp).
                                                         Replace(";", Constantes.esp).Replace("  ", Constantes.esp))
                                          .ToArray().Where(b => b != string.Empty).ToArray() ;
            // Recuperar las oraciones con mas de 15 palabras:
            return oraciones.Count(c => c.Split(' ').Count() > 15);
        }


        /// <summary>
        /// Permite la generación de los post para enviar:
        /// </summary>
        public static string GenerarPost()
        {
            string post = string.Empty;
            for (int i = 150; i < 600; i = i + 150)
            {
                post += RandomString(i);
                post += post + ".\n\r";
            }
            return post;
        }

        /// <summary>
        /// Toomado de internet. Fuente: StackOverflow.
        /// </summary>
        static string RandomString(int Size)
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789 .,;";
            Random random = new Random();
            var chars = Enumerable.Range(0, Size)
                                   .Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }

    }
}
