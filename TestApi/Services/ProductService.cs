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

        //private List<Product> products = new List<Product>();

        public ProductService(IProductDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            //var server = client.Get
            var database = client.GetDatabase("ECommerceDb");

            //_products = client.GetDatabase("ECommerceDb").GetCollection<Product>(settings.ProductCollectionName);
            //_products = client.GetDatabase("ECommerceDb").GetCollection<Product>("ProductCatalog");
            // _list = database.get
            /*var docs = _products.Find(new BsonDocument()).ToEnumerable();
            foreach (var doc in docs)
            {
                String name = doc.Name;
                String id = doc.Id;

            }*/

            foreach (var item in database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                //Console.WriteLine(item.ToString());
                _list.Add(item.ToString());
            }

            _products = database.GetCollection<Product>("ProductCatalog");
        }

        public List<string> Get()
        { 
            foreach (var item in _products.Find(x => true).ToList())
            {
                //Console.WriteLine(item.ToString());
                _list.Add(item.Name);
            }
            return _list;
        }
    //_products.Find(x => true).ToList();

        //public List<string>  Get() =>
         //   _list;

         /*{   var docs = _products.Find(new BsonDocument()).ToEnumerable();
            foreach (var doc in docs)
            {
                products.Add(doc);
                
            }
            return products.Count;
            }*/

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

