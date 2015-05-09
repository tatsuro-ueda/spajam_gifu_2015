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
    public class TalksController : ApiController
    {
        private SpajamMadobenDBEntities db = new SpajamMadobenDBEntities();

        // GET: api/Talks
        public IQueryable<Talk> GetTalk()
        {
            return db.Talk;
        }

        // GET: api/Talks/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TalkExists(string id)
        {
            return db.Talk.Count(e => e.UserID == id) > 0;
        }
    }
}