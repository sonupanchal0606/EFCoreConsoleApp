using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileEFCoreConsole
{
    // One-to-One Relationship
    public class User
    {
        public Guid UserId { get; set; } // Primary Key
        public string UserName { get; set; }
        public string Email { get; set; }

        // One-to-One: Navigation property
        public Profile Profile { get; set; }

        // One-to-Many: Navigation property
        public List<Post> Posts { get; set; } = new();

        // Many-to-Many: Navigation property
        public List<UserRole> UserRoles { get; set; } = new();

    }

    public class Profile
    {
        public Guid ProfileId { get; set; } // Primary Key
        public string Bio { get; set; }
        public string Website { get; set; }

        // Foreign Key for One-to-One Relationship
        public Guid UserId { get; set; }
        public User User { get; set; }

    }

    // One-to-Many Relationship
    public class Post
    {
        public Guid PostId { get; set; } // Primary Key
        public string Title { get; set; }
        public string Content { get; set; }

        // Foreign Key for One-to-Many
        public Guid UserId { get; set; }
        public User User { get; set; }

    }

    // Many-to-Many Relationship with Bridging Table
    public class Role
    {
        public Guid RoleId { get; set; } // Primary Key
        public string RoleName { get; set; }

        // Navigation property
        public List<UserRole> UserRoles { get; set; } = new();
    }

    public class UserRole
    {
        // Bridging table for Many-to-Many Relationship
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
