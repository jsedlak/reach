//using Microsoft.AspNetCore.Components.Authorization;
//using Reach.Cqrs;
//using Reach.EditorApp.ServiceModel;
//using Reach.Membership.Model;
//using Reach.Membership.ServiceModel;
//using Reach.Membership.Views;
//using Reach.Orchestration.ServiceModel;

//namespace Reach.EditorApp.Services;

//public class RepositoryTenantService : ITenantService
//{
//    private readonly ITenantRepository _tenantRepository;
//    private readonly IRegionProvider _regionProvider;
//    private readonly AuthenticationStateProvider _authenticationStateProvider;

//    public RepositoryTenantService(
//        AuthenticationStateProvider authenticationStateProvider,
//        ITenantRepository tenantRepository, IRegionProvider regionProvider)
//    {
//        _authenticationStateProvider = authenticationStateProvider;
//        _tenantRepository = tenantRepository;
//        _regionProvider = regionProvider;
//    }

//    public async Task<CommandResponse> CreateAsync(Tenant tenant)
//    {
//        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

//        if(state is null || state.User.Identity is null || state.User.Identity.Name is null)
//        {
//            return new CommandResponse();
//        }

//        tenant.OwnerIdentifier = state.User.Identity.Name;
//        var result = await _tenantRepository.Create(tenant);

//        return result;
//    }

//    public async Task<IEnumerable<AvailableTenantView>> GetTenantsForUserAsync()
//    {
//        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

//        if (state is null || state.User.Identity is null || state.User.Identity.Name is null)
//        {
//            return [];
//        }

//        var userId = state.User.Identity.Name;
//        var result = await _tenantRepository.GetAllForUser(userId);

//        var regions = await _regionProvider.GetAll();

//        return result.Select(m => new AvailableTenantView
//        {
//            TenantId = m.Id,
//            Name = m.Name,
//            Slug = m.Slug,
//            Region = regions.First(region => region.Key == m.RegionId),
//            IconUrl = $"https://picsum.photos/seed/{m.Id}/200"
//        });
//    }
//}
