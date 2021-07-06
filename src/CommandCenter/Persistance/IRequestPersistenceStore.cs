// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CommandCenter.Models;
using System;
using System.Threading.Tasks;

namespace CommandCenter.Persistance
{
    public interface IRequestPersistenceStore
    {
        Task<AzureSubscriptionProvisionModel> GetRequestBySubscriptionIdAsync(Guid subscriptionId);
        Task<bool> InsertRequestAsync(AzureSubscriptionProvisionModel modelToInsert);
    }
}