using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class Resposta
    {
        public string msg { get; set; }
        public DateTime ts { get; set; }
    }

    public class Dolar
    {
        public string nome { get; set; }
        public float valor { get; set; }
        public int ultima_consulta { get; set; }
        public string fonte { get; set; }
        public DateTime ts { get; set; }
    }

    public class Cotacao
    {
        public bool status { get; set; }
        public List<Dolar> valores { get; set; }
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
        public string GetResposta() {

            Resposta resp = new Resposta
            {
                msg = "teste",
                ts = DateTime.Now
            };

            string s = JsonConvert.SerializeObject(resp);
            return s;
        }

        //
        // GET: api/Teste/resposta
        [Route("api/Teste/dolar")]
        [HttpGet]
        public async Task<Dolar> GetDolar()
        {
            string path = "http://api.promasters.net.br/cotacao/v1/valores";
            HttpClient client = new HttpClient();
            Dolar doleta = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string ret = await response.Content.ReadAsStringAsync();
                JObject googleSearch = JObject.Parse(ret);
                doleta = googleSearch["valores"]["USD"].ToObject<Dolar>();

                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                doleta.ts = dtDateTime.AddSeconds(doleta.ultima_consulta).ToLocalTime();
            }
/*
            //var t1 = service.getdata1Async();
            //var t2 = service.getdata2Async();
            //var t3 = service.getdata3Async();
            //await Task.WhenAll(t1, t2, t3);

            var data = new returnObject
            {
                d1 = await t1,
                d2 = await t2,
                d3 = await t3
            };

            return Ok(data); */
            return doleta;
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
