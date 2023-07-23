global using Microsoft.EntityFrameworkCore;
using BasicWebAPI.Models;

namespace BasicWebAPI.DbContexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Company> Companies { get;  set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }

        

    }
}
