// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandCenter.CustomBundle;
    using CommandCenter.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Custom Bundle Options.
    /// </summary>
    [Route("api/{controller}")]
    public class FactionCustomBundleOptionsController : Controller
    {
        private ICustomBundleBandwidthService bandwidthService;
        private ICustomBundleStorageSizeService storageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FactionCustomBundleOptionsController"/> class.
        /// </summary>
        /// <param name="bandwidthService">The instance of the class implementing the <see cref="ICustomBundleBandwidthService"/> interface.</param>
        /// <param name="storageService">The instance of the class implementing the <see cref="ICustomBundleStorageSizeService"/> interface.</param>
        public FactionCustomBundleOptionsController(ICustomBundleBandwidthService bandwidthService, ICustomBundleStorageSizeService storageService)
        {
            this.bandwidthService = bandwidthService;
            this.storageService = storageService;
        }

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
        public async Task<IActionResult> GetCustomBundleOptions([FromQuery] string performanceTier)
        {
            var returnValue = new CustomBundleOptionsModel();
            FactionCustomPerformanceTier tier;
            if (Enum.TryParse<FactionCustomPerformanceTier>(performanceTier, out tier))
            {
                // These are placeholder options and will be replaced with values from a service/lookup.
                returnValue.PossibleBandwidthOptions = await this.bandwidthService.GetBandwidthOptionsForTier(tier);
                returnValue.PossibleStorageSizes = await this.storageService.GetStorageSizesForPerformanceTier(tier);
            }

            return this.Ok(returnValue);
        }
    }
}
