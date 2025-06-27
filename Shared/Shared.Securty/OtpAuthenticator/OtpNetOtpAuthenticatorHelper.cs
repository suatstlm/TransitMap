using OtpNet;

namespace Shared.Securty.OtpAuthenticator;

public class OtpNetOtpAuthenticatorHelper : IOtpAuthenticatorHelper
{
    public Task<byte[]> GenerateSecretKey()
    {
        return Task.FromResult(Base32Encoding.ToBytes(Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20))));
    }

    public Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        return Task.FromResult(Base32Encoding.ToString(secretKey));
    }

    public Task<bool> VerifyCode(byte[] secretKey, string code)
    {
        return Task.FromResult(new Totp(secretKey).ComputeTotp(DateTime.UtcNow) == code);
    }
}
