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

namespace SpajamHonsen.Controllers
{
    public class SpotMastersController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/SpotMasters
        public IQueryable<SpotMaster> GetSpotMaster()
        {
            return db.SpotMaster;
        }

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

        // PUT: api/SpotMasters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSpotMaster(string id, SpotMaster spotMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spotMaster.SpotID)
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
                if (SpotMasterExists(spotMaster.SpotID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = spotMaster.SpotID }, spotMaster);
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
            return db.SpotMaster.Count(e => e.SpotID == id) > 0;
        }
    }
}