namespace WebApplication1
{
    using System.Data.Entity;

    public partial class UserManagementEntities : DbContext
    {
        public UserManagementEntities()
            : base("name=UserManagementEntities")
        {
        }
    
        public virtual DbSet<User> Users { get; set; }
    }
}
