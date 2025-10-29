using Shared.Models;

namespace ResultPattern;

public class UserService
{
    public Result<User> GetUser(int userId)
    {
        // Simulate database call
        if (userId <= 0)
            return Result<User>.Failure("Invalid user ID");

        var user = new User { Id = userId, Name = "John Doe", Email = "john@example.com" };
        return Result<User>.Success(user);
    }

    public Result<string> ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<string>.Failure("Email is required");

        if (!email.Contains("@"))
            return Result<string>.Failure("Invalid email format");

        return Result<string>.Success(email);
    }

    public void ProcessUserWorkflow()
    {
        var result = GetUser(1)
            .Map(user => user.Email)  // Transform to email
            .Bind(email => ValidateEmail(email))  // Chain validation
            .OnSuccess(email => Console.WriteLine($"Valid email: {email}"))
            .OnFailure(error => Console.WriteLine($"Error: {error}"));

        // Pattern matching
        var message = result.Match(
            onSuccess: email => $"Email processed: {email}",
            onFailure: error => $"Failed to process: {error}"
        );

        Console.WriteLine(message);

        // Use custom operator
        var fallbackResult = result | Result<string>.Success("fallback@example.com");
    }
}
