using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api_relatorio_transacoes.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private dbContext _contexto;

        public ValuesController(dbContext context)
        {
            _contexto = context;
        }
        // GET api/data/5
        [HttpGet("{a}")]
        public ActionResult<string> Get(string a)
        {
            return "value > "+a;
        }

        // GET api/data/?id=5
        [HttpGet]
        public ActionResult<string> Get(int id=3)
        {
            return "value: "+id;
        }

        
    }

    [Route("api/otro")]
    
    public class MioController : ControllerBase
    {
        // GET api/otro
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // Get api/otro/3
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return  "value2: "+id ;
        }
        
    }
}
