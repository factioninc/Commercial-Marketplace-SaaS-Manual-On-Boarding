// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.CustomBundle
{
    using System.Threading.Tasks;
    using CommandCenter.Models;

    /// <summary>
    /// Interface for Custom Bandwidth Service.
    /// </summary>
    public interface ICustomBundleBandwidthService
    {
        /// <summary>
        /// Gets the available Bandwidth options for a given performance tier.
        /// </summary>
        /// <param name="tier">The performance tier used to determine the appropriate bandwidth options.</param>
        /// <returns>A Task instance with the result being a list of strings representing the possible bandwidth options.</returns>
        Task<string[]> GetBandwidthOptionsForTier(FactionCustomPerformanceTier tier);
    }
}