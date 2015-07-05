using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SpajamHonsen.Models;
using SpajamHonsen.Models.JsonRequest;
using System.Web;
using SpajamHonsen.Utilities;
using System.IO;

namespace SpajamHonsen.Controllers
{
    public class TweetsController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/Tweets
        public IQueryable<Tweet> GetTweet()
        {
            return db.Tweet;
        }

        // GET: api/Tweets/5
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> GetTweet(string id)
        {
            Tweet tweet = await db.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            return Ok(tweet);
        }

        // PUT: api/Tweets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTweet(string id, Tweet tweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tweet.TweetID)
            {
                return BadRequest();
            }

            db.Entry(tweet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TweetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tweets
        /// <summary>
        /// 詳細画面から音声とHVCのログを登録する
        /// </summary>
        /// <param name="tweetPostRequest"></param>
        /// <returns></returns>
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> PostTweet(TweetPostRequest tweetPostRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tweet音声の解析
            byte[] byteArray = System.Convert.FromBase64String(tweetPostRequest.base64Audio);

            // ローカルに一時的に作成する音声のパス
            var inputFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" 
                + Guid.NewGuid().ToString() + ".wav");

            // 一時的に音声ファイルを作成
            using (System.IO.FileStream inputFileStream =
                new System.IO.FileStream(
                    inputFilePath,
                    System.IO.FileMode.Create,
                    System.IO.FileAccess.Write))
            {
                // バイト型配列の内容をすべて書き込む
                inputFileStream.Write(byteArray, 0, byteArray.Length);
            }

            var language = tweetPostRequest.hVCLogPostRequest.Language;
            var sex = tweetPostRequest.hVCLogPostRequest.Sex;

            #region 音声ファイルのbase64文字列ログ出力
            // TODO 完成後コメントアウトする
            var audioLogFilePath = HttpContext.Current.Server.MapPath("~/Temp/Logs/" + Guid.NewGuid().ToString() + ".log");
            
            // ファイルを作成する
            using (System.IO.FileStream hStream = System.IO.File.Create(audioLogFilePath))
            {
                // 作成時に返される FileStream を利用して閉じる
                if (hStream != null)
                {
                    hStream.Close();
                }
            }

            var base64String = System.Convert.ToBase64String(byteArray, 0, byteArray.Length);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    audioLogFilePath,
                    true,
                    System.Text.Encoding.GetEncoding("utf-8"));
            sw.Write(base64String);
            sw.Close();
            var azureStorageUtil = new AzureStorageUtil();
            azureStorageUtil.UploadBlobStrage(audioLogFilePath, Guid.NewGuid().ToString() + ".log", "logs");
            #endregion 音声ファイルのbase64文字列ログ出力

            string convertFilePath = string.Empty;
            // 音声ファイルのレートを変換
            if (language == "en" || language == "jp")
            {
                convertFilePath = FFmpegUtil.ConvertAudioRate(inputFilePath, "16000");
            }
            else if (language == "cn")
            {
                convertFilePath = FFmpegUtil.ConvertAudioRate2(inputFilePath, "8000");
            }

            var tweet = new Tweet();

            // 変換結果ファイルを読み込み
            using (System.IO.FileStream outputFileStream =
                new System.IO.FileStream(
                    convertFilePath, FileMode.Open))
            {
                byte[] audioByteArray = new byte[outputFileStream.Length];
                outputFileStream.Read(audioByteArray, 0, audioByteArray.Length);

                // 音声解説ファイルの解析
                string speechTextjp = string.Empty;
                string speechTexten = string.Empty;
                string speechTextcn = string.Empty;
                if (language == "jp")
                {
                    speechTextjp = await GoogleUtil.RequestGoogleSpeechAPIAsync(audioByteArray);
                    speechTextcn = await BingUtil.RequestMicrosoftTranslatorAPIAsync(speechTextjp, "ja", "cn");
                    speechTexten = await BingUtil.RequestMicrosoftTranslatorAPIAsync(speechTextjp, "ja", "en");
                }
                else if (language == "en")
                {
                    speechTexten = await GoogleUtil.RequestGoogleSpeechAPIAsync(audioByteArray);
                    speechTextjp = await BingUtil.RequestMicrosoftTranslatorAPIAsync(speechTexten, "en", "ja");
                    speechTextcn = await BingUtil.RequestMicrosoftTranslatorAPIAsync(speechTextjp, "en", "cn");
                }
                else if (language == "cn")
                {
                    speechTextcn = await BaiduUtil.RequestBaiduSpeechAPIAsync(audioByteArray); 
                    speechTextjp = await BingUtil.RequestMicrosoftTranslatorAPIAsync(speechTextcn, "zh-cn", "ja");
                    speechTexten = await BingUtil.RequestMicrosoftTranslatorAPIAsync(speechTextcn, "zh-cn", "en");
                }

                // 音声解説ファイルの解析結果の漢字変換
                if (language == "jp") 
                {
                    speechTextjp = await GoogleUtil.RequestGoogleJapaneseAPI(speechTextjp); 
                }

                // 音声解析結果の音声合成
                var othersUtil = new OthersUtil();

                string tweetURL = "default";
                string voiceTextFileName = string.Empty;

                if (language == "jp")
                {
                    if(sex == "m")
                    {
                        voiceTextFileName = await othersUtil.RequestVoiceTextAPI(speechTextjp, "show");
                    }
                    else if (sex == "m")
                    {
                        voiceTextFileName = await othersUtil.RequestVoiceTextAPI(speechTextjp, "haruka");
                    }
                }
                else if (language == "en")
                {
                    //TODO 音声合成英語
                }
                else if (language == "cn")
                {
                    // 音声合成中国語
                    // voiceTextFileName = await BaiduUtil.RequestVoiceTextAPI(speechText);
                }

                // var azureStorageUtil = new AzureStorageUtil();
                // tweetURL = azureStorageUtil.GetBlobStrageUrl(voiceTextFileName, "voices");

                // 登録情報の設定
                tweet.TweetID = Guid.NewGuid().ToString();
                tweet.SpotID = tweetPostRequest.hVCLogPostRequest.SpotID;
                tweet.TweetTextjp = speechTextjp;
                tweet.TweetTextcn = speechTextcn;
                tweet.TweetTexten = speechTexten;
                tweet.TweetURL = tweetURL;
                tweet.CreateDateTime = DateTime.Now;

                db.Tweet.Add(tweet);

                var hVCLog = new HVCLog()
                {
                    LogID = Guid.NewGuid().ToString(),
                    SpotID = tweetPostRequest.hVCLogPostRequest.SpotID,
                    TweetID = tweet.TweetID,
                    Language = tweetPostRequest.hVCLogPostRequest.Language,
                    Expression = tweetPostRequest.hVCLogPostRequest.Expression,
                    Age = tweetPostRequest.hVCLogPostRequest.Age,
                    Sex = tweetPostRequest.hVCLogPostRequest.Sex,
                    CreateDateTime = DateTime.Now,
                };

                db.HVCLog.Add(hVCLog);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TweetExists(tweet.TweetID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tweet.TweetID }, tweet);
        }

        /*
        // POST: api/Tweets
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> PostTweet(Tweet tweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tweet.Add(tweet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TweetExists(tweet.TweetID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tweet.TweetID }, tweet);
        }
        */

        // DELETE: api/Tweets/5
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> DeleteTweet(string id)
        {
            Tweet tweet = await db.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            db.Tweet.Remove(tweet);
            await db.SaveChangesAsync();

            return Ok(tweet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TweetExists(string id)
        {
            return db.Tweet.Count(e => e.TweetID == id) > 0;
        }
    }
}