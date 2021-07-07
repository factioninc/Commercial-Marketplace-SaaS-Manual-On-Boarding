// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Persistance
{
    using System;
    using System.Threading.Tasks;
    using CommandCenter.Models;

    /// <summary>
    /// Request Persistence Store Interface.
    /// </summary>
    public interface IRequestPersistenceStore
    {
        /// <summary>
        /// Retrieves an instance of the <see cref="AzureSubscriptionProvisionModel"/> based on the supplied subscription Id.
        /// </summary>
        /// <param name="subscriptionId">The unique identifier for the requested subscription.</param>
        /// <returns>An instance of the <see cref="AzureSubscriptionProvisionModel"/> if a previous request has been persisted, otherwise null.</returns>
        Task<AzureSubscriptionProvisionModel> GetRequestBySubscriptionIdAsync(Guid subscriptionId);

        /// <summary>
        /// Inserts a new Subscription Request into Cosmos DB.
        /// </summary>
        /// <param name="modelToInsert">The instance of the <see cref="AzureSubscriptionProvisionModel"/> representing the request to be persisted.</param>
        /// <returns>A Task instance, with the result being the success or failure of the request to Insert.</returns>
        Task<bool> InsertRequestAsync(AzureSubscriptionProvisionModel modelToInsert);
    }
}