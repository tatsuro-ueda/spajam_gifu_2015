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
using SpajamHonsen.Models.JsonResponse;

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
        //[ResponseType(typeof(HVCLog))]
        //public async Task<IHttpActionResult> GetHVCLog(string id)
        //{
        //    HVCLog hVCLog = await db.HVCLog.FindAsync(id);
        //    if (hVCLog == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(hVCLog);
        //}

        // GET: api/GetHVCLog/5
        //[ResponseType(typeof(void))]
        //public ExpressInformation GetHVCLog(string id)
        //{
        //    var express1 = 0;
        //    if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 1))
        //    {
        //        express1 = db.HVCLog.Where(item => item.SpotID == id && item.Expression == 1).Sum(item => item.Expression);
        //    }
        //    var express2 = 0;
        //    if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 2))
        //    {
        //        express2 = db.HVCLog.Where(item => item.SpotID == id && item.Expression == 2).Sum(item => item.Expression);
        //    }
        //    var express3 = 0;
        //    if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 3))
        //    {
        //        express3 = db.HVCLog.Where(item => item.SpotID == id && item.Expression == 3).Sum(item => item.Expression);
        //    }
        //    var express4 = 0;
        //    if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 4))
        //    {
        //        express4 = db.HVCLog.Where(item => item.SpotID == id && item.Expression == 4).Sum(item => item.Expression);
        //    }
        //    var express5 = 0;
        //    if (db.HVCLog.Any(item => item.SpotID == id && item.Expression == 5))
        //    {
        //        express5 = db.HVCLog.Where(item => item.SpotID == id && item.Expression == 5).Sum(item => item.Expression);
        //    }
        //    var expressInfo = new ExpressInformation()
        //    {
        //        Express1 = express1,
        //        Express2 = express2,
        //        Express3 = express3,
        //        Express4 = express4,
        //        Express5 = express5
        //    };

        //    return expressInfo;
        //}

        // GET: api/HVCLogs/5
        [ResponseType(typeof(HVCLog))]
        public List<HcvInformation> GetHVCLog(string id, string lan)
        {
            var result = new List<HcvInformation>();
            foreach (var model in db.HVCLog.Where(item => item.SpotID == id))
            {
                result.Add(new HcvInformation()
                {
                    Age = model.Age,
                    Sex = model.Sex,
                    Expression = model.Expression,
                    SpotID = model.SpotID,
                    CreateDateTime = model.CreateDateTime,
                    Language = model.Language,
                    LogID = model.LogID
                });

                // Sdb.Tweet.Where(item => item.SpotID == model.SpotID && item.)
            }
            return result;
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
        public async Task<IHttpActionResult> PostHVCLog(HVCLog hVCLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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