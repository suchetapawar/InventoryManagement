using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TestApi.Models;
using MongoDB.Bson;

namespace TestApi.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        private List<string> _list = new List<string>();

        private BsonDocument _doc;
        //private List<Product> products = new List<Product>();

        public ProductService(IProductDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            //var server = client.Get
            var database = client.GetDatabase("ECommerceDb");

           

            /*foreach (var item in database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                //Console.WriteLine(item.ToString());
                _list.Add(item.ToString());
            }

            var command = new BsonDocument { { "ProductCatalog", 1 } };
            _doc = database.RunCommand<BsonDocument>(command);*/
            //Console.WriteLine(result.ToJson());

            _products = database.GetCollection<Product>("ProductCatalog");
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

