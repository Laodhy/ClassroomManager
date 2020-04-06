using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Models.Users;
using Web.Models.Classrooms;

namespace Web.Data
{
    public class WebContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public WebContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebContext"));
        }

        public DbSet<Web.Models.Users.User> User { get; set; }

        public DbSet<Web.Models.Classrooms.Classroom> Classroom { get; set; }

    }
}
