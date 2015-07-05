using System.Linq;
using System.Web.Http;
using SpajamHonsen.Models;
using SpajamHonsen.Models.JsonResponse;

namespace SpajamHonsen.Controllers
{
    public class TweetSummyController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/GetTweetSummy/5
        //[ResponseType(typeof(void))]
        public ExpressInformation GetTweetSummy(string id)
        {
            var express0 = 0;
            if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 0 && item.TweetID != null))
            {
                express0 = db.HVCLog.Count(item => item.SpotID == id && item.Expression == 0 && item.TweetID != null);
            }
            var express1 = 0;
            if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 1 && item.TweetID != null))
            {
                express1 = db.HVCLog.Count(item => item.SpotID == id && item.Expression == 1 && item.TweetID != null);
            }
            var express2 = 0;
            if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 2 && item.TweetID != null))
            {
                express2 = db.HVCLog.Count(item => item.SpotID == id && item.Expression == 2 && item.TweetID != null);
            }
            var express3 = 0;
            if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 3 && item.TweetID != null))
            {
                express3 = db.HVCLog.Count(item => item.SpotID == id && item.Expression == 3 && item.TweetID != null);
            }
            var express4 = 0;
            if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 4 && item.TweetID != null))
            {
                express4 = db.HVCLog.Count(item => item.SpotID == id && item.Expression == 4 && item.TweetID != null);
            }
            var expressInfo = new ExpressInformation()
            {
                Express1 = express0,
                Express2 = express1,
                Express3 = express2,
                Express4 = express3,
                Express5 = express4
            };

            return expressInfo;
        }
    }
}