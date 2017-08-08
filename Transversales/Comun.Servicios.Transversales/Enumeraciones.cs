using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Servicios.Transversales
{
    public enum EstadoPost
    {
        Error = -1,
        Creado = 1,
        Enviando = 2,
        Enviado = 3
    }

    public enum EstadoAnalisis
    {
        Error = -1,
        Creado = 1,
        EnAnalisis = 2,
        Analizado = 3
    }

    public enum Opcion
    {
        Crear = 1,
        Actualizar = 2,
        Eliminar = 3,
        Consultar = 4
    }
}
