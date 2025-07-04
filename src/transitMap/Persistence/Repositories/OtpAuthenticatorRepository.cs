﻿using Application.Services.Repositories;
using Domain.Entities;
using Shared.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, Guid, BaseDbContext>, IOtpAuthenticatorRepository
{
    public OtpAuthenticatorRepository(BaseDbContext context)
        : base(context) { }
}
