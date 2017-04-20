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
using LMS.Models;

namespace LMS.Controllers
{
    public class UsersController : ApiController
    {
        [Authorize]
        public IEnumerable<User> Get()
        {


            using (LibraryDBEntities entities = new LibraryDBEntities())
            {
                return entities.Users.ToList();
            }
        }

        public HttpResponseMessage LoadUserById(int id)
        {
            using (LibraryDBEntities entities = new LibraryDBEntities())
            {
                var entity = entities.Users.FirstOrDefault(e => e.UserID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with id = " + id.ToString() + "not found");
                }
            }
        }
       
    }
}