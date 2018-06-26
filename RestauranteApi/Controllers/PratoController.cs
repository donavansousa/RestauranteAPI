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
    public class PratoController : ApiController
    {
        private Contexto contexto;

        public PratoController() {
            contexto = new Contexto();
        }

        // Inserir Prato
        [ResponseType(typeof(Prato))]
        [HttpPost]
        public HttpResponseMessage PostPrato(Prato prato) {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Prato.Add(prato);
                    contexto.SaveChanges();

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, prato);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = prato.IdPrato }));
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

        // Deletar Prato  
        [ResponseType(typeof(Prato))]
        public HttpResponseMessage DeletePrato(int id)
        {
            Prato prato = contexto.Prato.Find(id);
            if (prato == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
 
            contexto.Prato.Remove(prato);
 
            try
            {
                contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
 
            return Request.CreateResponse(HttpStatusCode.OK, prato);


        }

        //Editar Prato
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutPrato(int id, Prato prato)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != prato.IdPrato)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            prato.Restaurante = contexto.Restaurante.Find(prato.IdRestaurante);
            contexto.Entry(prato).State = EntityState.Modified;

            try
            {
                contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK,prato);

        }

        //Verifica se Prato Existe de Acordo com id
        private bool PratoExiste(int id)
        {
            return contexto.Prato.Count(e => e.IdPrato == id) > 0;
        }

        //Retorna todos os Pratos Cadastrados
        
        public IEnumerable<Prato> GetPratos() {
            return (from c in contexto.Prato.Include("Restaurante") orderby c.Restaurante.Descricao select c).ToList();
        }

        //Retorna Pratos Cadastrados de Acordo com o filtro

        public IEnumerable<Prato> GetFilterPratos(string filtro)
        {
            return (from c in contexto.Prato.Include("Restaurante")  where c.Descricao.Contains(filtro) || c.Restaurante.Descricao.Contains(filtro)  orderby c.Restaurante.Descricao select c).ToList();
        }

        public IHttpActionResult GetPrato(int id)
        {
            var prato =contexto.Prato.Include("Restaurante").FirstOrDefault(c => c.IdPrato == id);
            if (prato == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prato);
            }
        }

    }
}
