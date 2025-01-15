using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileEFCoreConsole
{
    internal class oldProgram
    {
        static void main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Hello World! Sonu");
            Console.WriteLine("Hello World! Panchal");

            //using var context = new AppDbContext();

            // Add initial data
            //if (!context.Users.Any())
            //{
            //    var user = new User
            //    {
            //        UserId = Guid.NewGuid(),
            //        UserName = "JohnDoe",
            //        Email = "john@example.com",
            //        Profile = new Profile
            //        {
            //            ProfileId = Guid.NewGuid(),
            //            Bio = "Software Developer",
            //            Website = "https://johndoe.dev"
            //        },
            //        Posts = new()
            //        {
            //            new Post
            //            {
            //                PostId = Guid.NewGuid(),
            //                Title = "First Post",
            //                Content = "Hello World!"
            //            }
            //        },
            //        UserRoles = new()
            //        {
            //            new UserRole
            //            {
            //                Role = new Role
            //                {
            //                    RoleId = Guid.NewGuid(),
            //                    RoleName = "Admin"
            //                }
            //            }
            //        }
            //    };

            //    context.Users.AddAsync(user);
            //    context.SaveChangesAsync();
            //    Console.WriteLine("Database seeded successfully.");
            //}
            //else
            //{
            //    Console.WriteLine("Database already contains data.");
            //}


            // using var context = new AppDbContext();

            var userService = new UserService(new AppDbContext());

            // Call CRUD operations
            userService.CreateUser();  // Create
            userService.ReadUsers();   // Read


            //var con = new AppDbContext();
            //con
            // Assume we have a userId to update and delete
            //var userId =  con.Users.FirstOrDefault()?.UserId;

            //if (userId != null)
            {
                userService.UpdateUser(new Guid("45b2870a-7ce0-4bea-99c7-284ccb2689a5")); // Update
                userService.ReadUsers();             // Verify update
                userService.DeleteUser(new Guid("45b2870a-7ce0-4bea-99c7-284ccb2689a5")); // Delete
                userService.ReadUsers();             // Verify delete
            }
        }

    }
}
