// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Options for the Persistence Store.
    /// </summary>
    public class PersistenceStoreOptions
    {
        /// <summary>
        /// Gets or sets the Endpoint property.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the Primary Key property.
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets the Database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the Container name.
        /// </summary>
        public string ContainerName { get; set; }
    }
}
