using Microsoft.AspNetCore.Mvc;
using SimplePay.Application.UseCases.User.RegisterUser;
using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;

namespace SimplePay.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisteredUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUser useCase,
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(request, cancellationToken);

        return Created(string.Empty, result);
    }
}