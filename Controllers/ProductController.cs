using WebAPI_SD2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebAPI_SD2.Controllers
{
    public class ProductController : ApiController
    {
        //List<Product> products = new List<Product>
        //{
        //    new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //    new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //    new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        //};

        private string caminhoArquivo = "";

        //Propriedade
        List<Product2> products;// { get => Desserializar(); set => Serializar(value); }

        //private List<Product> Desserializar() { }
        //private void Serializar(List<Product> lista)
        //{
        //    //FileStream stream = new FileStream()
        //}

        // GET api/<controller>
        public IEnumerable<Product2> GetAllProducts()
        {
            return products;
        }

        // GET api/<controller>/5
        public IHttpActionResult GetProduct(int id)
        {
            var product = BuscarProduto(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        private Product2 BuscarProduto(int id)
        {
            //var product = products.FirstOrDefault((p) => p.Id == id);
            return (from p in products
                    where p.Id == id
                    select p).FirstOrDefault();
        }

        // POST api/<controller>
        [HttpPost]
        [ResponseType(typeof(Product2))]
        public IHttpActionResult Post([FromBody]Product2 produto)
        {
            var p = BuscarProduto(produto.Id);
            if(p != null)
            {
                return BadRequest();
            }
            //Adiciona o novo objeto à lista:
            products.Add(produto);
            
            return Ok(produto);
            //return Created<Product>(new Uri(Url.Link("DefaultApi", new { id = produto.Id })), produto);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [ResponseType(typeof(Product2))]
        public IHttpActionResult Put(int id, [FromBody]Product2 produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }
            //Tenta localizar o Produto pelo id
            Product2 p = BuscarProduto(id);
            if(p == null)
            {
                return NotFound();
            }
            //Altera os campos do produto a ser atualizado:
            p.Name = produto.Name;
            p.Category = produto.Category;
            p.Price = produto.Price;

            return Ok(p);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var p = BuscarProduto(id);
            if(p == null)
            {
                return NotFound();
            }
            //Remove o objeto da lista:
            products.Remove(p);

            return Ok();
        }
    }
}