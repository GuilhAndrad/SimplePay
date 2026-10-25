using Microsoft.AspNetCore.Mvc;
using SimplePay.Application.UseCases.Wallet.Deposit;
using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;

namespace SimplePay.API.Controllers;

[Route("[controller]")]
[ApiController]
public class WalletsController : ControllerBase
{
    [HttpPost("deposit")]
    [ProducesResponseType(typeof(DepositResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deposit(
        [FromServices] IDeposit useCase,
        [FromBody] DepositRequest request,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(request, cancellationToken);

        return Ok(result);
    }
}