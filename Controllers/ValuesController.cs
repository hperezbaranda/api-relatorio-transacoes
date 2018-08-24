using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api_relatorio_transacoes.Models;
using MongoDB.Bson;

namespace api_relatorio_transacoes.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DBContext _contexto;

        public ValuesController(DBContext context)
        {
            _contexto = context;
        }

       
        // GET api/data/5
        [HttpGet("{a}")]
        public ActionResult<string> Get(int a)
        {
            return "value > "+a;
        }

        // GET api/data/?id=5,time=6
        [HttpGet]
        public ActionResult<Transacao> GetTransacao(int id=-1,long cnpj=-1,int checkout=-1
                , string cardnum="" ,int amount=-1, int inst=-1, string acqname="",
                string paymethod="", string brandname="", string status="", string statusinf ="",
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

        
    }



    [Route("api/otro")]
    
    public class MioController : ControllerBase
    {
         private DBContext _contexto;

        public MioController(DBContext context)
        {
            _contexto = context;
        }

        // GET api/otro
        [HttpGet]
        public ActionResult<Transacao> GetAll()
        {
            List<Transacao> trans = null;
            trans = _contexto.ObterItems<Transacao>();
            if (trans != null)
                return new JsonResult(trans);
            else
                return NotFound();
            // return new  JsonResult(_contexto.ObterItems<Transacao>());
        }


        // Get api/otro/3
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return  "value2: "+id ;
        }
        
    }
}
