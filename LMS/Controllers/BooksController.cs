using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace LMS.Controllers
{
   
    public class BooksController : ApiController
    {
       
        public IEnumerable<Book> Get()
        {
           

            using(LibraryDBEntities entities = new LibraryDBEntities())
            {
               return entities.Books.ToList();
            }
        }


        [HttpGet]
        public HttpResponseMessage LoadBookById(int id)
        {
            using (LibraryDBEntities entities = new LibraryDBEntities())
            {
                var entity = entities.Books.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with id = " + id.ToString() + "not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Book book)
        {
            try
            {
                using (LibraryDBEntities entities = new LibraryDBEntities())
                {

                    entities.Books.Add(book);
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

        public HttpResponseMessage Put(int id, [FromBody]Book book)
        {
            try
            {
                using (LibraryDBEntities entities = new LibraryDBEntities())
                {
                    var entity = entities.Books.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with id = " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Title = book.Title;
                        entity.Author = book.Author;
                        entity.Edition = book.Edition;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LibraryDBEntities entities = new LibraryDBEntities())
                {
                    var entity = entities.Books.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with id " + id.ToString() + "not found to delete");
                    }
                    else
                    {
                        entities.Books.Remove(entity);
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
