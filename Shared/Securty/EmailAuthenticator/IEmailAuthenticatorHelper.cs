namespace Securty.EmailAuthenticator;
public interface IEmailAuthenticatorHelper
{
    Task<string> CreateEmailActivationKey();

    Task<string> CreateEmailActivationCode();
}
