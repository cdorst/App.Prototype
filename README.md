# App.Prototype

This application processes a given DevOps declaration: it creates GitHub repositories, adds AppVeyor->NuGet CI/CD pipelines, authors code & common files like LICENSE and README, and tracks created artifacts with a source-code graph in a repository named [Project.Index](https://github.com/cdorst/Project.Index). Project.Index contains a .dgml file of the repository dependency graph and a listing of each code-generated repository in the project. (Ideally, the entire GitHub account is being managed by the same DevOps declaration project).

This console-app prototype will be used to author a version of this application as an Azure cloud service.

The goal of this project is to provide a high-level API that bots and humans can use to create and manage open-source cloud applications. Ideally, this project is the tool with which self-authoring software authors itself.

## Prerequisites

### Accounts

You'll need Azure, Appveyor, GitHub, and, NuGet accounts.

*NuGet, Appveyor, and GitHub accounts must have the same name (e.g. "CDorst").*

If you have existing accounts and your names do not match this pattern, please create alternate accounts that adhere to this strict convention.

### Azure Resources

To avoid being time-bound by NuGet.org's package indexing process, this code generator maintains a local NuGet package source on the AppVeyor build instance by uploading and downloading .nupkg files to Azure CDN (backed by Azure Blob Storage).

You'll need to create a public Azure Blob Storage instance fronted by an Azure CDN instance named "{{accountName}}-dev" (e.g. "cdorst-dev"). (NOTE: the blob-storage instance name does not matter; the CDN name does matter since it's referenced during the AppVeyor build). Create a container within Azure Storage called "nuget". Take your NuGet icon image (or make one if you don't have one - it's just a square 64x64 pixel .png image), name it "package-icon-64.png" and add it to your "nuget" blob container. When done correctly, you'll be able to download your image from https://{{accountName}}-dev.azureedge.net/nuget/package-icon-64.png

Copy your Azure Storage connection string from the portal and goto AppVeyor's website and use their [Encypt Configuration Data](https://ci.appveyor.com/tools/encrypt) tool to encrypt your connection string. Use the value of AppVeyor's encrypted version of your Azure Storage connection string when setting the value of `ConsoleApp6.Declarations.Project.AppveyorAzureStorageSecret`. This AppVeyor-encrypted string is included in each repository's appveyor.yml build-definition.

### AppVeyor Environments

Each NuGet-package repository's AppVeyor build will produce .nupkg artifacts that need to be deployed to your NuGet account. To achieve this, setup an AppVeyor "Environment" (of type: NuGet) called "{{AccountName}}NuGet" (e.g. "CDorstNuGet") and include your API token.

## Environment Variables

This project requires that the environment variables below are set in the execution environment. For assistance with setting environment varibles, see this Stack Exchange answer: [https://superuser.com/a/284351](https://superuser.com/a/284351)

Name | Description 
---- | -----------
`APPVEYOR_API_TOKEN` | This is the AppVeyor API token required to add and start project builds. Provision a new token here: [https://ci.appveyor.com/api-token](https://ci.appveyor.com/api-token)
`GITHUB_PERSONAL_ACCESS_TOKEN` | This is the GitHub "Personal Access Token" API token required to create repositories and push commits. Provision a new token here: [https://github.com/settings/tokens](https://github.com/settings/tokens)

## Support

Support this work by collaborating through GitHub and by [donating to cdorst on Patreon](https://www.patreon.com/user?u=9178360).


