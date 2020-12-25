using AspNetNote.MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetNote.MVC.DataContext
{
    public class AspNetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-3CT1070\SQLEXPRESS;Database=AspnetNoteDb;User Id=sa;Password=1q2w3e4r;");
            //base.OnConfiguring(optionsBuilder);
        }
    }
}
