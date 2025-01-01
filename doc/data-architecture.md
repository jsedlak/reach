# Data Architecture

This document describes the basic hierarchy of data being represented by the Reach.

## Content

The following structures makeup the core data of the platform, representing the structures necessary for designing and managing content.

### Editor Definition

An *Editor Definition* is a basic building block of the Content Platform and creates a link between the ability to display a type of an editor and its underlying primitive.

For instance, if the underlying Blazor primitive is a component, say `Reach.Components.Editors.TextBox`, responsible for rendering a text box and reporting changes back to the system. It may have some parameters such as whether or not a value is required, if there is a maximum length, or if there is a particular required format.

An Editor Definition, `TextBox`, represents an in-platform link to that primitive, exposing the parameters as appropiate.

In general, there will be a fixed set of *Editor Definition* items within the platform, generally defined by the number of underlying primitive components.

### Field Definition

Whereas an Editor Definition is a link between a primitive and the platform, a *Field Definition* is a reusable application of that primitive's representation.

For instance, if we consider the Editor Definition, `TextBox`, then we may have Field Definitions such as `Text Field`, `Number Field`, `Email Field`, and so on. These Field Definitions utilize the parameters of the Editor Definition to shape how the control behaves into a particular way.

Much like the Editor Definition before it, there will be an upper bound on how many Field Definitions can exist, limited by the number of parameters and the usefulness of their various combinations.

### Field

Unlike the previous two structures, a Field is not represented by a Grain in the Reach platform. It is understood as an instance of a Field Definition, an application of the schema within another structure.

It's use is very specific and links the underlying primitive and platform editor to a piece of content/data being managed by the platform. For example, a Page Component Definition may declare a Field of type `TextBox`, with name "Page Title" and the Required attribute set to true.

Fields may exist on Component Definitions and Components.

### Component Definitions

Component Definitions create a schema of Fields (and their Field Definitions and Editor Definitions) that, as a group, create a representation of a known type of content.

For instance, a Page Component Definition would include fields like Page Title, Meta Description, Child Components, and more, defining properties that are relied upon to render a page.

As another example, a Hero Component Definition would include fields such as Background Image, Title, Subtitle, and Call To Action (Link).

In either case, the Component Definition declares a reusable structure that may be married with data (as a Component) to build content for a site or application.

### Component

A Component is how content is ultimately represented in Reach and is the struture with which Content Editors will spend most of their time.

Whereas a Component Definition defines the schema, such as Page or Hero, a Component is the realization of that schema, such as Home Page or Home Page Hero.