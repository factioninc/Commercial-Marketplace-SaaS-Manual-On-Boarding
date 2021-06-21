// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Faction Custom Bundle Model.
    /// </summary>
    public class FactionCustomBundleModel
    {
        /// <summary>
        /// Gets or sets the Requested Performance Tier.
        /// </summary>
        [Display(Name="Requested Performance Tier")]
        public FactionCustomPerformanceTier RequestedPerformanceTier { get; set; }

        /// <summary>
        /// Gets or sets the Requested Storage Size.
        /// </summary>
        [Display(Name ="Requested Storage Size")]
        public string RequestedStorageSize { get; set; }

        /// <summary>
        /// Gets or sets the Requested Bandwidth.
        /// </summary>
        [Display(Name ="Requested Bandwidth")]
        public string RequestedBandwidth { get; set; }
    }
}
