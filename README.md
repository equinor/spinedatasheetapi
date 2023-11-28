# Spine Datasheet API
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/5391ed7be72e48a891f4431ccb9d7ad0)](https://app.codacy.com/gh/equinor/spinedatasheetapi/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
![Known Vulnerabilities](https://snyk.io/test/github/equinor/spinedatasheetapi/badge.svg)
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url] [![Stargazers][stars-shield]][stars-url] [![Issues][issues-shield]][issues-url]

## Spine Datasheet

- Product owner: [Olaf Grødem](mailto:ogro@equinor.com)
- Business area: 

## Summary Description

Datasheet is a Fusion application that lets the user view datasheets and review tag data from Spine.

## [Runbook](https://github.com/equinor/spinedatasheetapi/blob/main/documentation/runbook.md)

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

## Development

### Team
Spine Datasheet API is developed by the It's a Feature team in TDI EDT DSD EDB. 

### Repository


### Build and Release

## Architecture Diagrams

The following diagrams have been created using PlantUML.

### System Context Diagram

System context diagram for the Spine Datasheet application.


### Container Diagram

Container diagram for the Spine Datasheet application.


## Access to application and data - UNDER CONSTRUCTION

AD groups that can view data (AccessIT groups work in progress)
|Name|Description|User types|How to check|
|-|-|-|-|

### Admin Consent Decision Matrix



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