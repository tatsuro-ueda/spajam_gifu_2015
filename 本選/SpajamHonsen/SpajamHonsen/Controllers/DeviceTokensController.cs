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
    public class DeviceTokensController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/DeviceTokens
        public IQueryable<DeviceToken> GetDeviceToken()
        {
            return db.DeviceToken;
        }

        // GET: api/DeviceTokens/5
        [ResponseType(typeof(DeviceToken))]
        public async Task<IHttpActionResult> GetDeviceToken(string id)
        {
            DeviceToken deviceToken = await db.DeviceToken.FindAsync(id);
            if (deviceToken == null)
            {
                return NotFound();
            }

            return Ok(deviceToken);
        }

        // PUT: api/DeviceTokens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeviceToken(string id, DeviceToken deviceToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceToken.DeviceTokenID)
            {
                return BadRequest();
            }

            db.Entry(deviceToken).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTokenExists(id))
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

        // POST: api/DeviceTokens
        [ResponseType(typeof(DeviceToken))]
        public async Task<IHttpActionResult> PostDeviceToken(DeviceToken deviceToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceToken.Add(deviceToken);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeviceTokenExists(deviceToken.DeviceTokenID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deviceToken.DeviceTokenID }, deviceToken);
        }

        // DELETE: api/DeviceTokens/5
        [ResponseType(typeof(DeviceToken))]
        public async Task<IHttpActionResult> DeleteDeviceToken(string id)
        {
            DeviceToken deviceToken = await db.DeviceToken.FindAsync(id);
            if (deviceToken == null)
            {
                return NotFound();
            }

            db.DeviceToken.Remove(deviceToken);
            await db.SaveChangesAsync();

            return Ok(deviceToken);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceTokenExists(string id)
        {
            return db.DeviceToken.Count(e => e.DeviceTokenID == id) > 0;
        }
    }
}