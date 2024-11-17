using Microsoft.AspNetCore.Authorization;

namespace Reach.Security;

[AttributeUsage(AttributeTargets.All)]
public class TenantRequiredAttribute : Attribute, IAuthorizationRequirement,
                                     IAuthorizationRequirementData
{
    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}