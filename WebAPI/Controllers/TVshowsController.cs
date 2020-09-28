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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TVshowsController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/TVshows
        public IQueryable<TVshow> GetTVshow()
        {
            return db.TVshow;
        }

      /*  // GET: api/TVshows/5
        [ResponseType(typeof(TVshow))]
        public IHttpActionResult GetTVshow(int id)
        {
            TVshow tVshow = db.TVshow.Find(id);
            if (tVshow == null)
            {
                return NotFound();
            }

            return Ok(tVshow);
        }*/

        // PUT: api/TVshows/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTVshow(int id, TVshow tVshow)
        {
           /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            if (id != tVshow.ShowID)
            {
                return BadRequest();
            }

            db.Entry(tVshow).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TVshowExists(id))
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

        // POST: api/TVshows
        [ResponseType(typeof(TVshow))]
        public IHttpActionResult PostTVshow(TVshow tVshow)
        {
          /*  if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            db.TVshow.Add(tVshow);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tVshow.ShowID }, tVshow);
        }

        // DELETE: api/TVshows/5
        [ResponseType(typeof(TVshow))]
        public IHttpActionResult DeleteTVshow(int id)
        {
            TVshow tVshow = db.TVshow.Find(id);
            if (tVshow == null)
            {
                return NotFound();
            }

            db.TVshow.Remove(tVshow);
            db.SaveChanges();

            return Ok(tVshow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TVshowExists(int id)
        {
            return db.TVshow.Count(e => e.ShowID == id) > 0;
        }
    }
}