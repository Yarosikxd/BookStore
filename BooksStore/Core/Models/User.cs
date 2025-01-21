namespace Core.Models
{
    public enum UserRole
    {
        Admin,
        User
    }

    public class User
    {

        private User(Guid id, string name, string email, string passwordHash, UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.User;

        public static User Create(Guid id, string name, string email, string passwordHash, UserRole role)
        {
            return new User(id, name, email, passwordHash, role);
        }
    }
}
