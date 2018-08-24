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
        
            if (trans != null)
                return new JsonResult(trans);
            else
                return NotFound();
        }

        //GET api/trans/cnpj/13123213,666666
        //GET api/trans/cnpj/13123213
        [HttpGet("cnpj/{ids}")]
        public ActionResult<string> GetCNPJ(string ids){
            
            List<Transacao> trans = null;
            trans = _contexto.GetByType<Transacao>(SearchType.cnpj, ids);
            if (trans != null)
                return new JsonResult(trans);
            else
                return NotFound();
        }

        //GET api/trans/brandname/name1,name2
        //GET api/trans/brandname/name1
        [HttpGet("brandname/{names}")]
        public ActionResult<string> GetBrandName(string names){
            
            List<Transacao> trans = null;
            trans = _contexto.GetByType<Transacao>(SearchType.brandname, names);
            if (trans != null)
                return new JsonResult(trans);
            else
                return NotFound();
        }

        //GET api/trans/adquirente/name1,name2
        //GET api/trans/adquirente/name1
        [HttpGet("acquirer/{names}")]
        public ActionResult<string> GetAcquirer(string names){
            
            List<Transacao> trans = null;
            trans = _contexto.GetByType<Transacao>(SearchType.acquirer, names);
            if (trans != null)
                return new JsonResult(trans);
            else
                return NotFound();
        }

    }

}
