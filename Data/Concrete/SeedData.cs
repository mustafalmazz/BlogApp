using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Reflection;

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
                        new Entity.Tag { Text = "Web Programlama", Url = "Web-Programlama", Color = Entity.TagColors.warning },
                        new Entity.Tag { Text = "Backend", Url = "Backend", Color = Entity.TagColors.info },
                        new Entity.Tag { Text = "Frontend", Url = "Frontend", Color = Entity.TagColors.success },
                        new Entity.Tag { Text = "Fullstack", Url = "Fullstack", Color = Entity.TagColors.secondary },
                        new Entity.Tag { Text = "php", Url = "php", Color = Entity.TagColors.primary }
                     );
                    context.SaveChanges();
                }
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new Entity.User { UserName = "mustafaalmaz", Name="Mustafa Almaz", Email="admin@gmail.com",Password="admin" ,Image = "Kırpıldı.png" },
                    new Entity.User { UserName = "Kerem_1907", Name = "Kerem Aktürkoğlu", Email = "kerem1907@gmail.com", Password = "1907", Image = "kerem.png" }
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
                        Description = "asp .net core dersleri",
                        Url = "asp-net-core",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "1.png",
                        UserId = 1,
                        Comments = new List<Comment> {
                        new Comment{Text = "iyi bir kurs ama daha da iyileştirilebilir 6/10" , PublishedOn = DateTime.Now.AddDays(-20) ,UserId = 1},
                        new Comment {Text = "Kursun Teknik Detayları Çok Fazla ve Bu Kalite Hoşuma Gitti :)" , PublishedOn = DateTime.Now.AddDays(-10).AddHours(14) ,UserId = 2}
                        }
                    },
                    new Entity.Post
                    {
                        Title = "Java",
                        Content = "Java dersleri",
                        Description = "Java dersleri",
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
                        Description = "React.js dersleri",
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
                        Description = "MongoDb dersleri",
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
                        Description = "Angular dersleri",
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
