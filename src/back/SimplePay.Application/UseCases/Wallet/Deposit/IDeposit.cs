using SimplePay.Communication.Requests;
using SimplePay.Communication.Responses;

namespace SimplePay.Application.UseCases.Wallet.Deposit;

public interface IDeposit
{
    Task<DepositResponse> Execute(DepositRequest request, CancellationToken cancellationToken);
}