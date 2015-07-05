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
            var express5 = 0;
            if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 5 && item.TweetID != null))
            {
                express5 = db.HVCLog.Count(item => item.SpotID == id && item.Expression == 5 && item.TweetID != null);
            }
            var expressInfo = new ExpressInformation()
            {
                Express1 = express1,
                Express2 = express2,
                Express3 = express3,
                Express4 = express4,
                Express5 = express5
            };

            return expressInfo;
        }
    }
}