﻿using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;
namespace api_relatorio_transacoes.Models
{
    //public enum SearchType { cnpj, brandname, acquirer }

    public class DBContext : IDBContext
    {
        private IConfiguration _configuration;
        private MongoClient client;
        IMongoDatabase db;

        public DBContext(IConfiguration config)
        {
            _configuration = config;
            client = new MongoClient(
                _configuration.GetSection("MongoConnection:ConnectionString").Value);
            db= client.GetDatabase("stone");
        }
        public DBContext()
        {
            
        }

        public virtual List<T> GetByType<T>(SearchType type,string elements){

            var lstelement = elements.Split(",");
            var telemnt= "";
            var filter = "";
            foreach (var ielement in lstelement)
            {
                switch (type)
                {
                    case SearchType.cnpj:
                        telemnt+=""+ielement+",";
                        filter = "{MerchantCnpj:{$in:["+telemnt+"]}}";
                        break;
                    case SearchType.brandname:
                        telemnt+="'"+ielement+"',";
                        filter = "{CardBrandName:{$in:["+telemnt+"]}}";
                        break;
                    case SearchType.acquirer:
                        telemnt+="'"+ielement+"',";
                        filter = "{AcquirerName:{$in:["+telemnt+"]}}";
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

        public virtual List<T> GetItem<T>(string filter)
        {   
            var coll = db.GetCollection<T>("transacoes");
            return coll.Find(filter).ToList();
        }
    }
}