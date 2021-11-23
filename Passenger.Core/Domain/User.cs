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
    
    public DateTime CreatedAt { get; protected set; }
    
    private User() {}

    public User(string email, string userName, string password, string salt)
    {
        Id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is invalid");
        }
        Email = email.ToLowerInvariant();
        Username = userName;
        Password = password;
        Salt = salt;
        CreatedAt = DateTime.UtcNow;
    }
}