# Reach CMS

Reach is an open source, headless, composable content platform built on .NET/Blazor, Microsoft Orleans, and Tailwind.

## Applets

Reach provides the basic building blocks for creating and managing content through a series of out-of-the-box structures. These data sets are managed through a series of Applets, which provide plug-and-play features to the core platform.

### Built-in Applets

The following Applets are included as part of the core platform

* **Content** - The Content Applet supports editing of Pages, Content, Components, Fields and Field Types, providing the basic functionality of creating and maintaining content on the platform.
* **Pipelines** - With Pipelines, users are able to define state machines and workflows for processing data. Whether it is defining how data should be reviewed and published for retrieval by the frontend or ingestion and ETL type workflows, Pipelines supports it all.
* **Endpoints** - Endpoints define how and by whome data is retrieved both internally and externally. In conjunction with Content and Pipelines, Endpoints provides a surface for supporting unique business scenarios.

### Developing Applets

#### The Applet Architecture

![Applet Architecture](./doc/Applet%20Architecture.png)

#### Create Your Applet Definition

#### Create Your Applet Components

## Architecture

### The Content Model

![Content Model](./doc/Content%20Model.png)

## Authentication / Authorization