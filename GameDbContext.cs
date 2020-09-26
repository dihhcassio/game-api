using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI
{
    public class GameDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=game.db");
        }

        public DbSet<User> Users { get; set; }       
        public DbSet<Game> Games { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<GameLoan> GameLents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User {Id = 1, Email = "teste1@teste.com", Password = "12345", Role = "admin", CreateAt = DateTime.Now, UpdateAt = DateTime.Now, Removed = false });
            modelBuilder.Entity<User>().HasData(new User {Id = 2, Email = "teste2@teste.com", Password = "12345", Role = "user", CreateAt = DateTime.Now, UpdateAt = DateTime.Now, Removed = false });

        }

    }
}
