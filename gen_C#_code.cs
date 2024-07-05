```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;

public class User
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [PasswordComplexity]
    public string Password { get; set; }

    public byte[] ProfilePicture { get; set; }
}

public class PasswordComplexityAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;

        if (password.Length < 8)
            return new ValidationResult("Password must be at least 8 characters long.");

        if (!Regex.IsMatch(password, "[A-Z]"))
            return new ValidationResult("Password must contain at least one uppercase letter.");

        if (!Regex.IsMatch(password, "[a-z]"))
            return new ValidationResult("Password must contain at least one lowercase letter.");

        if (!Regex.IsMatch(password, "[0-9]"))
            return new ValidationResult("Password must contain at least one number.");

        if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            return new ValidationResult("Password must contain at least one special character.");

        return ValidationResult.Success;
    }
}

public class UserService
{
    private UserDbContext _dbContext;
    private IEmailService _emailService;

    public UserService(UserDbContext dbContext, IEmailService emailService)
    {
        _dbContext = dbContext;
        _emailService = emailService;
    }

    public void RegisterUser(User user)
    {
        if (_dbContext.Users.Any(u => u.Email == user.Email))
            throw new Exception("Email is already registered.");

        user.Password = HashPassword(user.Password);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        _emailService.SendVerificationEmail(user);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}

public interface IEmailService
{
    void SendVerificationEmail(User user);
}

public class EmailService : IEmailService
{
    public void SendVerificationEmail(User user)
    {
        var mailMessage = new MailMessage();
        mailMessage.To.Add(user.Email);
        mailMessage.Subject = "Verify your account";
        mailMessage.Body = "Please click the link to verify your account.";

        var smtpClient = new SmtpClient();
        smtpClient.Send(mailMessage);
    }
}
```