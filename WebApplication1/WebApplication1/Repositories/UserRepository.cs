using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Repositories
{
    public class UserRepository: IUserRepository
    {
        private UserManagementEntities db  = new UserManagementEntities();

        #region Public Methods
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        public async Task<User> GetUser(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            if (UserNameExists(user.Name))
            {
                throw new ArgumentException("Name Present");
            }
            db.Users.Add(user);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id) != null)
                {
                    throw new ArgumentException("Id Present");
                }
                else
                {
                    throw;
                }
            }
            return user;
        }

        public async Task<User> SetPoints(User user)
        {
            User res = UserExists(user.Id);
            if (res == null)
            {
                throw new ArgumentException("Id Not Found");
            }
            res.Points = user.Points;
            db.Entry(res).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return res;
        }

        #endregion

        #region PrivateMethods
        private bool UserNameExists(string name)
        {
            return db.Users.Count(e => e.Name == name) > 0;
        }
        private User UserExists(int id)
        {
            return db.Users.Where(e => e.Id == id).FirstOrDefault();
        }
        #endregion

    }
}