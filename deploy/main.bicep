@description('The environment name (e.g., dev, test, prod)')
param environmentName string

@description('The location for all resources')
param location string = resourceGroup().location

@description('The name of the existing App Service Plan')
param appServicePlanName string

@description('The resource group of the existing App Service Plan')
param appServicePlanResourceGroup string = resourceGroup().name

@description('The name of the existing SQL Server')
param sqlServerName string

@description('The resource group of the existing SQL Server')
param sqlServerResourceGroup string = resourceGroup().name

@description('The name of the existing Key Vault')
param keyVaultName string

@description('The resource group of the existing Key Vault')
param keyVaultResourceGroup string = resourceGroup().name

@description('The name of the application (used for resource naming)')
param applicationName string = 'tob-accounts-api'

@description('The Entra ID Tenant ID')
param entraIdTenantId string

@description('The Entra ID Client ID (App Registration)')
param entraIdClientId string

@description('The Entra ID Audience (API Client ID)')
param entraIdAudience string

@description('CORS allowed origins (semicolon-delimited)')
param corsAllowedOrigins string

@description('OpenTelemetry OTLP Endpoint')
param otlpEndpoint string = 'http://localhost:4317'

@description('Enable OpenTelemetry tracing')
param enableTracing bool = true

@description('Enable OpenTelemetry metrics')
param enableMetrics bool = true

@description('Enable OpenTelemetry logging')
param enableLogging bool = true

// Variables
var uniqueSuffix = uniqueString(resourceGroup().id)
var appServiceName = '${applicationName}-${environmentName}-${uniqueSuffix}'
var sqlDatabaseName = '${applicationName}-db-${environmentName}'
var appInsightsName = '${applicationName}-ai-${environmentName}-${uniqueSuffix}'
var logAnalyticsName = '${applicationName}-la-${environmentName}-${uniqueSuffix}'

// Reference to existing App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' existing = {
  name: appServicePlanName
  scope: resourceGroup(appServicePlanResourceGroup)
}

// Reference to existing SQL Server
resource sqlServer 'Microsoft.Sql/servers@2023-08-01-preview' existing = {
  name: sqlServerName
  scope: resourceGroup(sqlServerResourceGroup)
}

// Reference to existing Key Vault
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
  scope: resourceGroup(keyVaultResourceGroup)
}

// Log Analytics Workspace for Application Insights
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

// Application Insights
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalytics.id
    IngestionMode: 'LogAnalytics'
  }
}

// SQL Database
resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  parent: sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
    capacity: 5
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 2147483648 // 2 GB
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
  }
}

// App Service (Web App)
resource appService 'Microsoft.Web/sites@2023-12-01' = {
  name: appServiceName
  location: location
  kind: 'app'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v9.0'
      metadata: [
        {
          name: 'CURRENT_STACK'
          value: 'dotnet'
        }
      ]
      alwaysOn: true
      ftpsState: 'Disabled'
      minTlsVersion: '1.2'
      http20Enabled: true
      appSettings: [
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: environmentName
        }
        {
          name: 'AzureAd__Instance'
          value: 'https://login.microsoftonline.com/'
        }
        {
          name: 'AzureAd__TenantId'
          value: entraIdTenantId
        }
        {
          name: 'AzureAd__ClientId'
          value: entraIdClientId
        }
        {
          name: 'AzureAd__Audience'
          value: entraIdAudience
        }

        {
          name: 'Cors__AllowCredentials'
          value: 'true'
        }
        {
          name: 'Cors__AllowedMethods'
          value: 'GET;POST;PUT;DELETE;PATCH;OPTIONS'
        }
        {
          name: 'Cors__AllowedHeaders'
          value: 'Content-Type;Authorization;X-Requested-With'
        }
        {
          name: 'Cors__MaxAge'
          value: '600'
        }
        {
          name: 'KeyVault__VaultUri'
          value: keyVault.properties.vaultUri
        }
        {
          name: 'OpenTelemetry__UseConsoleExporter'
          value: 'false'
        }
        {
          name: 'OpenTelemetry__OtlpEndpoint'
          value: otlpEndpoint
        }
        {
          name: 'OpenTelemetry__ServiceName'
          value: 'TOB.Accounts.API'
        }
        {
          name: 'OpenTelemetry__ServiceVersion'
          value: '1.0.0'
        }
        {
          name: 'OpenTelemetry__EnableTracing'
          value: string(enableTracing)
        }
        {
          name: 'OpenTelemetry__EnableMetrics'
          value: string(enableMetrics)
        }
        {
          name: 'OpenTelemetry__EnableLogging'
          value: string(enableLogging)
        }
      ]
      connectionStrings: [
        {
          name: 'AccountDbConnection'
          connectionString: 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${sqlDatabaseName};Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;'
          type: 'SQLAzure'
        }
      ]
    }
  }
}

// Assign Key Vault Secrets User role to App Service managed identity
resource keyVaultRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(keyVault.id, appService.id, 'Key Vault Secrets User')
  scope: keyVault
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6') // Key Vault Secrets User
    principalId: appService.identity.principalId
    principalType: 'ServicePrincipal'
  }
}

// Outputs
output appServiceName string = appService.name
output appServiceUrl string = 'https://${appService.properties.defaultHostName}'
output appServicePrincipalId string = appService.identity.principalId
output sqlServerName string = sqlServer.name
output sqlDatabaseName string = sqlDatabase.name
output keyVaultName string = keyVault.name
output keyVaultUri string = keyVault.properties.vaultUri
output appInsightsName string = appInsights.name
output appInsightsConnectionString string = appInsights.properties.ConnectionString
