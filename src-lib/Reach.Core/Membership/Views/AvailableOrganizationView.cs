﻿using Reach.GraphQl;

namespace Reach.Membership.Views;

[GraphQueryName("organizations")]
public class AvailableOrganizationView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public AvailableHubView[] Hubs { get; set; } = Array.Empty<AvailableHubView>();
}