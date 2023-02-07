//var tenantId = subscription().tenantId
var location = resourceGroup().location
var baseAppName = 'datasheet'

module appInsights 'appi/appi.bicep' = {
  name: 'appInsight'
  params: {
    baseAppName: baseAppName
    location: location

  }
}


module appcs 'appcs/appcs.bicep' = {
  name: 'appcs'
  params: {
    baseAppName: baseAppName
    location: location
  }
}


module acr 'acr/acr.bicep' = {
  name: 'cr'
  params: {
    baseAppName: baseAppName
    location: location
  }
}

module kv 'kv/kv.bicep' = {
  name: 'kv'
  params: {
    baseAppName: baseAppName
    location: location
    accessPolicies: [
      {
        applicationId: 'string'
        objectId: 'string'
        permissions: {
          certificates: ['string']
          keys: ['string']
          secrets: ['string']
          storage: ['string']
        }
        tenantId: 'string'
      }
    ]
  }
}
