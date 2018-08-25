using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
// using Newtonsoft.Json;

namespace api_relatorio_transacoes.Models
{
    [BsonIgnoreExtraElements]
    public class Transacao
    {
        // public ObjectId _id { get; set; }   
        
        // public int TransactionId { get; set; }
        [BsonElement("MerchantCnpj")]
        [BsonIgnoreIfNull]
        public BsonInt64 MerchantCnpj { get; set; }
        [BsonElement("CheckoutCode")]
        [BsonIgnoreIfNull]
        public int CheckoutCode { get; set; }
        [BsonElement("CipheredCardNumber")]
        [BsonIgnoreIfNull]
        public string CipheredCardNumber { get; set; }
        [BsonElement("AmountInCent")]
        [BsonIgnoreIfNull]
        public int AmountInCent { get; set; }
        [BsonElement("Installments")]
        [BsonIgnoreIfNull]
        public int Installments { get; set; }
        [BsonElement("AcquirerName")]
        [BsonIgnoreIfNull]
        public string AcquirerName { get; set; }
        [BsonElement("PaymentMethod")]
        [BsonIgnoreIfNull]
        public string PaymentMethod { get; set; }
        [BsonElement("CardBrandName")]
        [BsonIgnoreIfNull]
        public string CardBrandName { get; set; }
        [BsonElement("Status")]
        [BsonIgnoreIfNull]
        public string Status { get; set; }
        [BsonElement("StatusInfo")]
        [BsonIgnoreIfNull]
        public string StatusInfo { get; set; }
        [BsonElement("CreatedAt")]
        [BsonIgnoreIfNull]
        public BsonDateTime CreatedAt { get; set; }
        [BsonElement("AcquirerAuthorizationDateTime")]
        [BsonIgnoreIfNull]
        public BsonDateTime AcquirerAuthorizationDateTime { get; set; }
        
    }


}