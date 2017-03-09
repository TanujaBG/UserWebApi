using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class setPointsController : ApiController
    {
        private IUserRepository repository;
        public setPointsController()
        {
            repository = new UserRepository();
        }

        public setPointsController(IUserRepository rep)
        {
            repository = rep;
        }

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
                newUser = await repository.SetPoints(user);
            }
            catch (ArgumentException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = newUser.Id }, newUser);
        }
    }
}
