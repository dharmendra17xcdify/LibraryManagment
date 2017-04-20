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
using LMS.Models.API;

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

        [HttpGet]
        public HttpResponseMessage LoadAssignedBookById(int id)
        {
            using (LibraryDBEntities entities = new LibraryDBEntities())
            {
                var entity = entities.AssignedBooks.ToList().Where(e => e.UserID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Books with userid  = " + id.ToString() + " not found");
                }
            }
        }
       // method to assigne books
        public HttpResponseMessage Post([FromBody] BooksAssigned assignedBooks)
        {
            try
            {
                using (LibraryDBEntities entities = new LibraryDBEntities())
                {
                    foreach (var book in assignedBooks.Books)
                    {
                        var entity = entities.AssignedBooks.ToList().Where(e => e.UserID == assignedBooks.UserId && e.BookID == book );

                        //var bookQty = entities.AssignedBooks.ToList().Where(e => e.UserID == assignedBooks.UserId && e.BookID.Value == book);
                        //if(bookQty.Count() < 3)                      

                        if(entity.Count() == 0)
                        {
                            entities.AssignedBooks.Add(new AssignedBook { UserID = assignedBooks.UserId, BookID = book });
                        }
                        
                    }
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, "true");
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LibraryDBEntities entities = new LibraryDBEntities())
                {
                    var entity = entities.AssignedBooks.FirstOrDefault(e => e.BookID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with id " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.AssignedBooks.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}