// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Custom Bundle Options Model.
    /// </summary>
    public class CustomBundleOptionsModel
    {
        /// <summary>
        /// Gets or sets the possible bandwidth options.
        /// </summary>
        public string[] PossibleBandwidthOptions { get; set; } = { };

        /// <summary>
        /// Gets or sets the possible storage sizes.
        /// </summary>
        public string[] PossibleStorageSizes { get; set; } = { };
    }
}
