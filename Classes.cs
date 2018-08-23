using MongoDB.Bson;

namespace APICatalogo
{
    public class Status
    {
        public string Nome { get; set; }
    }

    public class Transacao
    {
        public ObjectId _id { get; set; }
        public int TransactionId { get; set; }
        public string MerchantCnpj { get; set; }
        public int CheckoutCode { get; set; }
        public string CipheredCardNumber { get; set; }
        public int AmountInCent { get; set; }
        public string AcquirerName { get; set; }
        public string PaymentMethod { get; set; }
        public string CardBrandName { get; set; }
        public string Status { get; set; }
        public string StatusInfo { get; set; }
        public string CreatedAt { get; set; }
        public string AcquirerAuthorizationDateTime { get; set; }


        // public Fornecedor DadosFornecedor { get; set; }
    }

    public class Servico
    {
        public ObjectId _id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public double ValorHora { get; set; }
    }
}