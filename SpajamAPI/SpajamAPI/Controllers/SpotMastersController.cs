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
    public class SpotMastersController : ApiController
    {
        private SpajamMadobenDBEntities db = new SpajamMadobenDBEntities();

        // GET: api/SpotMasters
        public IQueryable<SpotMaster> GetSpotMaster()
        {
            return db.SpotMaster;
        }

        // GET: api/SpotMasters/21
        public IQueryable<SpotMaster> GetSpotMaster(string id)
        {
            return db.SpotMaster.Where(master => master.PrefectureID.ToString() == id);
        }

        /*
        // GET: api/SpotMasters/5
        [ResponseType(typeof(SpotMaster))]
        public async Task<IHttpActionResult> GetSpotMaster(string id)
        {
            SpotMaster spotMaster = await db.SpotMaster.FindAsync(id);
            if (spotMaster == null)
            {
                return NotFound();
            }

            return Ok(spotMaster);
        }
        */

        // PUT: api/SpotMasters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSpotMaster(string id, SpotMaster spotMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spotMaster.SpotKey)
            {
                return BadRequest();
            }

            db.Entry(spotMaster).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpotMasterExists(id))
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

        // POST: api/SpotMasters
        [ResponseType(typeof(SpotMaster))]
        public async Task<IHttpActionResult> PostSpotMaster(SpotMaster spotMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SpotMaster.Add(spotMaster);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpotMasterExists(spotMaster.SpotKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = spotMaster.SpotKey }, spotMaster);
        }

        // DELETE: api/SpotMasters/5
        [ResponseType(typeof(SpotMaster))]
        public async Task<IHttpActionResult> DeleteSpotMaster(string id)
        {
            SpotMaster spotMaster = await db.SpotMaster.FindAsync(id);
            if (spotMaster == null)
            {
                return NotFound();
            }

            db.SpotMaster.Remove(spotMaster);
            await db.SaveChangesAsync();

            return Ok(spotMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SpotMasterExists(string id)
        {
            return db.SpotMaster.Count(e => e.SpotKey == id) > 0;
        }
    }
}