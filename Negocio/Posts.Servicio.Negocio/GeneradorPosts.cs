using Comun.Servicios.Datos;
using Comun.Servicios.Entidades;
using Comun.Servicios.Transversales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Servicio.Negocio
{
    public class GeneradorPosts : IDisposable
    {
        #region Variables de clase

        private BackgroundWorker bgWorker;
        private Queue<Post> queue = new Queue<Post>();
        private PostRepositorio repositorio;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GeneradorPosts()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~GeneradorPosts()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se ejecuta cuando se inicia la tarea de forma asíncrona
        /// </summary>
        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Se procesan todos los elementos pendientes de la cola
            while (queue.Any())
            {
                int tareasActuales = 0;
                int numeroSolicitudes = 1000;
                List<Task> tareasPendientes = TareasPendientes(tareasActuales, numeroSolicitudes).ToList();
                // Finalizar el procesamiento de las tareas:
                try
                {
                    Task.WaitAll(tareasPendientes.ToArray());
                }
                catch
                {
                    // Incluir la captura de error:
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta al finalizar la tarea
        /// </summary>
        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
            }
            else if (e.Cancelled)
            {
            }
            else
            {
                if (Convert.ToBoolean(e.Result))
                {
                }
            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Ejecución de tareas asociadas al llenado e inicializacion de la cola
        /// </summary>
        public void Gestionar()
        {
            GestionarCola();
            IniciarGestionCola();
        }

        /// <summary>
        /// Inicia la verificación de nuevos elementos para que sean incluidos en la cola y posteriormente procesados
        /// </summary>
        private void GestionarCola()
        {
            if (!bgWorker.IsBusy && !queue.Any())
            {
                try
                {
                    // Generar los post:
                    List<Post> posts = new List<Post>();
                    for (int i = 0; i < 100; i++)
                    {
                        var postGenerado = new Post() { Descripcion = Utiles.GenerarPost() };
                        posts.Add(postGenerado);
                    }
                    // Ponerlos en la cola:
                    if (posts.Any())
                    {
                        foreach (var post in posts)
                        {
                            // Cargar elemento a la cola si no existe. Es posible que esté pendiente por procesamiento
                            if (!queue.Where(p => p.Id == p.Id).Any())
                                queue.Enqueue(post);
                        }
                    }

                }
                catch
                {
                    // Incluir la captura de error:
                }
            }
        }

        /// <summary>
        /// Obtiene el próximo elemento de la cola
        /// </summary>
        private Post ObtenerSiguienteElemento()
        {
            Post post = queue.Dequeue();
            return post;
        }

        /// <summary>
        /// Inicia la tarea de envío de notificaciones si exiten en cola
        /// </summary>
        private void IniciarGestionCola()
        {
            if (!bgWorker.IsBusy && queue.Any())
            {
                bgWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Permite invocar el procesamiento del post
        /// </summary>
        private Task ProcesarPost(Post post)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    repositorio = new PostRepositorio();
                    if (!Equals(post, null))
                    {
                        repositorio.CrearPost(post);
                    }
                }
                catch
                {
                    repositorio.ActualizarEstadoPost(post.Id, EstadoPost.Creado);
                }
            });
        }

        /// <summary>
        /// Permite encolar en un List<Task> las solicitudes pendientes para los post
        /// </summary>
        private IEnumerable<Task> TareasPendientes(int tareasActuales, int numeroSolicitudes)
        {
            List<Task> tareasPendientes = new List<Task>();
            while (tareasActuales < numeroSolicitudes && queue.Any())
            {
                try
                {
                    Post elementoActual = ObtenerSiguienteElemento();
                    Task tareaActual = ProcesarPost(elementoActual);
                    tareasPendientes.Add(tareaActual);
                    tareasActuales++;
                }
                catch
                {
                    // Incluir la captura de error:
                }
            }
            return tareasPendientes;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Liberar recursos al finalizar el uso del objeto
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Descarga los objetos completamente
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (bgWorker != null)
                {
                    bgWorker.Dispose();
                    bgWorker = null;
                }
                if (!Equals(repositorio, null))
                    repositorio.Dispose();
            }
        }

        #endregion
    }
}
