targetScope = 'resourceGroup'

@description('')
param location string = resourceGroup().location

@description('')
param sku string = 'Standard'

@description('')
param principalId string

@description('')
param principalType string


resource serviceBusNamespace_IVo9PrFFR 'Microsoft.ServiceBus/namespaces@2021-11-01' = {
  name: toLower(take('bus${uniqueString(resourceGroup().id)}', 24))
  location: location
  tags: {
    'aspire-resource-name': 'bus'
  }
  sku: {
    name: sku
  }
  properties: {
  }
}

resource roleAssignment_ItVjsKV3u 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: serviceBusNamespace_IVo9PrFFR
  name: guid(serviceBusNamespace_IVo9PrFFR.id, principalId, subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '090c5cfd-751d-490a-894a-3ce6f1109419'))
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '090c5cfd-751d-490a-894a-3ce6f1109419')
    principalId: principalId
    principalType: principalType
  }
}

resource serviceBusQueue_WjpLsq5ey 'Microsoft.ServiceBus/namespaces/queues@2021-11-01' = {
  parent: serviceBusNamespace_IVo9PrFFR
  name: 'myqueue'
  location: location
  properties: {
  }
}

output serviceBusEndpoint string = serviceBusNamespace_IVo9PrFFR.properties.serviceBusEndpoint
