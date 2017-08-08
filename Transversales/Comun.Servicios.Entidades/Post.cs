using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Servicios.Entidades
{
    /// <summary>
    /// Clase empleada para el transporte de la información de los post que se transmitirán y se recibirán
    /// </summary>
    public class Post
    {
        public int Id { get; set; }
        // Corresponde al campo Post en BD, para no generar conflictos en el código
        public string Descripcion { get; set; }
        public int EstadoId { get; set; }
        public int CuentaPalabras { get; set; }
        public int CuentaOraciones { get; set; }
        public int CuentaParrafos { get; set; }
        public int CuentaCaracteres { get; set; }
    }

}
