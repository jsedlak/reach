# Multi-Tenancy

> ### **IMPORTANT**
>
> This documentation is created as a method of documenting the intended functionality of Reach. It is subject to change as the platform is developed. Please feel free to contribute to the discussion via Issues or Discussions.

Multi-tenancy is integral to Reach. Whether you are self-hosting a Reach instance to support multiple sites, or running a managed provider hosting thousands of customers, tenancy is the key.

Organizations are the main partitioning factor in Reach, by which data is segregated. While users may have access to multiple Organizations, they may only interact with a single Organization at a time.

Organizations are split into Hubs, which provide an additional mechanism of data partitioning. Unlike Organizations, it is intended that data can and will be shared, copied, or moved between Hubs.

Organizations are also the default system of billing. Each Organization may (optionally) be attributed to licensing details, specifying limits on resource counts or usage.

# How Organizations Work

Reach functionality is split into two main areas: Global Functionality and Tenant Functionality. This is specified in the URL, though custom hosted solutions may change this.

```
Global Functionality
https://reachcms.io/*

Organization Functionality
https://reachcms.io/{organizationSlug}/*
https://reachcms.io/{organizationSlug}/{hubSlug}/*
```

When navigating to any Organization Functionality, an authorization check occurs, whereby the user is validated against the requested tenant. If successful, the page is loaded and interaction proceeds normally.

If unsuccessful, one of two things happens. If the user has no tenants and has not decided to skip provisioning a tenant, they are redirected to the onboarding process where a new Tenant may be provisioned. If the user does has tenants or has opted to skip onboarding, they are redirected to the **Global Dashboard**.

# Organization Subscriptions

For more information about configuring Subscriptions, please see the [Billing](./billing.md) documentation.

By default, Organization Subscriptions are disabled and an Unlimited License is granted to all provisioned Tenants. This allows self-hosting to be simpler to setup and run.

By configuring Subscriptions, Tenants may be provisioned with specific licensing details that outline limits and overages on API usage, storage, and resource counts.

# Configuring Tenancy

TBD.

# Configuring Backup Region Tenancy

Optionally, secondary regions may be used as a replication of the primary tenant for read-only options.

Details are TBD.
