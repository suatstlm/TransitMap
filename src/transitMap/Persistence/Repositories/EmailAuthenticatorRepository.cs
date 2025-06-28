using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Shared.Persistence.Repositories;

namespace Persistence.Repositories;

public class EmailAuthenticatorRepository
    : EfRepositoryBase<EmailAuthenticator, Guid, BaseDbContext>,
        IEmailAuthenticatorRepository
{
    public EmailAuthenticatorRepository(BaseDbContext context)
        : base(context) { }
}
