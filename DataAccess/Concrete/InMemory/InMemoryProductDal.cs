using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal :IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>()
            {
                new Product(){ProductId = 1,CategoryId = 1, ProductName = "Elma", UnitPrice = 25, UnitsInStock = 35},
                new Product(){ProductId = 2,CategoryId = 1, ProductName = "Kivi", UnitPrice = 60, UnitsInStock = 65},
                new Product(){ProductId = 3,CategoryId = 1, ProductName = "Muz", UnitPrice = 45, UnitsInStock = 21},
                new Product(){ProductId = 4,CategoryId = 1, ProductName = "Greyfurt", UnitPrice = 50, UnitsInStock = 8},
                new Product(){ProductId = 5,CategoryId = 1, ProductName = "Portakal", UnitPrice = 20, UnitsInStock = 14}
            };
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            // where aldığı koşula uyan tüm dataları alır ve listeye çevirip geriye döndürür.
           return _products.Where(p => p.CategoryId == categoryId).ToList();

        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            // Gönderdiğim product nesnesini listede bul ve bana dön.Burada SingleOrDefault 2 tane eşleştirme bulursa hata verir.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName  = product.ProductName;
            productToUpdate.UnitPrice= product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.CategoryId = product.CategoryId;


        }

        public void Delete(Product product)
        {
            //Product productToDelelete = null;

            //LINQ kullanmadan bu format kullanabilirdik.
            /*
             foreach (var p in _products)
            {
                if (product.ProductId == p.ProductId)
                {
                    productToDelelete = p;
                }
            }
            */

            //LINQ kullanarak yaptığımız işlem aslında yukarıda yaptığımızın aynısı.

           Product productToDelelete = _products.SingleOrDefault(p=> p.ProductId == product.ProductId);

            _products.Remove(productToDelelete);

        }
    }
}
