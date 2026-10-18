using SimplePay.Communication.Enums;

namespace SimplePay.Communication.Responses;

public class RegisteredUserResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserType Type { get; set; }
}
