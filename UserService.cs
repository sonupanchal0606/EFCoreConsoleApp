using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileEFCoreConsole
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Create Operation
        public void CreateUser()
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = "JaneDoe",
                Email = "jane@example.com",
                Profile = new Profile
                {
                    ProfileId = Guid.NewGuid(),
                    Bio = "Data Scientist",
                    Website = "https://janedoe.ai"
                },
                Posts = new()
                {
                    new Post
                    {
                        PostId = Guid.NewGuid(),
                        Title = "AI Post",
                        Content = "Artificial Intelligence is the future."
                    }
                },
                UserRoles = new()
                {
                    new UserRole
                    {
                        Role = new Role
                        {
                            RoleId = Guid.NewGuid(),
                            RoleName = "Editor"
                        }
                    }
                }
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            Console.WriteLine("User added successfully.");
        }

        // Read Operation
        public void ReadUsers()
        {
            var users = _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Posts)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.UserName}, Email: {user.Email}");
                Console.WriteLine($"  Profile: Bio: {user.Profile?.Bio}, Website: {user.Profile?.Website}");
                Console.WriteLine("  Posts:");
                foreach (var post in user.Posts)
                {
                    Console.WriteLine($"    Title: {post.Title}, Content: {post.Content}");
                }
                Console.WriteLine("  Roles:");
                foreach (var userRole in user.UserRoles)
                {
                    Console.WriteLine($"    Role: {userRole.Role.RoleName}");
                }
            }
        }


        // Update Operation
        public void UpdateUser(Guid userId)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.Posts)
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                // Simulate an update
                user.Email = "updated_jane@example.com";

                //_context.SaveChanges();

                var p = new Post
                {
                    PostId = Guid.NewGuid(),
                    Title = "TAI Post",
                    Content = "TArtificial Intelligence is the future.",
                    UserId = userId
                };

                _context.Posts.Add(p);
                user.Posts.Add(p);
                _context.SaveChanges();
                Console.WriteLine("User updated successfully.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Concurrency conflict detected. Please retry.");
            }
        }

/*
        public void UpdateUser(Guid userId)
        {
            try
            {
                var user = _context.Users
                    .Include(u => u.Posts)
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                // Simulate an update
                user.Email = "updated_jane@example.com";

                //_context.SaveChanges();

                var p = new Post
                {
                    PostId = Guid.NewGuid(),
                    Title = "TAI Post",
                    Content = "TArtificial Intelligence is the future.",
                    UserId = userId
                };

                _context.Posts.Add(p);
                user.Posts.Add(p);
                _context.SaveChanges();
                Console.WriteLine("User updated successfully.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Concurrency conflict detected. Please retry.");
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is User)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        if (databaseValues == null)
                        {
                            Console.WriteLine("The entity was deleted by another process.");
                        }
                        else
                        {
                            Console.WriteLine("Resolving conflict... Using database values.");
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }*/

        public void DeleteUser(Guid userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
                Console.WriteLine("User deleted successfully.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Concurrency conflict detected while deleting. Please retry.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
