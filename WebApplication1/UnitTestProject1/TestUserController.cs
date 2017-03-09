using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Http.Results;
using WebApplication1;
using WebApplication1.Controllers;
using WebApplication1.Repositories;

namespace UnitTestProject1
{
    [TestClass]
    public class TestUserController 
    {
        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            IUserRepository repository = new TestRepository();

             var controller = new UsersController(repository);
            var result = controller.GetUsers();
            Assert.IsNotNull(result);                         
            Assert.AreEqual(4, result.Count<User>());
        }

        [TestMethod]
        public void GetUser_ShouldReturnUserWithSameID()
        {
            IUserRepository repository = new TestRepository();

            var controller = new UsersController(repository);
            var result = controller.GetUser(3).Result as OkNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Id);            
        }

        [TestMethod]
        public void GetUser_ShouldReturnUserWith_InvalidID()
        {
            IUserRepository repository = new TestRepository();

            var controller = new UsersController(repository);
            var result = controller.GetUser(5).Result as NotFoundResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostUser_ShouldReturnSameUser()
        {
            IUserRepository repository = new TestRepository();
            var controller = new UsersController(repository);

            var item = new User() { Name = "Demo name", Points = 5 };

            var result =  controller.PostUser(item).Result as CreatedAtRouteNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Name, item.Name);
        }

        [TestMethod]
        public void PostUser_ShouldReturnException_ExisitngUser()
        {
            IUserRepository repository = new TestRepository();
            var controller = new UsersController(repository);

            var item = new User() { Name = "Demo4", Points = 5 };

            var result = controller.PostUser(item).Result as ConflictResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostSetPoints_ShouldReturnSameUser()
        {
            IUserRepository repository = new TestRepository();
            var controller = new setPointsController(repository);

            var item = new User() { Id=2, Points = 20 };

            var result = controller.PostUser(item).Result as CreatedAtRouteNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Points, item.Points);
        }

        [TestMethod]
        public void PostSetPoints_Name_ShouldNot_Update()
        {
            IUserRepository repository = new TestRepository();
            var controller = new setPointsController(repository);

            var item = new User() { Id = 2,Name ="", Points = 20 };

            var result = controller.PostUser(item).Result as CreatedAtRouteNegotiatedContentResult<User>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Points, item.Points);
            Assert.AreNotEqual(result.Content.Name, item.Name);
        }

        //TODO: When there are no records in DB, it should not throw exception
        //TODO: SetPoints sending invalid ID
    }
}
