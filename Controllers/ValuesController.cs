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

        // GET api/data/?id=5
        [HttpGet]
        public ActionResult<string> GetTransacao(int id=3)
        {
            Transacao trans = null;
            trans = _contexto.ObterItem<Transacao>(id);
            // return "value: "+id;
            if (trans != null)
                return new ObjectResult(trans);
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
        public ActionResult<string> GetAll()
        {
            return new ObjectResult(_contexto.ObterItems<Transacao>());
        }


        // Get api/otro/3
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return  "value2: "+id ;
        }
        
    }
}
