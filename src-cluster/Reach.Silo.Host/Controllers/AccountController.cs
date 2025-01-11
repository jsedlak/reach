using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reach.Cqrs;
using Reach.Membership.Commands;
using Reach.Membership.Views;
using Reach.Security;
using Reach.Silo.Membership.GrainModel;
using Reach.Silo.Membership.ServiceModel;
using System.Reflection;

namespace Reach.Silo.Host.Controllers;

[Authorize]
[Route("/api/account")]
public class AccountController : Controller
{
    static Dictionary<Type, MethodInfo> AccountCommandMethods = new();

    static AccountController()
    {
        var methods = typeof(IAccountGrain).GetMethods(BindingFlags.Public | BindingFlags.Instance);

        foreach(var method in methods)
        {
            if (method.ReturnType == typeof(Task<CommandResponse>))
            {
                AccountCommandMethods.Add(method.GetParameters().First().ParameterType, method);
            }
        }
    }
    private readonly IAccountViewReadRepository _accountViewReadRepository;
    private readonly IClusterClient _clusterClient;

    public AccountController(IClusterClient clusterClient, IAccountViewReadRepository accountViewReadRepository)
    {
        _clusterClient = clusterClient;
        _accountViewReadRepository = accountViewReadRepository;
    }

    [HttpGet("settings")]
    public async Task<AccountSettingsView> GetSettings()
    {
        var result = await _accountViewReadRepository.GetSettings(User.GetIdentifierClaim());
        return result ?? new();
    }

    [HttpPost("execute")]
    public async Task<CommandResponse> Execute([FromHeader(Name = "X-Command-Type")] string commandTypeHeader)
    {
        // TODO: We need to move this to a common place, and combine it with OrleansWebApplicationExtensions

        // confirm the type
        var commandType = Type.GetType(commandTypeHeader)!;

        if(!commandType.IsAssignableTo(typeof(BaseAccountCommand)))
        {
            throw new InvalidOperationException("Cannot execute a non-account command against an account.");
        }

        // get access to the grain
        var grain = _clusterClient.GetGrain<IAccountGrain>(User.GetIdentifierClaim());

        string body = "";
        using (StreamReader stream = new StreamReader(Request.Body))
        {
            body = await stream.ReadToEndAsync();
        }

        // deserialize the command
        var command = JsonConvert.DeserializeObject(body, commandType);

        // force the tenant id into the command to avoid chance
        // user submits with tenant header but substitutes another 
        // tenant in the command body
        if (command is BaseAccountCommand accountCommand)
        {
            accountCommand.AccountId = User.GetIdentifierClaim();
        }

        // route the command to the grain
        var method = AccountCommandMethods[commandType];
        var response = (Task<CommandResponse>)method.Invoke(grain, [command])!;

        return await response;
    }
}