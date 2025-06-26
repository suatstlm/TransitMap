
using System.Security.Cryptography;

namespace Securty.EmailAuthenticator;

public class EmailAuthenticatorHelper : IEmailAuthenticatorHelper
{
    public virtual Task<string> CreateEmailActivationKey()
    {
        return Task.FromResult(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)));
    }

    public virtual Task<string> CreateEmailActivationCode()
    {
        return Task.FromResult(RandomNumberGenerator.GetInt32(Convert.ToInt32(Math.Pow(10.0, 6.0))).ToString().PadLeft(6, '0'));
    }
}