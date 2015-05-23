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