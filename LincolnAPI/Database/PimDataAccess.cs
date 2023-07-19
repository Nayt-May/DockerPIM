using LincolnAPI.DBModels.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace LincolnAPI.Database
{
    public class PimDataAccess : DbContext
    {
        public PimDataAccess(DbContextOptions<PimDataAccess> options) :base(options) { }

        //Put records here to represent tables.

        //e.g. public DbSet<Indentity> Indentities { get; set; };
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
