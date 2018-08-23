using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MongoDB.Bson;

namespace api_relatorio_transacoes.Models
{
    public class DBContext
    {
        private IConfiguration _configuration;
        private MongoClient client;
        IMongoDatabase db;

        public DBContext(IConfiguration config)
        {
            _configuration = config;
            client = new MongoClient(
                _configuration.GetSection("MongoConnection:ConnectionString").Value);
            // MongoClient client = new MongoClient(
            //     _configuration.GetConnectionString("ConnectionString"));
            db= client.GetDatabase("stone");
        }

        public  List<Transacao> ObterItems<Transacao>()
        {   
            var builder = Builders<Transacao>.Filter;
            var filter = builder.Eq("CardBrandName", "Maestro") & builder.Eq("AmountInCent",3000);
            
            var coll = db.GetCollection<Transacao>("transacoes");
           
            // var cursor = coll.Find(filter).ToList();
            return coll.Find(filter).ToList();
            // List<Transacao> t = new List<Transacao>();
            // foreach (var document in cursor)
            // {
            //     // System.Console.Write(document.ToJson());
            //     t.Add(document);   
            // }
            
            // foreach (var item in t)
            // {
            //     System.Console.Write(item);
            // }


            // // //System.Console.Write(t);
            // return t;

            // return db.GetCollection<Transacao>("transacoes")
            //     .Find(filter).ToList();
            
        }
        public Transacao ObterItem<Transacao>(int codigo)
        {   
            var builder = Builders<Transacao>.Filter;
            // var filter = builder.Eq("CardBrandName", "Maestro") & builder.Eq("AmountInCent",3000);
            var filter = builder.Eq("TransactionId",codigo);
            // var filter = "{'CardBrandName:Maestro','AmountInCent:3000'}";


            return db.GetCollection<Transacao>("transacoes")
                .Find(filter).FirstOrDefault();
        }
    }
}