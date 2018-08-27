using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using api_relatorio_transacoes.Controllers;
using api_relatorio_transacoes.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace api_relatorio_transacoes_test
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public void TestGetTrasaction()
        {
            var mock = new Mock<DBContext>();
            mock.Setup(d => d.GetItem<Transacao>(It.IsAny<string>())).Returns(new List<Transacao>());
            var control = new TransactionController(mock.Object);
            string cnpj = "123456789";
            Assert.IsNotNull(control.GetTransaction(cnpj));
            // Assert.AreEqual(control.GetTransaction(cnpj), new List<Transacao>());
        }

        [TestMethod]
        public void TestGetCNPJ()
        {
            var mock = new Mock<DBContext>();
            
            mock.Setup(d =>d.GetByType<Transacao>(SearchType.cnpj,It.IsAny<string>())).Returns(new List<Transacao>());
            var control = new TransactionController(mock.Object);

            Assert.IsNotNull(control.GetCNPJ("123456789"));
            // Assert.AreEqual(control.GetCNPJ("123456789"), new List<Transacao>());
        }
    }
}