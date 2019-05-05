# CSV Importer

The purpose of this project is being able to import a csv file.

## Pre-requisites

- [DotNet Core 2.2](https://dotnet.microsoft.com/download)
- [Azure](https://azure.microsoft.com/)

  If you don't have an Azure subscription, create a free account before you begin. We are going to use:

  - ApplicationInsights
  - ServiceBus
  - BlobStorage
  - KeyVault

* [Liquibase](http://www.liquibase.org/) (migrations)
  - Dependency on Java

## Getting started

### Configuring services in Azure

1. Add a new instance of ApplicationInsights
2. Create a new SQL Database
3. Add a new Storage account for storing Blobs.
4. Create a new namespace in ServiceBus
   - The tier needs to be Standard or Premium for being able to create topics.
5. KeyVault:
   - Add a new key vault for this project.
   - Create following secrets:
     - SqlConnectionString
     - ServiceBusConnectionString
     - CloudStorageConnectionString
     - AppInsightsInstrumentationKey
