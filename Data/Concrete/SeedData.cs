using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Entity.Tag { Text = "Web Programlama",Url = "Web-Programlama"},
                        new Entity.Tag { Text = "Backend", Url = "Backend" },
                        new Entity.Tag { Text = "Frontend", Url = "Frontend" },
                        new Entity.Tag { Text = "Fullstack", Url = "Fullstack" },
                        new Entity.Tag { Text = "php", Url = "php" }
                     );
                    context.SaveChanges();
                }
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new Entity.User {UserName="mustafaalmaz" } ,
                    new Entity.User {UserName="tedesco" } 
                );
                context.SaveChanges();
            }
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Entity.Post
                    {
                        Title = "asp .net core",
                        Content = "asp .net core dersleri",
                        Url = "asp-net-core",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "1.png",
                        UserId = 1
                    },
                    new Entity.Post
                    {
                        Title = "Java",
                        Content = "Java dersleri",
                        Url = "Java",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "2.jpg",
                        UserId = 1
                    },
                    new Entity.Post
                    {
                        Title = "React.js",
                        Content = "React.js dersleri",
                        Url = "react.js",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "3.jpg",
                        UserId = 1
                    },
                    new Entity.Post
                    {
                        Title = "MongoDb",
                        Content = "MongoDb dersleri",
                        Url = "mongodb",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "3.jpg",
                        UserId = 1
                    },
                    new Entity.Post
                    {
                        Title = "Angular",
                        Content = "Angular dersleri",
                        Url = "angular",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "3.jpg",
                        UserId = 1
                    }
               );

                context.SaveChanges();
            }

        }
    }
}
