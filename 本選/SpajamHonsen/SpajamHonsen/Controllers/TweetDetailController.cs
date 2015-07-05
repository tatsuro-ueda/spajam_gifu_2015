using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using SpajamHonsen.Models;
using SpajamHonsen.Models.JsonResponse;
using SpajamHonsen.Utilities;

namespace SpajamHonsen.Controllers
{
    public class TweetDetailController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/GetTweetDetail/5
        [ResponseType(typeof(void))]
        public async Task<List<HcvInformation>> GetTweetDetail(string id, string lan)
        {
            var results = new List<HcvInformation>();
            foreach (var model in db.HVCLog.Where(item => item.SpotID == id && item.TweetID != null).OrderByDescending(item => item.CreateDateTime))
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
                    if (lan == "jp")
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
                }
                results.Add(result);
            }
            return results;
        }

        public static string GetAconUrl(string lan, string sex, int age, int expression)
        {
            var result = "";
            // var result = HttpContext.Current.Server.MapPath("~/Scripts/");
            if (lan == "cn")
            {
                result = "cn_";
            } 
            else if (lan == "en")
            {
                result = "en_";
            }
            else
            {
                result = "jp_";
            }

            if (sex == "f")
            {
                result += "f_";
            }
            else
            {
                result += "m_";
            }

            if (age > 0 && age <= 15)
            {
                result += "00-15_";
            } 
            else if (age > 16 && age <= 19) 
            {
                result += "16-19_";
            }
            else if (age > 20 && age <= 29)
            {
                result += "20-29_";
            }
            else if (age > 30 && age <= 45)
            {
                result += "30-45_";
            }
            else 
            {
                result += "46-60_";
            }
          
            result += (expression).ToString() + ".png";
            
            return result;
        }

        public static string GetAconDescripUrl(string lan, string sex, int age, int expression)
        {
            var result = "[";
            if (lan == "cn")
            {
                result += "中国　";

                if (sex == "f")
                {
                    result += "女性　";
                }
                else
                {
                    result += "男性　";
                }
            }
            else if (lan == "en")
            {
                result += "English　";

                if (sex == "f")
                {
                    result += "female　";
                }
                else
                {
                    result += "male　";
                }
            }
            else
            {
                result += "日本　";

                if (sex == "f")
                {
                    result += "女性　";
                }
                else
                {
                    result += "男性　";
                }
            }

            

            if (age > 0 && age <= 15)
            {
                result += "00-15";
            }
            else if (age > 16 && age <= 19)
            {
                result += "16-19";
            }
            else if (age > 20 && age <= 29)
            {
                result += "20-29";
            }
            else if (age > 30 && age <= 45)
            {
                result += "30-45";
            }
            else
            {
                result += "46-60";
            }

            result += "]";
            return result;
        }
    }
}