using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VgFc.Test;
using VgFc.Test.Model;

namespace VgFc.Test.Repository
{
    /// <summary>
    /// Works with EF model and returns DTOs objects
    /// </summary>
    public class Product
    {
        VentasEntities db = new VentasEntities();

        public bool Exists(int key)
        {
            try
            {
                    return db.Products.Any(p => p.Id == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Dto.Product> Get()
        {
            try
            {
                    var dtoProducts = from prd in db.Products
                                      select new Dto.Product
                                      {
                                          Id = prd.Id,
                                          Name = prd.Name,
                                          Price = prd.Price
                                      };
                    return dtoProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Dto.Product> Get(int key)
        {
            try
            {
                    var dtoProduct = from prd in db.Products
                                     where prd.Id==key
                                     select new Dto.Product
                                     {
                                          Id = prd.Id,
                                          Name = prd.Name,
                                          Price = prd.Price
                                     };
                    return dtoProduct;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Save(Dto.Product product)
        {
            try
            {
                Model.Product newProduct = new Model.Product()
                {
                    Name = product.Name,
                    Price = product.Price
                };
                db.Products.Add(newProduct);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(int key, Dto.Product product)
        {
            try
            {
                //db.Products.Add(product);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Dto.Product product)
        {
            try
            {
                //db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}