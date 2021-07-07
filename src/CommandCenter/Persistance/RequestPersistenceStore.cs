// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Persistance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandCenter.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Class to manage the persistence of Request to and from an Azure CosmosDB instance.
    /// </summary>
    public class RequestPersistenceStore : IRequestPersistenceStore
    {
        private readonly string cosmosDBName;
        private readonly string cosmosRequestsContainerName;
        private readonly string cosmosDBEndpoint;
        private readonly string cosmosPrimaryKey;
        private readonly CosmosClient cosmosClient;
        private Database cosmosDatabase;
        private Container cosmosRequestContainer;
        private ILogger<RequestPersistenceStore> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPersistenceStore"/> class.
        /// </summary>
        /// <param name="storeOptions">The store options.</param>
        /// <param name="logger">The logger instance.</param>
        public RequestPersistenceStore(IOptionsMonitor<PersistenceStoreOptions> storeOptions, ILogger<RequestPersistenceStore> logger)
        {
            this.cosmosDBEndpoint = storeOptions.CurrentValue.Endpoint;
            this.cosmosPrimaryKey = storeOptions.CurrentValue.PrimaryKey;
            this.cosmosDBName = storeOptions.CurrentValue.DatabaseName;
            this.cosmosRequestsContainerName = storeOptions.CurrentValue.ContainerName;
            this.logger = logger;

            this.cosmosClient = new CosmosClient(this.cosmosDBEndpoint, this.cosmosPrimaryKey);
        }

        /// <inheritdoc/>
        public async Task<bool> InsertRequestAsync(AzureSubscriptionProvisionModel modelToInsert)
        {
            // Return False if we cannot connect to the Cosmos instance.
            if (!await this.CreateResourcesAsync())
            {
                return false;
            }

            // Model to Insert does not contain a valid subscription Id
            if (modelToInsert.SubscriptionId == Guid.Empty)
            {
                return false;
            }

            // Return True if an instance has already been persisted for this subscription.
            if (await this.GetRequestBySubscriptionIdAsync(modelToInsert.SubscriptionId) != null)
            {
                return true;
            }

            try
            {
                await this.cosmosRequestContainer.CreateItemAsync(modelToInsert);
            }
            catch (CosmosException ce)
            {
                this.logger.LogError($"Error Creating Request Document.  Subscription ID: {modelToInsert.SubscriptionId}. Message: {ce.Message}");
                return false;
            }

            return false;
        }

#nullable enable
        /// <inheritdoc/>
        public async Task<AzureSubscriptionProvisionModel?> GetRequestBySubscriptionIdAsync(Guid subscriptionId)
        {
            if (!await this.CreateResourcesAsync())
            {
                this.logger.LogError("Could not Retrieve existing requests.");
                return null;
            }

            try
            {
                using (var linqIterator =
                    this.cosmosRequestContainer.GetItemLinqQueryable<AzureSubscriptionProvisionModel>()
                        .Where(i => i.SubscriptionId == subscriptionId)
                        .ToFeedIterator<AzureSubscriptionProvisionModel>())
                {
                    if (linqIterator.HasMoreResults)
                    {
                        var response = await linqIterator.ReadNextAsync();
                        return response.FirstOrDefault();
                    }
                }
            }
            catch (CosmosException ce)
            {
                this.logger.LogError($"Error retrieving Request Documents. Message: {ce.Message}");
            }

            return null;
        }
#nullable restore

        private async Task<bool> CreateResourcesAsync()
        {
            if (this.cosmosDatabase == null)
            {
                try
                {
                    this.cosmosDatabase = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(this.cosmosDBName);
                }
                catch (CosmosException ce)
                {
                    this.logger.LogError($"Error Creating or opening Cosmos Database Instance.  Name: {this.cosmosDBName}. Message: {ce.Message}");
                    return false;
                }
            }

            if (this.cosmosClient == null)
            {
                try
                {
                    this.cosmosRequestContainer = await this.cosmosDatabase.CreateContainerIfNotExistsAsync(this.cosmosRequestsContainerName, "/SubscriptionId");
                }
                catch (CosmosException ce)
                {
                    this.logger.LogError($"Error Creating or opening Cosmos Container Instance.  Name: {this.cosmosRequestsContainerName}. Message: {ce.Message}");
                    return false;
                }
            }

            return true;
        }
    }
}
