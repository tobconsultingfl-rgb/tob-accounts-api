# TOB Accounts API - Deployment

This folder contains the infrastructure and deployment pipelines for the TOB Accounts API.

## Files

- **main.bicep** - Bicep template for deploying Azure infrastructure
- **main.parameters.example.json** - Example parameters file for the Bicep template
- **infrastructure-pipelines.yml** - Azure Pipeline for deploying infrastructure
- **application-pipelines.yml** - Azure Pipeline for building and deploying the application

## Prerequisites

- Azure subscription
- Existing App Service Plan
- Existing SQL Server
- Existing Key Vault
- Azure DevOps organization with service connections configured

## Infrastructure Components

The Bicep template creates the following resources:

1. **App Service** - Hosts the .NET 9 API application with system-assigned managed identity
2. **SQL Database** - Database on the existing SQL Server
3. **Application Insights** - For monitoring and telemetry
4. **Log Analytics Workspace** - Backing store for Application Insights
5. **Role Assignment** - Grants the App Service managed identity "Key Vault Secrets User" role

## Existing Resources Referenced

The following resources must already exist:

1. **App Service Plan** - Windows or Linux App Service Plan
2. **SQL Server** - Azure SQL Server instance
3. **Key Vault** - Azure Key Vault for secrets storage

## Deployment

### Manual Deployment with Azure CLI

1. Copy and customize the parameters file:
   ```bash
   cp main.parameters.example.json main.parameters.dev.json
   ```

2. Edit `main.parameters.dev.json` with your values

3. Deploy the infrastructure:
   ```bash
   az deployment group create \
     --resource-group rg-tob-accounts-dev \
     --template-file main.bicep \
     --parameters main.parameters.dev.json
   ```

### Azure Pipeline Deployment

#### Infrastructure Pipeline

1. Configure the following variables in Azure DevOps:
   - `azureSubscription` - Name of your Azure service connection
   - `subscriptionId` - Azure subscription ID
   - `appServicePlanName` - Existing App Service Plan name
   - `sqlServerName` - Existing SQL Server name
   - `keyVaultName` - Existing Key Vault name
   - `entraIdTenantId` - Microsoft Entra ID Tenant ID
   - `entraIdClientId` - App Registration Client ID
   - `entraIdAudience` - API Client ID
   - `corsAllowedOrigins` - Semicolon-delimited list of allowed origins
   - `otlpEndpoint` - OpenTelemetry OTLP endpoint

2. Create a new pipeline in Azure DevOps using `infrastructure-pipelines.yml`

3. Run the pipeline and select the target environment (dev/test/prod)

#### Application Pipeline

1. Configure the following variables in Azure DevOps:
   - `azureSubscription` - Name of your Azure service connection
   - `appServiceName` - App Service name (output from infrastructure deployment)
   - `resourceGroupName` - Resource group name

2. Create a new pipeline in Azure DevOps using `application-pipelines.yml`

3. Run the pipeline and select the target environment (dev/test/prod)

## App Service Configuration

The App Service is configured with the following settings:

### App Settings
- **ASPNETCORE_ENVIRONMENT** - Environment name (dev/test/prod)
- **AzureAd__Instance** - Microsoft Entra ID instance URL
- **AzureAd__TenantId** - Tenant ID
- **AzureAd__ClientId** - Client ID
- **AzureAd__Audience** - API audience
- **Cors__AllowedOrigins** - Allowed CORS origins
- **KeyVault__VaultUri** - Key Vault URI
- **OpenTelemetry__*** - OpenTelemetry configuration

### Connection Strings
- **DefaultConnection** - SQL Database connection using Managed Identity authentication

## Security

### Managed Identity

The App Service uses a system-assigned managed identity for:
- Connecting to SQL Database (Azure AD authentication)
- Reading secrets from Key Vault

### Key Vault Access

The managed identity is granted "Key Vault Secrets User" role to read secrets from the Key Vault.

### SQL Database Access

After deployment, grant the App Service managed identity access to the SQL database:

```sql
CREATE USER [app-service-name] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [app-service-name];
ALTER ROLE db_datawriter ADD MEMBER [app-service-name];
ALTER ROLE db_ddladmin ADD MEMBER [app-service-name];
```

## Monitoring

Application Insights is configured for:
- Request tracking
- Dependency tracking
- Exception logging
- Custom metrics and traces

View telemetry in the Azure Portal under Application Insights.

## Deployment Slots (Production Only)

For production deployments, the application pipeline:
1. Deploys to a staging slot
2. Performs smoke tests
3. Swaps staging to production

This enables zero-downtime deployments.

## Database Migrations

Database migrations should be run as part of the deployment pipeline. Update the migration step in `application-pipelines.yml`:

```yaml
- task: AzureCLI@2
  displayName: 'Run Database Migrations'
  inputs:
    azureSubscription: $(azureSubscription)
    scriptType: 'bash'
    scriptLocation: 'inlineScript'
    inlineScript: |
      dotnet ef database update --project src/TOB.Accounts.Infrastructure \
        --startup-project src/TOB.Accounts.API \
        --connection "$(connectionString)"
```

## Troubleshooting

### App Service won't start
- Check Application Insights logs
- Verify all required app settings are configured
- Ensure managed identity has access to Key Vault and SQL Database

### Can't connect to SQL Database
- Verify firewall rules allow Azure services
- Confirm managed identity has been granted SQL permissions
- Check connection string is correct

### Can't read secrets from Key Vault
- Verify managed identity has "Key Vault Secrets User" role
- Ensure Key Vault URI is correct in app settings
- Check Key Vault is not behind a firewall blocking the App Service
