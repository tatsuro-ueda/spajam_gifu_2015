using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpajamHonsen.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public async Task<string> GetTalk(string id)
        {
            var url = "http://openapi.baidu.com/public/2.0/bmt/translate";
            var httpClient = new HttpClient();

            var uri = new Uri(url);

            var param = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", "xRhWEdqmUx5A7cX2HGhMUWDl" },
                { "q", "today" },
                { "from", "auto" },
                { "to", "auto" },
            });

            var result = await httpClient.PostAsync(uri, param);

            return await result.Content.ReadAsStringAsync();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
