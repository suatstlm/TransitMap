namespace Shared.Applicaton.Pipelines.Authorization;
public interface ISecuredRequest
{
    string[] Roles { get; }
}