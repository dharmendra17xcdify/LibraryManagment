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
using LMS;

namespace LMS.Controllers
{
    public class AssignedBooksController : ApiController
    {
       
        public IEnumerable<AssignedBook> Get()
        {


            using (LibraryDBEntities entities = new LibraryDBEntities())
            {
                return entities.AssignedBooks.ToList();
            }
        }

        public HttpResponseMessage Post([FromBody] AssignedBook book)
        {
            try
            {
                using (LibraryDBEntities entities = new LibraryDBEntities())
                {

                    entities.AssignedBooks.Add(book);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, book);
                    message.Headers.Location = new Uri(Request.RequestUri + book.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //private LibraryDBEntities db = new LibraryDBEntities();

        //// GET: api/AssignedBooks
        //public IQueryable<AssignedBook> GetAssignedBooks()
        //{
        //    return db.AssignedBooks;
        //}

        // GET: api/AssignedBooks/5
        //[ResponseType(typeof(AssignedBook))]
        //public IHttpActionResult GetAssignedBook(int id)
        //{
        //    AssignedBook assignedBook = db.AssignedBooks.Find(id);
        //    if (assignedBook == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(assignedBook);
        //}

        //// PUT: api/AssignedBooks/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAssignedBook(int id, AssignedBook assignedBook)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != assignedBook.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(assignedBook).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AssignedBookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/AssignedBooks
        //[ResponseType(typeof(AssignedBook))]
        //public IHttpActionResult PostAssignedBook(AssignedBook assignedBook)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AssignedBooks.Add(assignedBook);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = assignedBook.ID }, assignedBook);
        //}

        //// DELETE: api/AssignedBooks/5
        //[ResponseType(typeof(AssignedBook))]
        //public IHttpActionResult DeleteAssignedBook(int id)
        //{
        //    AssignedBook assignedBook = db.AssignedBooks.Find(id);
        //    if (assignedBook == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AssignedBooks.Remove(assignedBook);
        //    db.SaveChanges();

        //    return Ok(assignedBook);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool AssignedBookExists(int id)
        //{
        //    return db.AssignedBooks.Count(e => e.ID == id) > 0;
        //}
    }
}