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
using SpajamAPI.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace SpajamAPI.Controllers
{
    public class AudioCommentariesController : ApiController
    {
        // private const string SPEECH_TEXT = "関ヶ原の戦いは知っとるじゃろ？お昼を過ぎてから、東軍の家康が総がかりで西軍を押し始め、西軍の総大将石田三成は逃走したんじゃ。ところが途中で捕まってしまって、三条河原でさらし首になったのじゃが、その捕まった場所がこの坂なんじゃ。それ以来、毎年9月15日になるとむせび泣きが聞こえるようになってしまってのお。";
        private const string SPEECH_TEXT = "あーあーまいくのてすとちゅうー";

        private SpajamMadobenDBEntities db = new SpajamMadobenDBEntities();

        // GET: api/AudioCommentaries
        public IQueryable<AudioCommentary> GetAudioCommentary()
        {
            return db.AudioCommentary;
        }

        // GET: api/AudioCommentaries/5
        [ResponseType(typeof(AudioCommentary))]
        public async Task<IHttpActionResult> GetAudioCommentary(string id)
        {
            AudioCommentary audioCommentary = await db.AudioCommentary.FindAsync(id);
            if (audioCommentary == null)
            {
                return NotFound();
            }

            return Ok(audioCommentary);
        }

        // PUT: api/AudioCommentaries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAudioCommentary(string id, AudioCommentary audioCommentary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != audioCommentary.AudioCommentaryKey)
            {
                return BadRequest();
            }

            db.Entry(audioCommentary).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioCommentaryExists(id))
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

        [ResponseType(typeof(AudioCommentary))]
        public async Task<IHttpActionResult> PostAudioCommentary(AudioCommentariesRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appSettings = ConfigurationManager.AppSettings;


            // 音声解説ファイルのbase64変換+アップロード
            var accountKey = appSettings["CloudStorageAccount"];
            byte[] byteArray = System.Convert.FromBase64String(request.AudioBase64);
            var fileID = Guid.NewGuid().ToString();
            await UploadBlobStrage(accountKey, byteArray, fileID);

            // 音声解説ファイルのダウンロード
            // var response = await DownloadBlobStrage(accountKey, fileID);

            // 音声解説ファイルの解析
            var apiKey = appSettings["GoogleSpeechAPIKey"];
            var responseFromServer = await RequestGoogleSpeechAPI(apiKey, byteArray);
            var responceArray = responseFromServer.Split('\n');
            var googleSpeechResponce = responceArray[1];
            var responseJson = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SpajamAPI.Models.GoogleSpeechAPIResponseModels.GoogleSpeechAPIResponseModel>(googleSpeechResponce));

            string audioCommentaryResultOriginal = SPEECH_TEXT;

            if (responseJson != null)
            {
                audioCommentaryResultOriginal = responseJson.result[0].alternative[0].transcript; 
            }

            // 音声解説ファイルの解析結果の漢字変換
            // var audioCommentaryResultConversion = await RequestGoogleJapaneseAPI(audioCommentaryResultOriginal);

            // 音声解説ファイル変換結果の音声合成 TODO 本当は変換結果を送る
            var speechSynthesisFileID = await RequestVoiceTextAPI(audioCommentaryResultOriginal, appSettings);

            var audioCommentary = new AudioCommentary() 
            { 
                AudioCommentaryKey = Guid.NewGuid().ToString(),
                AudioCommentaryTitle = request.AudioCommentaryTitle,
                SpotKey = request.SpotKey,
                SortID = 1,
                FileID = fileID,
                AudioCommentaryResultOriginal = audioCommentaryResultOriginal,
                AudioCommentaryResultConversion = audioCommentaryResultOriginal, //TODO
                SpeechSynthesisFileID = speechSynthesisFileID,
                RegisteredUserID = request.RegisteredUserID,
                RegisteredDateTime = DateTime.Now,
            };

            db.AudioCommentary.Add(audioCommentary);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AudioCommentaryExists(audioCommentary.AudioCommentaryKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = audioCommentary.AudioCommentaryKey }, audioCommentary);
        }
        
        /*
        // POST: api/AudioCommentaries
        [ResponseType(typeof(AudioCommentary))]
        public async Task<IHttpActionResult> PostAudioCommentary(AudioCommentary audioCommentary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AudioCommentary.Add(audioCommentary);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AudioCommentaryExists(audioCommentary.AudioCommentaryKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = audioCommentary.AudioCommentaryKey }, audioCommentary);
        }
        */

        // DELETE: api/AudioCommentaries/5
        [ResponseType(typeof(AudioCommentary))]
        public async Task<IHttpActionResult> DeleteAudioCommentary(string id)
        {
            AudioCommentary audioCommentary = await db.AudioCommentary.FindAsync(id);
            if (audioCommentary == null)
            {
                return NotFound();
            }

            db.AudioCommentary.Remove(audioCommentary);
            await db.SaveChangesAsync();

            return Ok(audioCommentary);
        }

        /// <summary>
        /// AzureBlobStrageにファイルをアップロードする
        /// </summary>
        /// <param name="accountKey">AzureStorageのアカウントキー</param>
        /// <param name="stream">ストリーム</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        private static async Task UploadBlobStrage(string accountKey, Stream stream, string fileName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference("audios");

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + ".wav");

            await blockBlob.UploadFromStreamAsync(stream);
        }

        /// <summary>
        /// AzureBlobStrageにファイルをアップロードする
        /// </summary>
        /// <param name="accountKey">AzureStorageのアカウントキー</param>
        /// <param name="byteArray">バイナリデータのbyte配列</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        private static async Task UploadBlobStrage(string accountKey, byte[] byteArray, string fileName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference("audios");

            container.CreateIfNotExists();

            // Blobを作成
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName + ".flac");

            // byte配列をMemoryStreamに変換
            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                // Blobにアップロードする
                await blockBlob.UploadFromStreamAsync(ms);
            }
        }

        /// <summary>
        /// AzureBlobStrageからファイルをダウンロードする
        /// </summary>
        /// <param name="accountKey">AzureStorageのアカウントキー</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        private static async Task<string> DownloadBlobStrage(string accountKey, string fileName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accountKey);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // コンテナを作成
            CloudBlobContainer container = blobClient.GetContainerReference("audios");

            var blockBlob = container.GetBlockBlobReference(fileName + ".flac");

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Position = 0;
                await blockBlob.DownloadToStreamAsync(ms);
                var byteArray = ms.ToArray();
                return System.Convert.ToBase64String(byteArray); ;
            }
        }

        /// <summary>
        /// GoogleSpeechAPIにリクエスト送信
        /// </summary>
        /// <param name="key">APIキー</param>
        /// <param name="byteArray">音声ファイルのByte配列</param>
        /// <returns></returns>
        private static async Task<string> RequestGoogleSpeechAPI(string key, byte[] byteArray)
        {
            var httpClient = new HttpClient();

            //content-type指定
             var mediaType = new MediaTypeWithQualityHeaderValue("audio/x-flac");
            // var parameter = new NameValueHeaderValue("rate", "16000");
            //var mediaType = new MediaTypeWithQualityHeaderValue("audio/x-wav");
            var parameter = new NameValueHeaderValue("rate", "44100");
            mediaType.Parameters.Add(parameter);
            // httpClient.DefaultRequestHeaders.Accept.Add(mediaType);

            var url = "https://www.google.com/speech-api/v2/recognize?output=json&lang=ja-jp&key=";
            var uri = new Uri(url + key);

            using (MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length))
            {
                var param = new StreamContent(ms);
                param.Headers.ContentType = mediaType;

                var result = await httpClient.PostAsync(uri, param);

                return await result.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// VoiceTextAPIにリクエスト送信
        /// </summary>
        /// <param name="voiceText"></param>
        /// <param name="appSettings"></param>
        /// <returns>ファイルIDのstring</returns>
        public async Task<string> RequestVoiceTextAPI(string voiceText, System.Collections.Specialized.NameValueCollection appSettings)
        {
            var username = appSettings["VoiceTextAPIUser"];
            var url = "https://api.voicetext.jp/v1/tts";

            var authHeader = new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, ""))));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = authHeader;

            var uri = new Uri(url);

            // Request設定
            var param = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "text", voiceText },
                    { "speaker", "haruka" },
                });

            var result = await client.PostAsync(uri, param);
            var stream = await result.Content.ReadAsStreamAsync();

            var accountKey = appSettings["CloudStorageAccount"];
            var fileName = Guid.NewGuid().ToString();

            await UploadBlobStrage(accountKey, stream, fileName);
            return fileName;
        }

        /// <summary>
        /// Google日本語入力APIにリクエスト送信
        /// </summary>
        /// <param name="kanaText"></param>
        /// <returns>ファイルIDのstring</returns>
        public async Task<string> RequestGoogleJapaneseAPI(string kanaText)
        {
            var url = "http://www.google.com/transliterate";

            var client = new HttpClient();

            var uri = new Uri(url);

            // Request設定
            var param = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "langpair", "ja-Hira|ja" },
                    { "text", kanaText },
                });

            var result = await client.PostAsync(uri, param);
            var responseStream = await result.Content.ReadAsStreamAsync();
            using (StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
            {
                var resultString = sr.ReadToEnd();
                return resultString;
            } 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AudioCommentaryExists(string id)
        {
            return db.AudioCommentary.Count(e => e.AudioCommentaryKey == id) > 0;
        }
    }
}