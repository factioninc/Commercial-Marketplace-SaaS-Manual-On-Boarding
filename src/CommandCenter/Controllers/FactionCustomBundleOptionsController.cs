// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandCenter.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Custom Bundle Options.
    /// </summary>
    [Route("api/{controller}")]
    public class FactionCustomBundleOptionsController : Controller
    {
        /// <summary>
        /// Default Get for Faction Custom Bundle Options.
        /// </summary>
        /// <returns>The default view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Gets the possible custom bundle options for a provided performance tier.
        /// </summary>
        /// <param name="performanceTier">The performance tier identifier used to determine the available options.</param>
        /// <returns>An instance of the <see cref="CustomBundleOptionsModel"/> which contains the avaiable options for the given performance tier.</returns>
        [HttpGet]
        public IActionResult GetCustomBundleOptions([FromQuery] string performanceTier)
        {
            var returnValue = new CustomBundleOptionsModel();
            FactionCustomPerformanceTier tier;
            if (Enum.TryParse<FactionCustomPerformanceTier>(performanceTier, out tier))
            {
                // These are placeholder options and will be replaced with values from a service/lookup.
                returnValue.PossibleBandwidthOptions = new string[] { "10 GB", "20 GB", "30 GB", "40 GB", "50+ GB" };
                returnValue.PossibleStorageSizes = new string[] { "162 TB", "252 TB", "342 TB", "432 TB", "522 TB", "612 TB", "650+ TB" };
            }

            return this.Ok(returnValue);
        }
    }
}
