using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SpajamHonsen.Utilities;

namespace SpajamHonsen.Controllers
{
    public class BaiduTranslateController : ApiController
    {
        public async Task<string> GetTranslateAsync(string text)
        {
            return await BaiduUtil.RequestBaiduTranslateAPIAsync(text);
        }
    }
}
