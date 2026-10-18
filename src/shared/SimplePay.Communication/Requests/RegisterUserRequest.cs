using SimplePay.Communication.Enums;

namespace SimplePay.Communication.Requests;

public class RegisterUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType Type { get; set; }
}