using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace api_relatorio_transacoes
{
    public class dbContext
    {
        private IConfiguration _configuration;

        public dbContext(IConfiguration config)
        {
            _configuration = config;
        }

        public T ObterItem<T>(string codigo)
        {
            MongoClient client = new MongoClient(
                _configuration.GetConnectionString("ConnectionString"));
            IMongoDatabase db = client.GetDatabase("stone");

            var filter = Builders<T>.Filter.Eq("TransactionId", codigo);

            return db.GetCollection<T>("transacoes")
                .Find(filter).FirstOrDefault();
        }
    }
}