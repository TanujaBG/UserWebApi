using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<User> Get()
        {
            List<User> userList = new List<WebApplication1.User>();
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=UserManagement;Integrated Security=SSPI"))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User]", connection))
            {
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            User p = new User();
                            // To avoid unexpected bugs access columns by name.
                            p.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            p.Name = reader.GetString(reader.GetOrdinal("Name"));
                            p.Points = reader.GetInt32(reader.GetOrdinal("Points"));
                            userList.Add(p);
                        }
                    }
                }
            }
            return userList;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
