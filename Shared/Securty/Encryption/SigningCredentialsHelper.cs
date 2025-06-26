using Microsoft.IdentityModel.Tokens;

namespace Securty.Encryption;
public static class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512");
    }
}
