namespace Shared.Application.Pipelines.Authorization;
public interface ISecuredRequest
{
    string[] Roles { get; }
}