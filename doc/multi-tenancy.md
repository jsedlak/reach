# Multi-Tenancy

> ### **IMPORTANT** 
> This documentation is created as a method of documenting the intended functionality of Reach. It is subject to change as the platform is developed. Please feel free to contribute to the discussion via Issues or Discussions.

Multi-tenancy is integral to Reach. Whether you are self-hosting a Reach instance to support multiple sites, or running a managed provider hosting thousands of customers, tenancy is the key.

Tenants are the main partitioning factor in Reach, by which data is segregated. Tenants live within a single Orleans Cluster (region). While users may have access to multiple Tenants, they may only interact with a single Tenant at a time. 

Tenants are also the default system of billing. Each tenant may (optionally) be attributed to licensing details, specifying limits on resource counts or usage.

# How Tenants Work

Reach functionality is split into two main areas: Global Functionality and Tenant Functionality. This is specified in the URL, though custom hosted solutions may change this.

```
Global Functionality
https://reachcms.io/*

Tenant Functionality
https://reachcms.io/{TENANT}/*
https://{REGION}.reachcms.io/{TENANT}/*
```

When navigating to any Tenant Functionality, an authorization check occurs, whereby the user is validated against the requested tenant. If successful, the page is loaded and interaction proceeds normally. If unsuccessful, one of two things happens. If the user has no tenants and has not decided to skip provisioning a tenant, they are redirected to the onboarding process where a new Tenant may be provisioned. If the user does has tenants or has opted to skip onboarding, they are redirected to the **Global Dashboard**.

If **Region** based deployments are supported, the Tenant is hosted only within a Region. **Note** that the Tenant's slug (identifier) must be unique across the entire platform deployment, not just within a single region. While navigating within a particular region, the API endpoint for that region is used (e.g. `https://west-us.reachcms.io/api/*`).

# Tenant Subscriptions

For more information about configuring Subscriptions, please see the [Billing](./billing.md) documentation.

By default, Tenant Subscriptions are disabled and an Unlimited License is granted to all provisioned Tenants. This allows self-hosting to be simpler to setup and run.

By configuring Subscriptions, Tenants may be provisioned with specific licensing details that outline limits and overages on API usage, storage, and resource counts. 

# Configuring Tenancy

TBD.

# Configuring Backup Region Tenancy

Optionally, secondary regions may be used as a replication of the primary tenant for read-only options. 

Details are TBD.