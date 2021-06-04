// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.CustomBundle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandCenter.Models;

    /// <summary>
    /// Custom Bundle Bandwidth Service.
    /// Generates the appropriate list of bandwidth options based on provided performance tier.
    /// </summary>
    public class CustomBundleBandwidthService : ICustomBundleBandwidthService
    {
        /// <summary>
        /// Returns the list of possible bandwidth options, based on the Performance tier input.
        /// </summary>
        /// <param name="tier">The performance tier to use as the selector for the possible bandwidth options.</param>
        /// <returns>A Task reference, with the result being the names of the bandwidth options.</returns>
        public Task<string[]> GetBandwidthOptionsForTier(FactionCustomPerformanceTier tier)
        {
            var possibleOptions = new string[] { };
            switch (tier)
            {
                case FactionCustomPerformanceTier.Archive:
                    {
                        possibleOptions = new string[] { "10 Gb/s", "20 Gb/s", "30 Gb/s", "40 Gb/s", "50+ Gb/s" };
                        break;
                    }

                case FactionCustomPerformanceTier.Standard:
                    {
                        possibleOptions = new string[] { "20 Gb/s", "30 Gb/s", "40 Gb/s", "50+ Gb/s" };
                        break;
                    }

                case FactionCustomPerformanceTier.Premier:
                    {
                        possibleOptions = new string[] { "40 Gb/s", "50 Gb/s", "60 Gb/s", "70 Gb/s", "80 Gb/s", "100+ Gb/s" };
                        break;
                    }

                case FactionCustomPerformanceTier.Elite:
                    {
                        possibleOptions = new string[] { "20 Gb/s", "30 Gb/s", "40 Gb/s", "50 Gb/s", "60 Gb/s", "70 Gb/s", "80 Gb/s", "100+ Gb/s" };
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            return Task.FromResult(possibleOptions);
        }
    }
}
