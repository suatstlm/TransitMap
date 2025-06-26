using MediatR;
using System.Transactions;

namespace Applicaton.Pipelines.Transaction;

public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ITransactionalRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            TResponse result = await next();
            transactionScope.Complete();
            return result;
        }
        catch (Exception)
        {
            transactionScope.Dispose();
            throw;
        }
    }
}
