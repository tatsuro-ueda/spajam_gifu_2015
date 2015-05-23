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

namespace SpajamAPI.Controllers
{
    public class AudioCommentariesController : ApiController
    {
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

            // 音声解説ファイルの解析
            var apiKey = appSettings["GoogleSpeechAPIKey"];
            var responseFromServer = await RequestGoogleSpeechAPI(apiKey, byteArray);
            var responceArray = responseFromServer.Split('\n');
            var audioCommentaryResultOriginal = responceArray[1];

            // 音声解説ファイルの解析結果の変換

            // 音声解説ファイル変換結果の音声合成

            var audioCommentary = new AudioCommentary() 
            { 
                AudioCommentaryKey = Guid.NewGuid().ToString(),
                AudioCommentaryTitle = request.AudioCommentaryTitle,
                SpotKey = request.SpotKey,
                SortID = 1,
                FileID = fileID,
                AudioCommentaryResultOriginal = audioCommentaryResultOriginal
                AudioCommentaryResultConversion = string.Empty, //TODO 解析結果(変換)
                SpeechSynthesisFileID = Guid.NewGuid().ToString(),//TODO 音声合成したファイルID
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
            var parameter = new NameValueHeaderValue("rate", "16000");
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