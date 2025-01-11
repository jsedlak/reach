using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Membership.Views;
using Reach.Security;
using Reach.Silo.Membership.ServiceModel;

namespace Reach.Silo.Host.Controllers;

[Authorize]
[Route("/api/account")]
public class AccountController : Controller
{
    private readonly IAccountViewReadRepository _accountViewReadRepository;

    public AccountController(IAccountViewReadRepository accountViewReadRepository)
    {
        _accountViewReadRepository = accountViewReadRepository;
    }

    [HttpGet("settings")]
    public Task<AccountSettingsView> GetSettings()
    {
        return _accountViewReadRepository.GetSettings(User.GetIdentifierClaim());
    }
}