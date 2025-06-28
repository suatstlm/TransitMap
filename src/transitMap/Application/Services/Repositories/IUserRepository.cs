﻿using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User, Guid>, IRepository<User, Guid> { }
