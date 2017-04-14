using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using VgFc.Test;

namespace VgFc.Test.Services.Controllers
{
    public class ProductsController : ODataController
    {
        private Test.Repository.Product _product = new Test.Repository.Product();

        protected override void Dispose(bool disposing)
        {
            _product = null;
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            try
            {
                return _product.Exists(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [EnableQuery(AllowedOrderByProperties = "Name,Price")]
        public IQueryable<Dto.Product> Get()
        {
            try
            {
                return _product.Get();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [EnableQuery]
        public SingleResult<Dto.Product> Get([FromODataUri] int key)
        {
            try
            {
                return SingleResult.Create(_product.Get(key));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IHttpActionResult> Post(Dto.Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _product.Save(product);
                return Created(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Dto.Product> product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var originalProduct = _product.Get(key).First();
                if (originalProduct == null)
                    return NotFound();
                product.Patch(originalProduct);
                await _product.Update(key, originalProduct);
                return Updated(originalProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            try
            {
                var eraseProduct = _product.Get(key).First();
                if (eraseProduct == null)
                    return NotFound();
                await _product.Delete(eraseProduct);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}