using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StapleIT.Models;
using System.Text.RegularExpressions;

namespace StapleIT.DAL
{
    public class StapleITContext:DbContext
    {
        public StapleITContext(DbContextOptions<StapleITContext> options) : base(options) { }

        //map classes to tables in database
        public DbSet<User> User { get; set; }
        public DbSet<Models.Group> Group { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<Item> Item { get; set; }

        public DbSet<List> List { get; set; }
        public DbSet<ListDetail> ListDetail { get; set; }

    }
}
