using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormAuthenticationTest.Models
{
    public class EFDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(d => d.Roles).WithMany(r => r.Users).Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                    m.ToTable("UserAndRole");
                });

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DropCreateDatabaseIfModelChanged : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            context.Books.AddRange(new Book[]
                {
                    new Book()
                        {
                            Author = "Joseph Albahari and Ben Albahari",
                            Isbn = "978-1-449-32010-2",
                            Price = 329,
                            Published = new DateTime(2012, 6, 1),
                            Summary = "C# 5.0 represents the fourth major update to Microsoft’s " +
                                      "flagship programminglanguage, positioning C# as a language " +
                                      "with unusual flexibility and breadth.",
                            Title = "C# 5.0 IN A NUTSHELL"
                        },
                    new Book()
                        {
                            Author = "Jess Chadwick, Todd Snyder, and Hrusikesh Panda",
                            Isbn = "978-1-449-32031-7",
                            Price = 287.5,
                            Published = new DateTime(2012, 10, 1),
                            Summary = "",
                            Title = "Programming ASP.NET MVC 4"
                        }
                });

            context.Users.Add(new User()
                {
                    UserName = "york",
                    Password = "123456",
                    Email = "villagearcher@gmail.com",
                    LastActiveDate = new DateTime(2013,9,26),
                    Roles = new List<Role>()
                        {
                            new Role()
                                {
                                    RoleName = "Admin",
                                    Description = "Has the highest authority.",
                                    CreDate = new DateTime(2013,9,26),
                                    UpdDate = new DateTime(2013,9,26)
                                }
                        }
                });
            base.Seed(context);
        }
    }
}