using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        Task<User> GetUser(int id);
        Task<User> AddUser(User user);
        Task<User> SetPoints(User user);
    }
}