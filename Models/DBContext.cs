using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MongoDB.Bson;

namespace api_relatorio_transacoes.Models
{
    public enum SearchType { cnpj, brandname, acquirer }

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

        public List<T> GetByType<T>(SearchType type,string elements){

            var lstelement = elements.Split(",");
            var telemnt= "";
            var filter = "";
            foreach (var ielement in lstelement)
            {
                switch (type)
                {
                    case SearchType.cnpj:
                        telemnt+=""+ielement+",";
                        filter = "{MerchantCnpj:{$in:["+telemnt+"]},}";
                        break;
                    case SearchType.brandname:
                        telemnt+="'"+ielement+"',";
                        filter = "{CardBrandName:{$in:["+telemnt+"]},}";
                        break;
                    case SearchType.acquirer:
                        telemnt+="'"+ielement+"',";
                        filter = "{AcquirerName:{$in:["+telemnt+"]},}";
                        break;
                }
            }

            var coll = db.GetCollection<T>("transacoes");
            return coll.Find(filter).ToList();
        }

        public  List<T> GetByData<T>(string data1,string data2)
        {   
            var builder = Builders<T>.Filter;
            var filter = "{ CreatedAt: { $gte: ISODate('"+data1+"'),$lt: ISODate('"+data2+"')} }";
            var coll = db.GetCollection<T>("transacoes");
            return coll.Find(filter).ToList();            
        }

        public List<T> ObterItem<T>(string brand)
        {   
            // System.Console.WriteLine(brand);
            // var filter1 ="";
            var lstbrand = brand.Split(",");
            // System.Console.WriteLine(brand);
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

            var coll = db.GetCollection<T>("transacoes");

            // System.Console.WriteLine(filter1);

            return coll.Find(filter1).ToList();
        }
    }
}