using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class Resposta
    {
        public string msg { get; set; }
        public DateTime ts { get; set; }
    }


    public class TesteController : ApiController
    {
        // GET: api/Teste
        public IEnumerable<string> Get()
        {
            return new string[] { "oi", "oi2" };
        }

        // GET: api/Teste/5
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/Teste/now
        [Route("api/Teste/now")]
        [HttpGet]
        public string GetNow()
        {
            return DateTime.Now.ToString();
        }
        
        // GET: api/Teste/resposta
        [Route("api/Teste/resposta")]
        [HttpGet]
        public string GetResposta()
       
            Resposta resp = new Resposta();
            resp.msg = "teste";
            resp.ts = DateTime.Now;
            string s = JsonConvert.SerializeObject(resp);
            return s;
        }

        // POST: api/Teste
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Teste/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Teste/5
        public void Delete(int id)
        {
        }
    }
}
