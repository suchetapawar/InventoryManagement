using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TestApi.Models;

namespace TestApi.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IProductDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        public List<Product> Get() =>
            _products.Find(x => true).ToList();

        public Product Get(string id) =>
            _products.Find<Product>(x => x.Id == id).FirstOrDefault();

        public Product Create(Product prod)
        {
            _products.InsertOne(prod);
            return prod;
        }

        public void Update(string id, Product x) =>
            _products.ReplaceOne(prod => prod.Id == id, x);

        public void Remove(Product x) =>
            _products.DeleteOne(prod => prod.Id == x.Id);

        public void Remove(string id) =>
            _products.DeleteOne(prod => prod.Id == id);

        public ProductService()
        {
        }
    }
}

