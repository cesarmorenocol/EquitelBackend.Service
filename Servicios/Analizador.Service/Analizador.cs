using Analizador.Servicio.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analizador.Service
{
    public partial class Analizador : ServiceBase
    {
        #region Variables de clase

        // Temporizadores
        int analizadorTiempo = 1000;
        // Timers
        Timer analizadorTimer;
        // Haandler de la lógica de procesamiento:
        AnalizadorPosts sender = null;

        #endregion

        public Analizador()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            InicializarServicio();
        }

        protected override void OnStop()
        {
        }

        /// <summary>
        /// Permite la inicialización de los elementos propios del servicio
        /// </summary>
        private void InicializarServicio()
        {
            try
            {
                sender = new AnalizadorPosts();
                // Inicializar timer de revisión de nuevas imágenes.
                analizadorTimer = new System.Threading.Timer(new TimerCallback(SenderTimerTick), null, 0, analizadorTiempo);
            }
            catch (Exception e)
            {
                // Registro de eventos de errores:
            }
        }

        /// <summary>
        /// Administrador del timer para el enviador de posts:
        /// </summary>
        private void SenderTimerTick(object obj)
        {
            analizadorTimer.Change(Timeout.Infinite, Timeout.Infinite);
            try
            {
                sender.Gestionar();
            }
            catch (Exception e)
            {
                // Captura de error de ejecución de la gestión:
            }
            analizadorTimer.Change(analizadorTiempo, analizadorTiempo);
        }

        /// <summary>
        /// Dispose del servicio
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                analizadorTimer.Dispose();
                sender = null;
            }
            base.Dispose(disposing);
        }

    }
}
