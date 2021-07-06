// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Persistance
{
    using CommandCenter.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to manage the persistence of Request to and from an Azure CosmosDB instance.
    /// </summary>
    public class RequestPersistenceStore : IRequestPersistenceStore
    {
        private const string CosmosDBName = "Faction_Marketplace";
        private const string CosmosRequestsContainerName = "Requests";

        private readonly string cosmosDBConnectionString;
        private readonly CosmosClient cosmosClient;
        private Database cosmosDatabase;
        private Container cosmosRequestContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestPersistenceStore"/> class.
        /// </summary>
        /// <param name="cosmosDBConnectionString">A string instance containing the Connection string for the Cosmos DB instance to be used.</param>
        public RequestPersistenceStore(string cosmosDBConnectionString)
        {
            this.cosmosDBConnectionString = cosmosDBConnectionString;
            this.cosmosClient = new CosmosClient(this.cosmosDBConnectionString);
        }

        /// <summary>
        /// Inserts a new Subscription Request into Cosmos DB.
        /// </summary>
        /// <param name="modelToInsert">The instance of the <see cref="AzureSubscriptionProvisionModel"/> representing the request to be persisted.</param>
        /// <returns>A Task instance, with the result being the success or failure of the request to Insert.</returns>
        public async Task<bool> InsertRequestAsync(AzureSubscriptionProvisionModel modelToInsert)
        {
            // Return False if we cannot connect to the Cosmos instance.
            if (!await this.CreateResourcesAsync())
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
                return false;
            }

            return false;
        }

#nullable enable
        /// <summary>
        /// Retrieves an instance of the <see cref="AzureSubscriptionProvisionModel"/> based on the supplied subscription Id.
        /// </summary>
        /// <param name="subscriptionId">The unique identifier for the requested subscription.</param>
        /// <returns>An instance of the <see cref="AzureSubscriptionProvisionModel"/> if a previous request has been persisted, otherwise null.</returns>
        public async Task<AzureSubscriptionProvisionModel?> GetRequestBySubscriptionIdAsync(Guid subscriptionId)
        {
            if (!await this.CreateResourcesAsync())
            {
                return null;
            }

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

            return null;
        }
#nullable restore

        private async Task<bool> CreateResourcesAsync()
        {
            if (this.cosmosDatabase == null)
            {
                try
                {
                    this.cosmosDatabase = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(CosmosDBName);
                }
                catch (CosmosException ce)
                {
                    return false;
                }
            }

            if (this.cosmosClient == null)
            {
                try
                {
                    this.cosmosRequestContainer = await this.cosmosDatabase.CreateContainerIfNotExistsAsync(CosmosRequestsContainerName, "/SubscriptionId");
                }
                catch (CosmosException ce)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
