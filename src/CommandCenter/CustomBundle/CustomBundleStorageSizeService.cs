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
    /// Custom Bundle Storage Size Service.
    /// </summary>
    public class CustomBundleStorageSizeService : ICustomBundleStorageSizeService
    {
        /// <summary>
        /// Gets the possible Storage Sizes for a given performance tier.
        /// </summary>
        /// <param name="tier">The performance tier to be used to determine the appropriate sizes.</param>
        /// <returns>A Task reference, the result of which is an array of strings containing the possible storage sizes.</returns>
        public Task<string[]> GetStorageSizesForPerformanceTier(FactionCustomPerformanceTier tier)
        {
            var possibleOptions = new string[] { };

            switch (tier)
            {
                case FactionCustomPerformanceTier.Archive:
                case FactionCustomPerformanceTier.Premier:
                    {
                        possibleOptions = new string[] { "162 TB", "252 TB", "342 TB", "432 TB", "522 TB", "612 TB", "650+ TB" };
                        break;
                    }

                case FactionCustomPerformanceTier.Standard:
                    {
                        possibleOptions = new string[] { "540 TB", "640 TB", "1 PB+" };
                        break;
                    }

                case FactionCustomPerformanceTier.Elite:
                    {
                        possibleOptions = new string[] { "130 TB", "207 TB", "284 TB", "361 TB", "438 TB", "500+ TB" };
                        break;
                    }
            }

            return Task.FromResult(possibleOptions);
        }
    }
}
