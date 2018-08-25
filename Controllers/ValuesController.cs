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
    public class ValuesController : ControllerBase
    {
        private DBContext _contexto;

        public ValuesController(DBContext context)
        {
            _contexto = context;
        }

        // GET api/trans/?id=5,cnpj=6
        [HttpGet]
        public ActionResult<Transacao> GetTransacao(int id=-1,long cnpj=-1,int checkout=-1
                , string cardnum="" ,int amount=-1, int inst=-1, string acqname="",
                string paymethod="", string brandname="Visa", string status="", string statusinf ="",
                 DateTime crated=new DateTime(),DateTime adquire = new DateTime() )
        {
            // System.Console.WriteLine(test.Split(",")[0]);
            List<Transacao> trans = null;
            trans = _contexto.ObterItem<Transacao>(brandname);
            // return "value: "+id;
        
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound();
        }

        //GET api/trans/cnpj/13123213,666666
        //GET api/trans/cnpj/13123213
        [HttpGet("cnpj/{ids}")]
        public ActionResult<Transacao> GetCNPJ(string ids){
            // DateTime date1 = new DateTime(1996, 6, 3, 22, 15, 0);
            // DateTime date2 = new DateTime(1996, 12, 6, 13, 2, 0);
            // TimeSpan diff = new TimeSpan((24*30), 30, 0);
            // var d = date2.Subtract(diff);
            // return new ObjectResult(d);
            List<Transacao> trans = null;
            trans = _contexto.GetByType<Transacao>(SearchType.cnpj, ids);
            Console.WriteLine(trans);
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound();
        }

        //GET api/trans/brandname/name1,name2
        //GET api/trans/brandname/name1
        [HttpGet("brandname/{names}")]
        public ActionResult<Transacao> GetBrandName(string names){
            
            List<Transacao> trans = null;
            trans = _contexto.GetByType<Transacao>(SearchType.brandname, names);
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound();
        }

        //GET api/trans/adquirente/name1,name2
        //GET api/trans/adquirente/name1
        [HttpGet("acquirer/{names}")]
        public ActionResult<Transacao> GetAcquirer(string names){
            
            List<Transacao> trans = null;
            trans = _contexto.GetByType<Transacao>(SearchType.acquirer, names);
            if (trans.Count > 0)
                return Ok(new{results=trans});
            else
                return NotFound();
        }

        //GET api/trans/data/2018-05-56
        //GET api/trans/data/2018-12-30,2089-02-29
        [HttpGet("data/{pdata}")]
        public ActionResult<Transacao> GetData(string pdata){
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
                return NotFound();
        }

    }

}
