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
using SpajamMadobenWebAPI.Models;

namespace SpajamMadobenWebAPI.Controllers
{
    /// <summary>
    /// VoiceテーブルのCRUD処理を行う
    /// </summary>
    public class VoicesController : ApiController
    {
        private SpajamMadobenDBEntities2 db = new SpajamMadobenDBEntities2();

        // GET: api/Voices
        /// <summary>
        /// Voiceテーブルのデータ一覧を取得する
        /// </summary>
        /// <returns></returns>
        public IQueryable<Voice> GetVoice()
        {
            return db.Voice;
        }

        // GET: api/Voices/5
        /// <summary>
        /// Voiceテーブルのデータを1件取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Voice))]
        public async Task<IHttpActionResult> GetVoice(string id)
        {
            Voice voice = await db.Voice.FindAsync(id);
            if (voice == null)
            {
                return NotFound();
            }

            return Ok(voice);
        }

        // PUT: api/Voices/5
        /// <summary>
        /// Voiceテーブルのデータを1件更新する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="voice"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVoice(string id, Voice voice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voice.TalkID)
            {
                return BadRequest();
            }

            db.Entry(voice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoiceExists(id))
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

        // POST: api/Voices
        /// <summary>
        /// Voiceテーブルのデータを1件追加する
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        [ResponseType(typeof(Voice))]
        public async Task<IHttpActionResult> PostVoice(Voice voice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voice.Add(voice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VoiceExists(voice.TalkID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = voice.TalkID }, voice);
        }

        // DELETE: api/Voices/5
        /// <summary>
        /// Voiceテーブルのデータを1件削除する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Voice))]
        public async Task<IHttpActionResult> DeleteVoice(string id)
        {
            Voice voice = await db.Voice.FindAsync(id);
            if (voice == null)
            {
                return NotFound();
            }

            db.Voice.Remove(voice);
            await db.SaveChangesAsync();

            return Ok(voice);
        }

        /// <summary>
        /// DBインスタンスの破棄
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 重複の確認
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool VoiceExists(string id)
        {
            return db.Voice.Count(e => e.TalkID == id) > 0;
        }
    }
}