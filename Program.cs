using Microsoft.EntityFrameworkCore;
using System;

namespace UserProfileEFCoreConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Hello World! Sonu");
            Console.WriteLine("Hello World! Panchal");

            using var context = new AppDbContext();

            // Add initial data
            if (!context.Users.Any())
            {
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "JohnDoe",
                    Email = "john@example.com",
                    Profile = new Profile
                    {
                        ProfileId = Guid.NewGuid(),
                        Bio = "Software Developer",
                        Website = "https://johndoe.dev"
                    },
                    Posts = new()
                    {
                        new Post
                        {
                            PostId = Guid.NewGuid(),
                            Title = "First Post",
                            Content = "Hello World!"
                        }
                    },
                    UserRoles = new()
                    {
                        new UserRole
                        {
                            Role = new Role
                            {
                                RoleId = Guid.NewGuid(),
                                RoleName = "Admin"
                            }
                        }
                    }
                };

                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Database seeded successfully.");
            }
            else
            {
                Console.WriteLine("Database already contains data.");
            }

            var userService = new UserService(context);

            // Call CRUD operations
            userService.CreateUser();  // Create
            userService.ReadUsers();   // Read

            // Assume we have a userId to update and delete
            var userId = context.Users.FirstOrDefault()?.UserId;

            if (userId != null)
            {
                userService.UpdateUser(userId.Value); // Update
                userService.ReadUsers();        // Verify update
                userService.DeleteUser(userId.Value); // Delete
                userService.ReadUsers();        // Verify delete
            }

        }
    }
}

// o not need to push bin, obj folder, .vs ---> these files will get recreated using build 