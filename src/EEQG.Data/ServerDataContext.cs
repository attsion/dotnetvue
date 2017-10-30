using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


using EEQG.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EEQG.ServerData.Models;

namespace EEQG.ServerData
{
    public class ServerDataContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<EeqgPeople_Server> Peoples { get; set; }
        public DbSet<TransFile_Service> TransFiles { get; set; }
        public DbSet<EeqgPeopleScale> PeopleScales { get; set; }

        public ServerDataContext(DbContextOptions conn) : base(conn)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
