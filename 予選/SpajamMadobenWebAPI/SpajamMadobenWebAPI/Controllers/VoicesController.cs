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
    public class VoicesController : ApiController
    {
        private SpajamMadobenDBEntities db = new SpajamMadobenDBEntities();

        // GET: api/Voices
        public IQueryable<Voice> GetVoice()
        {
            return db.Voice;
        }

        // GET: api/Voices/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoiceExists(string id)
        {
            return db.Voice.Count(e => e.TalkID == id) > 0;
        }
    }
}