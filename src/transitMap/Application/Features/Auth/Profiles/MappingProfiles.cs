using Application.Features.Auth.Commands.RevokeToken;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Shared.Security.Entities.RefreshToken<Guid, Guid>, RefreshToken>().ReverseMap();
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
    }
}
