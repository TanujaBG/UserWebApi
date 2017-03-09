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
using WebApplication1;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class UsersController : ApiController
    {
        private IUserRepository repository;
        public UsersController()
        {
            repository = new UserRepository();
        }

        public UsersController(IUserRepository repo)
        {
            repository = repo;
        }
        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return repository.GetUsers();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await repository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }        

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User newUser = null;
            try
            {
                newUser = await repository.AddUser(user);
            }
            catch(ArgumentException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                throw ;
            }
  
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }      

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}