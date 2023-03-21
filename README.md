# Spine Datasheet API
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/5391ed7be72e48a891f4431ccb9d7ad0)](https://app.codacy.com/gh/equinor/spinedatasheetapi/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
![Known Vulnerabilities](https://snyk.io/test/github/equinor/spinedatasheetapi/badge.svg)
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url] [![Stargazers][stars-shield]][stars-url] [![Issues][issues-shield]][issues-url]

## Spine Datasheet

- Product owner: 
- Business area: 

## Summary Description

Lorem ipsum dolor sit amet. Qui consequuntur repudiandae et tempore modi ut cumque Quis id veniam sint ea perspiciatis quasi et Quis voluptatem qui architecto explicabo. Ex fugiat quia et quam dolores et minima omnis a illo dolor. Non assumenda saepe sed quia tempore qui dolorem molestias et laudantium praesentium id molestiae ipsa. Eum quam eaque in corrupti delectus ut adipisci distinctio 33 omnis accusamus.

### Prerequisites

- [.NET 6.0+](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node 16+ with npm 8+](https://github.com/nodesource/distributions/blob/master/README.md)
- [Docker](https://docs.docker.com/engine/install/)

## Architechture

The application is split between the [frontend app](#frontend) hosted in Fusion, and the [backend app](#backend) hosted in Radix. Authentication is based on [RBAC](https://learn.microsoft.com/en-us/azure/role-based-access-control/overview), where we have different app registrations for preproduction and production with are consented to access Fusion Preprod or Fusion Prod. 



### Security

Snyk surveillance has been added to the project for continuous monitoring of the code and its dependency. 

### Azure App Config

Azure App Configuration provides a service to centrally manage application settings and feature flags. It allows us to change configuration directly in Azure for all environments. Combined with Azure Key Vault it also combines a secure place to store secrets and connection strings.

### Omnia Radix

[Omnia Radix](https://console.radix.equinor.com/applications/datasheet) is a Equinor PaaS (Platform as a Service) based on AKS to build and run docker containers. You can either make Radix build your container directly, or pull the container from a container registry. For Spine Datasheet API the image is built in [Github Actions](#githubactions), and pushed to [Azure Container Registry](#azure-container-registry). Radix pulls the image corresponding to release stage.

Configuration of the required infrastructure is placed in a radixconfig.yml, which defines the different components and environments which are created. Runtime variables and secrets are also defined in radixconfig.yml. The DCD config is placed in a separate [git repo](https://github.com/equinor/dcd-radix-conf).

## API

The backend is dotnet webapi built with .NET 6 which provides a REST interface for the frontend. Swagger has been installed to provide documentation for the API, and to test functions. The backend retrieves and stores data in a Azure SQL Database for each environment. 

### Run backend

Create a file `backend/api/Properties/launchSettings.json` with the provided
template file. You need to populate the app configuration connection string
(navigate to azure portal, find app configuration resource, navigate to
settings -> access keys), and choose an AppConfiguration Environment (`dev` for
local development at time of writing).

Finally, to be able to use secrets referenced in the app config, you need to
authenticate yourself on the command line. [Get a hold of the azure CLI
`az`](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) and run `az login` in the command line. NB: You will need to use a browser for the
authentication, as far as I know.

Then, to start the backend, you can run

```cmd
cd backend/api
dotnet run
```

## Deployment

We have 3 different environments in use; dev, qa and prod. Dev is built
when pull requests are merged to main. 

## Development

### Team
Spine Datasheet API is developed by the It's a Feature team in TDI EDT DSD EDB. 

### Repository


### Build and Release

## Architecture Diagrams

The following diagrams have been created using PlantUML.

### System Context Diagram

System context diagram for the DCD application.
![SysContextDiagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/equinor/dcd///main/PlantUMLC4L1)

### Container Diagram

Container diagram for the DCD application.
![SysContextDiagram](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/equinor/dcd///main/DCD_C4Container.iuml)

## Access to application and data - UNDER CONSTRUCTION

AD groups that can view data (AccessIT groups work in progress)
|Name|Description|User types|How to check|
|-|-|-|-|
|Project Users| Read/write access to app | Employees, external hire, consultants | [ConceptApp Users](https://portal.azure.com/#view/Microsoft_AAD_IAM/GroupDetailsMenuBlade/~/Overview/groupId/cd75d09b-5f90-4fac-be54-de4af8b5b279), [fg_2S_IAF](https://portal.azure.com/#view/Microsoft_AAD_IAM/GroupDetailsMenuBlade/~/Overview/groupId/a64069dd-12fd-422b-8c1e-2093fa32819d), [fg_PRD EP CD VALU](https://portal.azure.com/#view/Microsoft_AAD_IAM/GroupDetailsMenuBlade/~/Overview/groupId/553eada8-9205-4c81-bd32-488ebc5dc349) |
|Read Only User| Only able to read all information in app | Employees, external hire, consultants | Currently no groups |
|Admin| Set/change specific settings in app | Employees, external hire, consultants | [ConceptApp Admins](https://portal.azure.com/#view/Microsoft_AAD_IAM/GroupDetailsMenuBlade/~/Overview/groupId/196697db-1a55-4e46-8581-7f2463016e8f), [fg_2S_IAF](https://portal.azure.com/#view/Microsoft_AAD_IAM/GroupDetailsMenuBlade/~/Overview/groupId/a64069dd-12fd-422b-8c1e-2093fa32819d) |

### Admin Consent Decision Matrix
|Privilege requested|In-house developed applications|Scope|
|-|-|-|
|Application API permissions (App to App).|Application: API Owner: Team IAF, Data Owner: Atle Svandal|Sites.Read.All, user_impersonation|



## notes and links
[Abbreviation examples for Azure resources](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations) 
[Conventional Commits](https://www.conventionalcommits.org/)
[Semantic Versioning](https://semver.org/)



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/equinor/spinedatasheetapi.svg?style=for-the-badge
[contributors-url]: https://github.com/equinor/spinedatasheetapi/graphs/contributors
[actions-shield]: https://github.com/equinor/spinedatasheetapi/actions/workflows/ci.yml/badge.svg
[forks-shield]: https://img.shields.io/github/forks/equinor/spinedatasheetapi.svg?style=for-the-badge
[forks-url]: https://github.com/equinor/spinedatasheet/network/members
[stars-shield]: https://img.shields.io/github/stars/equinor/spinedatasheetapi.svg?style=for-the-badge
[stars-url]: https://github.com/equinor/spinedatasheetapi/stargazers
[issues-shield]: https://img.shields.io/github/issues/equinor/spinedatasheetapi.svg?style=for-the-badge
[issues-url]: https://github.com/equinor/spinedatasheetapi/issues
[license-shield]: https://img.shields.io/github/license/equinor/spinedatasheetapi.svg?style=for-the-badge
[license-url]: https://github.com/equinor/spinedatasheetapi/blob/master/LICENSE.txt
[product-screenshot]: images/screenshot.png