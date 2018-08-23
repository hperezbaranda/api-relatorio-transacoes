using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace api_relatorio_transacoes.Models
{
    public class Transacao
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public ObjectId _id { get; set; }    
        [BsonElement("TransactionId")]
        public int TransactionId { get; set; }
        [BsonElement("MerchantCnpj")]
        public BsonInt64 MerchantCnpj { get; set; }
        [BsonElement("CheckoutCode")]
        public int CheckoutCode { get; set; }
        [BsonElement("CipheredCardNumber")]
        public string CipheredCardNumber { get; set; }
        [BsonElement("AmountInCent")]
        public int AmountInCent { get; set; }
        [BsonElement("Installments")]
        public int Installments { get; set; }
        [BsonElement("AcquirerName")]
        public string AcquirerName { get; set; }
        [BsonElement("PaymentMethod")]
        public string PaymentMethod { get; set; }
        [BsonElement("CardBrandName")]
        public string CardBrandName { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }
        [BsonElement("StatusInfo")]
        public string StatusInfo { get; set; }
        [BsonElement("CreatedAt")]
        public string CreatedAt { get; set; }
        [BsonElement("AcquirerAuthorizationDateTime")]
        public string AcquirerAuthorizationDateTime { get; set; }
        


        // public Fornecedor DadosFornecedor { get; set; }
    }


}