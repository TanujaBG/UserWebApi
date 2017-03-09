using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1;
using WebApplication1.Repositories;

namespace UnitTestProject1
{
    public class TestRepository : IUserRepository
    {
        #region Public Methods
        public async Task<User> AddUser(User user)
        {
            await Task.Delay(1);
            List<User> users = GetTestUsers();
            if (users.Select(x => x).Where(x => x.Name == user.Name).Count<User>() > 0)
            {
                throw new ArgumentException("Name Present");
            }
            if (user.Id != 0 && users.Select(x => x).Where(x => x.Id == user.Id).Count<User>() > 0)
            {
                throw new ArgumentException("Id Present");
            }

            User newUser = new User() { Id = users.Count, Name = user.Name, Points = user.Points };
            users.Add(newUser);
            return newUser;
        }

        public async Task<User> GetUser(int id)
        {
            await Task.Delay(1);
            List<User> users = GetTestUsers();
            var user = users.Select(x => x).Where(x => x.Id == id).FirstOrDefault();

            return user;
        }

        public async Task<User> SetPoints(User user)
        {
            await Task.Delay(1);
            User updatedUser = GetTestUsers().Where(x => x.Id == user.Id).FirstOrDefault();
            if (updatedUser == null)
            {
                throw new ArgumentException("Id not Present");
            }
            updatedUser.Points = user.Points;
            return updatedUser;
        }

        public IQueryable<User> GetUsers()
        {
            return GetTestUsers().AsQueryable();
        } 
        #endregion

        private List<User> GetTestUsers()
        {
            List<User> users = new List<User>();

            users.Add(new User { Id = 1, Name = "Demo1", Points = 1 });
            users.Add(new User { Id = 2, Name = "Demo2", Points = 2 });
            users.Add(new User { Id = 3, Name = "Demo3", Points = 3 });
            users.Add(new User { Id = 4, Name = "Demo4", Points = 4 });

            return users;
        }
      }
}
