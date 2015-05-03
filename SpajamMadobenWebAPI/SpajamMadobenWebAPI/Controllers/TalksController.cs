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
    /// TalkテーブルのCRUD処理を行う
    /// </summary>
    public class TalksController : ApiController
    {
        private SpajamMadobenDBEntities2 db = new SpajamMadobenDBEntities2();

        // GET: api/Talks
        /// <summary>
        /// Talkテーブルのデータ一覧を取得する
        /// </summary>
        /// <returns></returns>
        public IQueryable<Talk> GetTalk()
        {
            return db.Talk;
        }

        // GET: api/Talks/5
        /// <summary>
        /// Talkテーブルのデータを1件取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Talk))]
        public async Task<IHttpActionResult> GetTalk(string id)
        {
            Talk talk = await db.Talk.FindAsync(id);
            if (talk == null)
            {
                return NotFound();
            }

            return Ok(talk);
        }

        // PUT: api/Talks/5
        /// <summary>
        /// Talkテーブルのデータを1件更新する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="talk"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTalk(string id, Talk talk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != talk.UserID)
            {
                return BadRequest();
            }

            db.Entry(talk).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalkExists(id))
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

        // POST: api/Talks
        /// <summary>
        /// Talkテーブルのデータを1件追加する
        /// </summary>
        /// <param name="talk"></param>
        /// <returns></returns>
        [ResponseType(typeof(Talk))]
        public async Task<IHttpActionResult> PostTalk(Talk talk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Talk.Add(talk);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TalkExists(talk.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = talk.UserID }, talk);
        }

        // DELETE: api/Talks/5
        /// <summary>
        /// Talkテーブルのデータを1件削除する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Talk))]
        public async Task<IHttpActionResult> DeleteTalk(string id)
        {
            Talk talk = await db.Talk.FindAsync(id);
            if (talk == null)
            {
                return NotFound();
            }

            db.Talk.Remove(talk);
            await db.SaveChangesAsync();

            return Ok(talk);
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
        private bool TalkExists(string id)
        {
            return db.Talk.Count(e => e.UserID == id) > 0;
        }
    }
}