namespace CommandCenter.Models
#pragma warning restore SA1633 // File should have header
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandCenter.CustomValidation;

    /// <summary>
    /// Faction Technical Details.
    /// </summary>
    public class FactionTechnicalDetails
    {
        /// <summary>
        /// Gets or sets the value of the requested share name.
        /// </summary>
        [Display(Name="Requested share name")]
        public string RequestedShareName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to support NFS.
        /// </summary>
        [Display(Name ="Support NFS")]
        public bool SupportNFS { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to support CIFS.
        /// </summary>
        [Display(Name ="Support CIFS")]
        public bool SupportCIFS { get; set; }

        /// <summary>
        /// Gets or sets the value of the requested subnet.
        /// </summary>
        [Display(Name = "Choose subnet")]
        [CIDRNotation(24)]
        public string RequestedSubnet { get; set; } = "173.30.0.0/22";
    }
}
