using MedicProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicProject.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<User> USERS { get; set; }

        public DbSet<Appointments> APPOINTMENTS { get; set; }
    }
}
