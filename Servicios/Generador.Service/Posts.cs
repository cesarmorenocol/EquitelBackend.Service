using Posts.Servicio.Negocio;
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

namespace EquitelBackend.Service
{
    public partial class Posts : ServiceBase
    {
        #region Variables de clase

        // Temporizadores
        int generadorTiempo = 1000;
        int enviadorTiempo = 1000;
        // Timers
        Timer generadorTimer;
        Timer enviadorTimer;

        EnviadorPosts sender = null;

        #endregion

        public Posts()
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
                sender = new EnviadorPosts();
                // Inicializar timer de revisión de nuevas imágenes.
                enviadorTimer = new System.Threading.Timer(new TimerCallback(SenderTimerTick), null, 0, enviadorTiempo);
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
            enviadorTimer.Change(Timeout.Infinite, Timeout.Infinite);
            try
            {
                sender.Gestionar();
            }
            catch (Exception e)
            {
                // Captura de error de ejecución de la gestión:
            }
            enviadorTimer.Change(enviadorTiempo, enviadorTiempo);
        }

        /// <summary>
        /// Dispose del servicio
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                generadorTimer.Dispose();
                enviadorTimer.Dispose();
                sender = null;
            }
            base.Dispose(disposing);
        }
    }
}
