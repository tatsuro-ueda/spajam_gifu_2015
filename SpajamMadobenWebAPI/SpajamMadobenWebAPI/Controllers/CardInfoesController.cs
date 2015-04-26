using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SpajamMadobenWebAPI.Models;

namespace SpajamMadobenWebAPI.Controllers
{
    public class CardInfoesController : ApiController
    {
        private SpajamMadobenDBEntities db = new SpajamMadobenDBEntities();

        // GET: api/CardInfoes
        public IQueryable<CardInfo> GetCardInfo()
        {
            return db.CardInfo;
        }

        // GET: api/CardInfoes/5
        [ResponseType(typeof(CardInfo))]
        public IHttpActionResult GetCardInfo(string id)
        {
            CardInfo cardInfo = db.CardInfo.Find(id);
            if (cardInfo == null)
            {
                return NotFound();
            }

            return Ok(cardInfo);
        }

        // PUT: api/CardInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCardInfo(string id, CardInfo cardInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardInfo.CardID)
            {
                return BadRequest();
            }

            db.Entry(cardInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardInfoExists(id))
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

        // POST: api/CardInfoes
        [ResponseType(typeof(CardInfo))]
        public IHttpActionResult PostCardInfo(CardInfo cardInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CardInfo.Add(cardInfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CardInfoExists(cardInfo.CardID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cardInfo.CardID }, cardInfo);
        }

        // DELETE: api/CardInfoes/5
        [ResponseType(typeof(CardInfo))]
        public IHttpActionResult DeleteCardInfo(string id)
        {
            CardInfo cardInfo = db.CardInfo.Find(id);
            if (cardInfo == null)
            {
                return NotFound();
            }

            db.CardInfo.Remove(cardInfo);
            db.SaveChanges();

            return Ok(cardInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardInfoExists(string id)
        {
            return db.CardInfo.Count(e => e.CardID == id) > 0;
        }
    }
}