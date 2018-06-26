using RestauranteApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace RestauranteApi.Controllers
{
    [EnableCors(origins: "*", headers: " * ",methods: "*")]
    public class RestauranteController : ApiController
    {
        private Contexto contexto;

        public RestauranteController() {
            contexto = new Contexto();
        }

        // Inserir Restaurante
        [ResponseType(typeof(Restaurante))]
        [HttpPost]
        public HttpResponseMessage PostRestaurante(Restaurante restaurante) {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Restaurante.Add(restaurante);
                    contexto.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, restaurante);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = restaurante.IdRestaurante }));
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Deletar Restaurante  
        [ResponseType(typeof(Restaurante))]
        public HttpResponseMessage DeleteRestaurante(int id)
        {
            Restaurante restaurante = contexto.Restaurante.Find(id);
            if (restaurante == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
 
            contexto.Restaurante.Remove(restaurante);
 
            try
            {
                contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
 
            return Request.CreateResponse(HttpStatusCode.OK, restaurante);


        }

        //Editar Restaurante
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutRestaurante(int id, Restaurante restaurante)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != restaurante.IdRestaurante)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            contexto.Entry(restaurante).State = EntityState.Modified;

            try
            {
                contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK,restaurante);

        }

        //Verifica se Restaurante Existe de Acordo com id
        private bool RestauranteExiste(int id)
        {
            return contexto.Restaurante.Count(e => e.IdRestaurante == id) > 0;
        }

        //Retorna todos os Restaurantes Cadastrados
        
        public IEnumerable<Restaurante> GetRestaurantes() {
            return (from c in contexto.Restaurante orderby c.Descricao select c).ToList();
        }

        //Retorna Restaurantes Cadastrados de Acordo com o filtro

        public IEnumerable<Restaurante> GetFilterRestaurantes(string filtro)
        {
            return (from c in contexto.Restaurante where c.Descricao.Contains(filtro) orderby c.Descricao select c).ToList();
        }

        public IHttpActionResult GetRestaurante(int id)
        {
            var restaurante =contexto.Restaurante.FirstOrDefault(c => c.IdRestaurante == id);
            if (restaurante == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(restaurante);
            }
        }

    }
}
