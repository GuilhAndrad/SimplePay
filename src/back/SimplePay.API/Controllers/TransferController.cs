using Microsoft.AspNetCore.Mvc;
using SimplePay.Application.UseCases.Transfer.CreateTransfer;
using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;

namespace SimplePay.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(TransferResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromServices] ICreateTransfer useCase,
        [FromBody] TransferRequest request,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(request, cancellationToken);

        return Created(string.Empty, result);
    }
}