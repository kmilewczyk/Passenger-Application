using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain;

public class User
{
    private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
    private string _password;
    private string _email;
    private string _username;
    public Guid Id { get; protected set; }

    public string Email
    {
        get => _email;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email cannot be empty");
            }

            _email = value.ToLowerInvariant();
        }
    }

    public string Username
    {
        get => _username;
        protected set
        {
            if (!NameRegex.IsMatch(value))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username is invalid");
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username cannot be empty");
            }

            _username = value.ToLowerInvariant();
        }
    }

    public string Password
    {
        get => _password;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password cannot be empty");
            }

            if (value.Length < 4)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must contain at least 4 character");
            }

            if (value.Length > 100)
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password cannot contain more than 100 characters.");
            }

            _password = value;
        }
    }

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
        Email = email;
        Username = userName;
        Password = password;
        Salt = salt;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }
}