using serverstudent.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace serverstudent.Data
{
    public class ApplicationDbContext : DbContext
    {
       
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Student> students { get; set; }  
        }
    
}
