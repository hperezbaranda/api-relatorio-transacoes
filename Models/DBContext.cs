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
            // return trans.Where(t=>t.TransactionId == id).Select(t=>t).FirstOrDefault();
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
        public List<Transacao> ObterItem<Transacao>(string brand)
        {   
            // System.Console.WriteLine(brand);
            // var filter1 ="";
            var lstbrand = brand.Split(",");
            var brands= "";
            
            foreach (var ibrand in lstbrand)
            {
                brands+="'"+ibrand+"',";
            }
            var filter1 = "{CardBrandName:{$in:["+brands+"]},}";
            
            // var builder = Builders<Transacao>.Filter;
            // var filter = builder.Eq("CardBrandName", "Maestro") & builder.Eq("CardBrandName", "Visa");
            // var filter = builder.In("CardBrandName", "Visa");
            // var filter = "{CardBrandName:'Maestro',AmountInCent:3000}";
            // var filter = "{CardBrandName:{$in:['Maestro','Visa']}}";
            // var filter = "{CardBrandName:{$in:['Maestro','Visa',]}}";

            var coll = db.GetCollection<Transacao>("transacoes");

            // System.Console.WriteLine(filter1);

            return coll.Find(filter1).ToList();
        }
    }
}