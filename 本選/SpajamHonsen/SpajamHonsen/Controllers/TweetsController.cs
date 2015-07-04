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
    public class TweetsController : ApiController
    {
        private spajamhonsenEntities db = new spajamhonsenEntities();

        // GET: api/Tweets
        public IQueryable<Tweet> GetTweet()
        {
            return db.Tweet;
        }

        // GET: api/Tweets/5
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> GetTweet(string id)
        {
            Tweet tweet = await db.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            return Ok(tweet);
        }

        // PUT: api/Tweets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTweet(string id, Tweet tweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tweet.TweetID)
            {
                return BadRequest();
            }

            db.Entry(tweet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TweetExists(id))
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

        // POST: api/Tweets
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> PostTweet(Tweet tweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tweet.Add(tweet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TweetExists(tweet.TweetID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tweet.TweetID }, tweet);
        }

        // DELETE: api/Tweets/5
        [ResponseType(typeof(Tweet))]
        public async Task<IHttpActionResult> DeleteTweet(string id)
        {
            Tweet tweet = await db.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            db.Tweet.Remove(tweet);
            await db.SaveChangesAsync();

            return Ok(tweet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TweetExists(string id)
        {
            return db.Tweet.Count(e => e.TweetID == id) > 0;
        }
    }
}