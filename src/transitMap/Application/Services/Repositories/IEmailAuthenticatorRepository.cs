using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEmailAuthenticatorRepository: IAsyncRepository<EmailAuthenticator, Guid>, IRepository<EmailAuthenticator, Guid> { }
