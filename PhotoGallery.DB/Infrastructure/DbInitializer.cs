using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<PhotosDB>
    {
        protected override void Seed(PhotosDB context)
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

                string[] images = Directory.GetFiles("~/Images");
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
                if (!context.Roles.Any())
                {
                    // create roles
                    context.Roles.AddRange(new[] { new Role { Name="Admin"}});
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.Add(new User()
                    {
                        Email = "pfreydlin@gmail.com",
                        Username = "polina",
                        HashedPassword = "9wsmLgYM5Gu4zA/BSpxK2GIBEWzqMPKs8wl2WDBzH/4=",
                        Salt = "GTtKxJA6xJuj3ifJtTXn9Q==",
                        IsLocked = false,
                        DateCreated = DateTime.Now
                    });
                    // create user-admin for chsakell
                    context.UserRoles.AddRange(new[] { new UserRole { RoleId = 1, UserId = 1 }});
                }
                context.SaveChanges();
            }
        }
    }
}