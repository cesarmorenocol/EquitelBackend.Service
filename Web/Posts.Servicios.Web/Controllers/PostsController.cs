using Analizador.Servicio.Negocio;
using Comun.Servicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Posts.Servicios.Web.Controllers
{
    public class PostsController : ApiController
    {
        // GET: api/Personas
        public IQueryable<Post> GetPosts()
        {
            return null;
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = null;
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, Post post)
        {
            // Save Post:
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personas
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(Post post)
        {
            return await Task<IHttpActionResult>.Factory.StartNew(() =>
            {
                int postId;
                try
                {
                    AdministradorPosts handler = new AdministradorPosts();
                    postId = handler.CrearPost(post);
                    return CreatedAtRoute("DefaultApi", new { id = postId }, post);
                }
                catch
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //
            }
            base.Dispose(disposing);
        }
    }

}
