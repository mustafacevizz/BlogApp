using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void FillTestData(IApplicationBuilder app)
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
                        new Tag { Text = "web programming", Url="web-programlama",Color=TagColors.success},
                        new Tag { Text = "backend", Url = "backend", Color = TagColors.warning },
                        new Tag { Text = "frontend", Url = "frontend", Color = TagColors.primary },
                        new Tag { Text = "fullstack", Url = "fullstack", Color = TagColors.secondary },
                        new Tag { Text = "php", Url = "php", Color = TagColors.danger });
                }
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "mustafaceviz",Name= "Mustafa Ceviz",Email="info@mcvz.com", Password="123456" ,Image="p1.jpg" },
                    new User { UserName = "aliceviz",Name= "Ali Ceviz",Email="info@acvz.com", Password="123456" ,Image="p2.jpg" });

                context.SaveChanges();
            }
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "Asp.net Core",
                        Content = "Lesson asp.net core",
                        Url="asp-netcore",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        Image="1.jpg",
                        UserId = 1,
                        Comments=new List<Comment>
                        {
                            new Comment{Text="A useful course", PublishedOn=DateTime.Now.AddDays(-20),UserId=1},
                            new Comment{Text="A course that I benefited greatly from", PublishedOn=DateTime.Now.AddDays(-10),UserId=2},
                        }
                    },
                    new Post
                    {
                        Title = "Php",
                        Description= "Lesson Php core",
                        Content = "Lesson Php core",
                        Url = "php",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        UserId = 1
                    },
                    new Post
                    {
                        Title = "Django",
                        Description = "Lesson Django",
                        Content = "Lesson Django",
                        Url = "django",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-30),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "3.jpg",
                        UserId = 2
                    }, new Post
                    {
                        Title = "React",
                        Description = "Lesson React",
                        Content = "Lesson React",
                        Url = "react",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-40),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        UserId = 1
                    },
                    new Post
                    {
                        Title = "Angular",
                        Content = "Lesson Angular",
                        Url = "angular",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-50),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "2.jpg",
                        UserId = 1
                    });
                context.SaveChanges();
            }

        }
    }
}
