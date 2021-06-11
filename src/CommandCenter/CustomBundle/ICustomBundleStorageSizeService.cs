// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.CustomBundle
{
    using System.Threading.Tasks;
    using CommandCenter.Models;

    /// <summary>
    /// Interface for the Custom Bundle Storage Size Service.
    /// </summary>
    public interface ICustomBundleStorageSizeService
    {
        /// <summary>
        /// Gets the available storage sizes for a given performance tier.
        /// </summary>
        /// <param name="tier">The requested performance tier.</param>
        /// <returns>A Task, with the result being a list of strings representing the possible storage sizes.</returns>
        Task<string[]> GetStorageSizesForPerformanceTier(FactionCustomPerformanceTier tier);
    }
}