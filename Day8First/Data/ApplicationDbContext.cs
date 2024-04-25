﻿using Day8First.Models;
using Microsoft.EntityFrameworkCore;

namespace Day8First.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }  
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }  
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }    
    }
}
