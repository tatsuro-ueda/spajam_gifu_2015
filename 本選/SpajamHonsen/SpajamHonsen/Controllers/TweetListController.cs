using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SpajamHonsen.Models;
using SpajamHonsen.Models.JsonResponse;
using SpajamHonsen.Utilities;

namespace SpajamHonsen.Controllers
{
    public class TweetListController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/GetTweetList/5
        //[ResponseType(typeof(void))]
        public async Task<List<HcvInformation>> GetTweetList(string id, string lan)
        {
            var results = new List<HcvInformation>();
            foreach (var model in db.HVCLog.Where(item => item.SpotID == id && item.TweetID != null)
                                    .OrderByDescending(item => item.CreateDateTime).Take(5))
            {
                var result = new HcvInformation()
                {
                    Age = model.Age,
                    Sex = model.Sex,
                    Expression = model.Expression,
                    SpotID = model.SpotID,
                    CreateDateTime = model.CreateDateTime,
                    Language = model.Language,
                    LogID = model.LogID,
                    TweetID = model.TweetID,
                    IconURL = TweetDetailController.GetAconUrl(lan, model.Sex, model.Age, model.Expression),
                    IconDisp = TweetDetailController.GetAconDescripUrl(lan, model.Sex, model.Age, model.Expression)
                };

                var tweet = db.Tweet.First(item => item.SpotID == model.SpotID && item.TweetID == model.TweetID);
                if (tweet != null)
                {
                    if (lan == "ja")
                    {
                        result.TweetText = tweet.TweetTextjp;
                    }
                    else if (lan == "cn")
                    {
                        result.TweetText = tweet.TweetTextcn;
                    }
                    else if (lan == "en")
                    {
                        result.TweetText = tweet.TweetTexten;
                    }
                    result.TweetURL = tweet.TweetURL;
                }
                results.Add(result);
            }
            return results;
        }
    }
}