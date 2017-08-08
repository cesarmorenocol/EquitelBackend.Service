using Analizador.Servicio.Negocio;
using Posts.Servicio.Negocio;
using System;
using System.Threading;

namespace Comun.Servicios.Test
{
    class Program
    {
        int tiempoEnvio = 1000;
        static EnviadorPosts sender = new EnviadorPosts();
        static AnalizadorPosts analizador = new AnalizadorPosts();

        static void Main(string[] args)
        {
            //sender.Gestionar();
            analizador.Gestionar();
            Console.ReadKey();
        }
    }
}
