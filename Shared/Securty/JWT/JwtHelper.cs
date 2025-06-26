
using Microsoft.IdentityModel.Tokens;
using Securty.Encryption;
using Securty.Entities;
using Securty.Extensions;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Securty.JWT;

public class JwtHelper<TUserId, TOperationClaimId, TRefreshTokenId> : ITokenHelper<TUserId, TOperationClaimId, TRefreshTokenId>
{
    private readonly TokenOptions _tokenOptions;

    public JwtHelper(TokenOptions tokenOptions)
    {
        _tokenOptions = tokenOptions;
    }

    public virtual AccessToken CreateToken(User<TUserId> user, IList<OperationClaim<TOperationClaimId>> operationClaims)
    {
        DateTime dateTime = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey));
        JwtSecurityToken token = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims, dateTime);
        string token2 = new JwtSecurityTokenHandler().WriteToken(token);
        return new AccessToken
        {
            Token = token2,
            ExpirationDate = dateTime
        };
    }

    public RefreshToken<TRefreshTokenId, TUserId> CreateRefreshToken(User<TUserId> user, string ipAddress)
    {
        return new RefreshToken<TRefreshTokenId, TUserId>
        {
            UserId = user.Id,
            Token = randomRefreshToken(),
            ExpirationDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            CreatedByIp = ipAddress
        };
    }

    public virtual JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User<TUserId> user, SigningCredentials signingCredentials, IList<OperationClaim<TOperationClaimId>> operationClaims, DateTime accessTokenExpiration)
    {
        return new JwtSecurityToken(tokenOptions.Issuer, tokenOptions.Audience, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: SetClaims(user, operationClaims), signingCredentials: signingCredentials);
    }

    protected virtual IEnumerable<Claim> SetClaims(User<TUserId> user, IList<OperationClaim<TOperationClaimId>> operationClaims)
    {
        List<Claim> list = new List<Claim>();
        list.AddNameIdentifier(user.Id.ToString());
        list.AddEmail(user.Email);
        list.AddRoles(operationClaims.Select((OperationClaim<TOperationClaimId> c) => c.Name).ToArray());
        return list.ToImmutableList();
    }

    private string randomRefreshToken()
    {
        byte[] array = new byte[32];
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(array);
        return Convert.ToBase64String(array);
    }
}
