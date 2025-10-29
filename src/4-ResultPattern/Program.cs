// See https://aka.ms/new-console-template for more information
using ResultPattern;

Console.WriteLine("Testing Result Pattern Implementation:\n");

var userService = new UserService();

// Test GetUser with valid and invalid IDs
Console.WriteLine("Testing GetUser:");
Console.WriteLine("----------------");
var validUserResult = userService.GetUser(1);
var invalidUserResult = userService.GetUser(-1);

validUserResult
    .OnSuccess(user => Console.WriteLine($"Found user: {user.Name} ({user.Email})"))
    .OnFailure(error => Console.WriteLine($"Error: {error}"));

invalidUserResult
    .OnSuccess(user => Console.WriteLine($"Found user: {user.Name} ({user.Email})"))
    .OnFailure(error => Console.WriteLine($"Error: {error}"));

// Test Email Validation
Console.WriteLine("\nTesting Email Validation:");
Console.WriteLine("------------------------");
var validEmailResult = userService.ValidateEmail("john@example.com");
var invalidEmailResult = userService.ValidateEmail("invalid-email");
var emptyEmailResult = userService.ValidateEmail("");

validEmailResult
    .OnSuccess(email => Console.WriteLine($"Valid email: {email}"))
    .OnFailure(error => Console.WriteLine($"Error: {error}"));

invalidEmailResult
    .OnSuccess(email => Console.WriteLine($"Valid email: {email}"))
    .OnFailure(error => Console.WriteLine($"Error: {error}"));

emptyEmailResult
    .OnSuccess(email => Console.WriteLine($"Valid email: {email}"))
    .OnFailure(error => Console.WriteLine($"Error: {error}"));

// Test Complete User Workflow
Console.WriteLine("\nTesting User Workflow:");
Console.WriteLine("----------------------");
userService.ProcessUserWorkflow();