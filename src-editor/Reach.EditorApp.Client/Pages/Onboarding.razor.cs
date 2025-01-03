﻿using Microsoft.AspNetCore.Components;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.ServiceModel;
using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Client.Pages;

public partial class Onboarding : ComponentBase
{
    private readonly IOrganizationService _organizationService;
    private readonly IRegionService _regionService;
    private readonly NavigationManager _navigation;
    private readonly IRegionUrlFormatter _regionUrlFormatter;

    private IEnumerable<Region> _regions = [];

    private bool _isProcessing = false;
    private OnboardingModel _model = new();
    private string? _errorMessage = null;

    public Onboarding(
        IOrganizationService organizationService,
        IRegionService regionService,
        NavigationManager navigationManager,
        IRegionUrlFormatter regionUrlFormatter)
    {
        _organizationService = organizationService;
        _regionService = regionService;
        _navigation = navigationManager;
        _regionUrlFormatter = regionUrlFormatter;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _regions = await _regionService.GetAllRegionsAsync();
            StateHasChanged();
        }
    }

    private async Task BeginCreate()
    {
        _errorMessage = null;
        _isProcessing = true;
        StateHasChanged();

        var result = await _organizationService.CreateOrganization(
            _model.OrganizationId,
            _model.OrganizationName,
            _model.OrganizationSlug,
            "DUMMY"
        );

        if (result.IsSuccess)
        {
            var hubResult = await _organizationService.CreateHub(
                _model.HubId,
                _model.OrganizationId,
                _model.HubName,
                _model.HubSlug,
                _model.HubIconUrl,
                _model.HubRegionKey
            );

            // TODO: Print out a warning if we created the organization but not the hub
            _navigation.NavigateTo("/");
        }
        else
        {
            _errorMessage = "Could not create the organization!";
        }

        _isProcessing = false;
        StateHasChanged();
    }

    private class OnboardingModel
    {
        public Guid OrganizationId { get; set; } = Guid.NewGuid();

        public string OrganizationName { get; set; } = string.Empty;

        public string OrganizationSlug { get; set; } = string.Empty;

        public Guid HubId { get; set; } = Guid.NewGuid();

        public string HubName { get; set; } = string.Empty;

        public string HubSlug { get; set; } = string.Empty;

        public string HubIconUrl => $"https://picsum.photos/id/123/200";

        public string HubRegionKey { get; set; } = string.Empty;
    }
}
