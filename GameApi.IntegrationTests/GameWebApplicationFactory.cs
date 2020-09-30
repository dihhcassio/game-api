using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GameAPI;
using GameAPI.Models;
using Newtonsoft.Json;
using System.Text;
using System;

namespace GameApi.IntegrationTests
{
    public class GameWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<GameDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<GameDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbGame");
                });

                 var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<GameDbContext>();

                    
                    InitializeDbForTests(db);
                }
            });
        }

        public static void InitializeDbForTests(GameDbContext db)
        {
            db.Users.Add(new User 
            {
                Email = "teste1@teste.com", 
                Password = "12345", 
                Role = "admin", 
                CreateAt = DateTime.Now, 
                UpdateAt = DateTime.Now, 
                Removed = false 
            });

            db.Games.Add(new Game()
            {
                Title = "Jogo Teste 1 ",
                Category = "Categoria Teste1 ", 
                Removed = false,
                CreateAt = DateTime.Now
            });

            db.Games.Add(new Game()
             {
                Title = "Jogo Teste 2 ",
                Category = "Categoria Teste 2", 
                Removed = false,
                CreateAt = DateTime.Now
            });

            db.Friends.Add(new Friend()
            {
                Name = "Amigo 1",
                Phone = "77988885555",
                Removed = false, 
                CreateAt = DateTime.Now
            });

            db.SaveChanges();
        }
    }
}