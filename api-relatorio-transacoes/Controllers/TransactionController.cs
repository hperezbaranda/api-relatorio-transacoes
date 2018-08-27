using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api_relatorio_transacoes.Models;
using MongoDB.Bson;

namespace api_relatorio_transacoes.Controllers
{ 
    
    [Route("api/trans/")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private DBContext _contexto;

        public TransactionController(DBContext context)
        {
            _contexto = context;
        }

        // GET api/trans/?checkout=5,cnpj=6 ...
        [HttpGet]
        public ActionResult GetTransaction(string cnpj="",string amount="", string acqname="",
                 string brandname="", string status="", string data="", int days=0 )
        {
            string filter = "{";
            if(cnpj != ""){
                var lstelement = cnpj.Split(",");
                var telemnt= "";
                var tfilter ="";
                foreach (var ielement in lstelement){
                    telemnt+=""+ielement+",";
                    tfilter = "MerchantCnpj:{$in:["+telemnt+"]},";
                }
                filter +=tfilter;
            }
            if(amount !=""){
                var lstelement = amount.Split(",");
                var telemnt= "";
                var tfilter ="";
                foreach (var ielement in lstelement){
                    telemnt+=""+ielement+",";
                    tfilter = "AmountInCent:{$in:["+telemnt+"]},";
                }
                filter +=tfilter;
            }
            if(acqname !=""){
                var lstelement = acqname.Split(",");
                var telemnt= "";
                var tfilter ="";
                foreach (var ielement in lstelement){
                    telemnt+=""+ielement+",";
                    tfilter = "AcquirerName:{$in:["+telemnt+"]},";
                }
                filter +=tfilter;
            }
            if(brandname !=""){
                var lstelement = brandname.Split(",");
                var telemnt= "";
                var tfilter ="";
                foreach (var ielement in lstelement){
                    telemnt+=""+ielement+",";
                    tfilter = "CardBrandName:{$in:["+telemnt+"]},";
                }
                filter +=tfilter;
            }
            if(status !=""){
                var lstelement = status.Split(",");
                var telemnt= "";
                var tfilter ="";
                foreach (var ielement in lstelement){
                    telemnt+=""+ielement+",";
                    tfilter = "Status:{$in:["+telemnt+"]},";
                }
                filter +=tfilter;
            }
            if(data !=""){
                var data2 = new DateTime(Int32.Parse(data.Split("-")[0]),Int32.Parse(data.Split("-")[1])
                            ,Int32.Parse(data.Split("-")[2])+1).ToString("yyyy'-'MM'-'dd");;
                filter +=" CreatedAt: { $gte: ISODate('"+data+"'),$lt: ISODate('"+data2+"')} ";
            }else if(days != 0 ){
                TimeSpan diff = new TimeSpan((24*days), 00, 0);
           
                var datanow = DateTime.Now.AddDays(1);
                string data1 = datanow.Subtract(diff).ToString("yyyy'-'MM'-'dd");
                filter +=" CreatedAt: { $gte: ISODate('"+data1+"'),$lt: ISODate('"+datanow.ToString("yyyy'-'MM'-'dd")+"')} ";
            }
            filter +="}";
            List<Transacao> trans = new List<Transacao>();
            trans = _contexto.GetItem<Transacao>(filter);
        
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound("Nenhum resultado para essa Busca");
        }

        //GET api/trans/cnpj/13123213,666666
        //GET api/trans/cnpj/13123213
        [HttpGet("cnpj/{ids}")]
        public ActionResult GetCNPJ(string ids){
            List<Transacao> trans = new List<Transacao>();
            trans = _contexto.GetByType<Transacao>(SearchType.cnpj, ids);
            
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound("Nenhum resultado para essa Busca");
        }

        //GET api/trans/brandname/name1,name2
        //GET api/trans/brandname/name1
        [HttpGet("brandname/{names}")]
        public ActionResult GetBrandName(string names){
            
            List<Transacao> trans = new List<Transacao>();
            trans = _contexto.GetByType<Transacao>(SearchType.brandname, names);
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound("Nenhum resultado para essa Busca");
        }

        //GET api/trans/adquirente/name1,name2
        //GET api/trans/adquirente/name1
        [HttpGet("acquirer/{names}")]
        public ActionResult GetAcquirer(string names){
            
            List<Transacao> trans = new List<Transacao>();
            trans = _contexto.GetByType<Transacao>(SearchType.acquirer, names);
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound("Nenhum resultado para essa Busca");
        }

        //GET api/trans/data/2018-05-56
        //GET api/trans/data/2018-12-30,2089-02-29
        [HttpGet("data/{pdata}")]
        public ActionResult GetData(string pdata){
            List<Transacao> trans = new List<Transacao>();
            
            foreach (var item in pdata.Split(","))
            {
                var data1 = pdata.Split(",")[0];
                var data2 = new DateTime(Int32.Parse(item.Split("-")[0]),Int32.Parse(item.Split("-")[1]),Int32.Parse(item.Split("-")[2])+1)
                        .ToString("yyyy'-'MM'-'dd");
                trans =trans.Concat(_contexto.GetByData<Transacao>(data1,data2)).ToList();               
            }
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound("Nenhum resultado para essa Busca");
        }

        //GET api/trans/last/3
        [HttpGet("last/{days}")]
        public ActionResult GetLastDay(int days){
            List<Transacao> trans = new List<Transacao>();
            TimeSpan diff = new TimeSpan((24*days), 00, 0);
           
            var datanow = DateTime.Now.AddDays(1);
            string data = datanow.Subtract(diff).ToString("yyyy'-'MM'-'dd");
            trans =_contexto.GetByData<Transacao>(data,datanow.ToString("yyyy'-'MM'-'dd"));               

            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound("Nenhum resultado para essa Busca");
        }

    }

}
