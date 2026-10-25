using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;

namespace SimplePay.Application.UseCases.Transfer.CreateTransfer;

public interface ICreateTransfer
{
    Task<TransferResponse> Execute(TransferRequest request, CancellationToken cancellationToken);
}