@description('The location into which your Azure resources should be deployed.')
param location string = resourceGroup().location

@minLength(2)
@description('Azure Devops Project Name')
param applicationName string = 'UndefinedapplicationName'

@description('Key Vault Containing Secrets.')
param keyVaultName string = ''

@description('Azure AD Instance.')
@minLength(3)
param azureADInstance string
@description('Azure AD Domain.')
@minLength(3)
param azureADDomain string
@description('Azure AD Tenant ID.')
@minLength(3)
param azureADTenantId string
@description('Azure AD Client ID.')
@minLength(3)
param azureADClientId string
@description('Azure AD Client Secret.')
@minLength(3)
param azureADClientSecret string
@description('Azure AD Extension Id.')
@minLength(3)
param azureADExtensionId string
@description('SQL Server Name.')
param sqlServerName string
@description('SQL Server Username.')
param sqlAdminUsername string
@description('SQL Server Password.')
param sqlAdminPassword string  
@description('App Service Plan Name')
param appServicePlanName string
@description('Resource Group')
param resourceGroupName string

@description('Select the type of environment you want to provision. Case Sensitive!')
@allowed([
  'feature'
  'develop'
  'test'
  'prod'
])
param environmentType string
 

// Define the names for resources.
var sqlDatabaseName = 'AccountsDb'
var appServiceName = 'as-${applicationName}-${environmentType}-${location}'
var appInsightsName = 'ai-${applicationName}-${environmentType}-${location}'

var tags = {
  'Owner': 'TOB Consulting'
  'Env': environmentType
  'DR': 'Low'
  'Project': 'TOB Accounts API'
}

var appConfigSettings = [
  {
    name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
    value: appInsights.outputs.connectionString
  }
  {
    name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
    value: '~3'
  }
]

module database './modules/sqlDatabase.bicep' = {
  name: 'dbDeploy'
  params: {
    sqlServerName: sqlServerName
    sqlUsername: sqlAdminUsername
    sqlPassword: sqlAdminPassword
    databaseName: sqlDatabaseName
    environmentType: environmentType
    location: location
    tags: tags
  }
}

module appInsights './modules/appInsights.bicep' = {
  name: 'appInsightsDeploy'
  params: {
    applicationInsightsName: appInsightsName
    location: location
    tags: tags
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' existing = {
  name: appServicePlanName
  scope: resourceGroup(resourceGroupName)
}

module appService './modules/appService.bicep' = {
  name: 'appServiceDeploy'
  params: {
    appServiceName: appServiceName
    serverFarmId: appServicePlan.id
    location: location
    appSettings: appConfigSettings
    tags: tags
  }
}

// Reference to existing Key Vault
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
  scope: resourceGroup(resourceGroupName)
}

// Assign Key Vault Secrets User role to App Service managed identity
resource keyVaultRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(keyVault.id, appService.outputs.id, 'Key Vault Secrets User')
  scope: keyVault
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6') // Key Vault Secrets User
    principalId: appService.outputs.appServicePrincipalId
    principalType: 'ServicePrincipal'
  }
}

module connectionStringSecret './modules/keyVaultSecret.bicep' = {
  name: 'databaseConnectionStringSecretDeploy'
  params: {
    keyVaultName: keyVaultName
    secretName: 'ConnectionStrings--AccountsDBContext'
    secretValue: database.outputs.connectionString
  }
}


