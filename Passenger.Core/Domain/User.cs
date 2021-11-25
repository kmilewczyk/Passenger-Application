using System.ComponentModel.DataAnnotations;

namespace Passenger.Core.Domain;

public class User
{
    public Guid Id { get; protected set; }

    public string Email { get; protected set; }

    public string Username { get; protected set; }

    public string Password { get; protected set; }

    public string Salt { get; protected set; }

    public string Fullname { get; protected set; }
    public UserRole Role { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    private User(UserRole role)
    {
        Role = role;
    }

    public User(Guid userId, string email, string userName, string password, string salt, UserRole role)
    {
        Id = userId;
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is invalid");
        }

        Email = email.ToLowerInvariant();
        Username = userName;
        Password = password;
        Salt = salt;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }
}