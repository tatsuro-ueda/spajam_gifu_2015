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

namespace SpajamHonsen.Controllers
{
    public class HVCLogsController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/HVCLogs
        public IQueryable<HVCLog> GetHVCLog()
        {
            return db.HVCLog;
        }

        // GET: api/HVCLogs/5
        [ResponseType(typeof(HVCLog))]
        public async Task<IHttpActionResult> GetHVCLog(string id)
        {
            HVCLog hVCLog = await db.HVCLog.FindAsync(id);
            if (hVCLog == null)
            {
                return NotFound();
            }

            return Ok(hVCLog);
        }

        // PUT: api/HVCLogs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHVCLog(string id, HVCLog hVCLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hVCLog.LogID)
            {
                return BadRequest();
            }

            db.Entry(hVCLog).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HVCLogExists(id))
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

        // POST: api/HVCLogs
        [ResponseType(typeof(HVCLog))]
        public async Task<IHttpActionResult> PostHVCLog(HVCLogPostRequest hVCLogPostRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hVCLog = new HVCLog()
            {
                LogID = Guid.NewGuid().ToString(),
                SpotID = hVCLogPostRequest.SpotID,
                Language = hVCLogPostRequest.Language,
                Expression = hVCLogPostRequest.Expression,
                Age = hVCLogPostRequest.Age,
                Sex = hVCLogPostRequest.Sex,
                CreateDateTime = DateTime.Now,
            };



            db.HVCLog.Add(hVCLog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HVCLogExists(hVCLog.LogID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = hVCLog.LogID }, hVCLog);
        }

        // DELETE: api/HVCLogs/5
        [ResponseType(typeof(HVCLog))]
        public async Task<IHttpActionResult> DeleteHVCLog(string id)
        {
            HVCLog hVCLog = await db.HVCLog.FindAsync(id);
            if (hVCLog == null)
            {
                return NotFound();
            }

            db.HVCLog.Remove(hVCLog);
            await db.SaveChangesAsync();

            return Ok(hVCLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HVCLogExists(string id)
        {
            return db.HVCLog.Count(e => e.LogID == id) > 0;
        }
    }
}