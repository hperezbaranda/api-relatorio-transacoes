using System.Linq;
using MongoDB;
using Microsoft.Extensions.Configuration;

namespace api_relatorio_transacoes
{
    public class TransacoesContext
    {
        private IConfiguration _configuration;
        public TransacoesContext(IConfiguration config)
        {
            _configuration = config;
        }

        public T ObterItem<T>(string codigo)
        {
            MongoClient  client = new MongoClient(
                _configuration.GetConnectionString("ConexaoCatalogo"));
            MongoDatabase db = client.GetDatabase("DBCatalogo");

            var filter = Builders<T>.Filter.Eq("Codigo", codigo);

            return db.GetCollection<T>("Catalogo")
                .Find(filter).FirstOrDefault();
        }
    }

}