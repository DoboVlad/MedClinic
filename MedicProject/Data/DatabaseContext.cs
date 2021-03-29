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
        
        public DbSet<User> users { get; set; }

        public DbSet<Appointments> appointments { get; set; }

        public DbSet<Message> messages {get; set;}

        public DbSet<Schedule> schedules {get; set;}

        public DbSet<Hour> hours {get; set;}

        public DbSet<Result> result { get; set; }

        public DbSet<Medicine> medicine { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasOne(u => u.Receiver)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Transmitter)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointments>()
                .HasOne(u => u.User)
                .WithMany(a => a.Appointments)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.Entity<User>()
                .HasMany(u => u.Appointments)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
