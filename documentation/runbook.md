# Runbook for Spine Datasheet

### Prerequisites

- [.NET 6.0+](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node 16+ with npm 8+](https://github.com/nodesource/distributions/blob/master/README.md)
- [Docker](https://docs.docker.com/engine/install/)

### Run backend

Run backend from [spinedatasheetapi](https://github.com/equinor/spinedatasheetapi) repo.

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
cd datasheetapi
dotnet run
```

### Run frontend

Run frontend from [spinedatasheetweb](https://github.com/equinor/spinedatasheetweb) repo.

```cmd
yarn install
yarn start
```

## Deployment

We have 3 different environments in use; dev, qa and prod. Dev is built
when pull requests are merged to main. 