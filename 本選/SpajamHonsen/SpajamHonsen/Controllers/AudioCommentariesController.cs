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
using System.Configuration;
using SpajamHonsen.Utilities;
using System.Web;
using System.IO;

namespace SpajamHonsen.Controllers
{
    public class AudioCommentariesController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

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

        [ResponseType(typeof(AudioCommentary))]
        public async Task<string> PostAudioCommentary(AudioCommentariesRequestModel request)
        {
            string result = string.Empty;

            // データ削除
            AudioCommentary delaudioCommentary = db.AudioCommentary.Where(master => master.SpotKey == request.SpotKey).FirstOrDefault();
            if (delaudioCommentary != null)
            {
                db.AudioCommentary.Remove(delaudioCommentary);
                await db.SaveChangesAsync();
            }

            // 音声解説ファイルのアップロード
            byte[] byteArray = System.Convert.FromBase64String(request.AudioBase64);
            var azureStorageUtil = new AzureStorageUtil();
            var fileName = Guid.NewGuid().ToString();
            await azureStorageUtil.UploadBlobStrage(byteArray, fileName, "audios");

            // 音声解説ファイルのレート変換
            var inputFilePath = HttpContext.Current.Server.MapPath("~/ffmpeg/" + Guid.NewGuid().ToString() + ".wav");

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

            // 音声ファイルのレートを16000に変換
            var convertFilePath = FFmpegUtil.ConvertAudioRate(inputFilePath, "16000");

            // 変換結果ファイルを読み込み
            using (System.IO.FileStream outputFileStream =
                new System.IO.FileStream(
                    convertFilePath, FileMode.Open))
            {
                byte[] audioByteArray = new byte[outputFileStream.Length];
                outputFileStream.Read(audioByteArray, 0, audioByteArray.Length);

                // 音声解説ファイルの解析
                var speechText = await GoogleUtil.RequestGoogleSpeechAPIAsync(audioByteArray);

                // 音声解説ファイルの解析結果の漢字変換
                var kanjiText = await GoogleUtil.RequestGoogleJapaneseAPI(speechText);

                // 音声解析結果の音声合成
                var othersUtil = new OthersUtil();
                var voiceTextFileName = await othersUtil.RequestVoiceTextAPI(speechText, "hikari");
                result = azureStorageUtil.GetBlobStrageUrl(voiceTextFileName, "voicetext");

                // DB登録
                var audioCommentary = new AudioCommentary()
                {
                    AudioCommentaryKey = Guid.NewGuid().ToString(),
                    AudioCommentaryTitle = request.AudioCommentaryTitle,
                    SpotKey = request.SpotKey,
                    SortID = 1,
                    FileID = fileName,
                    AudioCommentaryResultOriginal = speechText,
                    AudioCommentaryResultConversion = kanjiText,
                    SpeechSynthesisFileID = voiceTextFileName,
                    RegisteredUserID = request.RegisteredUserID,
                    RegisteredDateTime = DateTime.Now,
                };

                db.AudioCommentary.Add(audioCommentary);
            }

            await db.SaveChangesAsync();

            return result;
        }

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