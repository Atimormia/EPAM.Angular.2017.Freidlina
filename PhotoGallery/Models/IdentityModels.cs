using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoGallery.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Uri { get; set; }
        public virtual Album Album { get; set; }
        public int AlbumId { get; set; }
        public DateTime DateUploaded { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }

    public class Album 
    {
        public Album()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Album> Albums { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class DbInitializer: DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Albums.Any())
            {
                List<Album> albums = new List<Album>();

                var album1 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });
                var album2 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 2",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });
                var album3 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 3",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });
                var album4 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 4",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    });

                albums.Add(album1); albums.Add(album2); albums.Add(album3); albums.Add(album4);

                string[] images = Directory.GetFiles("~/Content/images");
                Random rnd = new Random();

                foreach (string image in images)
                {
                    int selectedAlbum = rnd.Next(1, 4);
                    string fileName = Path.GetFileName(image);

                    context.Photos.Add(
                        new Photo()
                        {
                            Title = fileName,
                            DateUploaded = DateTime.Now,
                            Uri = fileName,
                            Album = albums.ElementAt(selectedAlbum)
                        }
                        );
                }

                context.SaveChanges();
            }
        }
    }
}