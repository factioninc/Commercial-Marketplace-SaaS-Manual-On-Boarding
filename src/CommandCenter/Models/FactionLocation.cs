#pragma warning disable SA1633 // File should have header
namespace CommandCenter.Models
#pragma warning restore SA1633 // File should have header
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Client Requested Faction Location.
    /// </summary>
    public enum FactionLocation
    {
        /// <summary>
        /// Northern Virginia
        /// </summary>
        [Display(Name ="Northern VA")]
        NorthernVA,

        /// <summary>
        /// SantaClara, CA
        /// </summary>
        [Display(Name ="Santa Clara, CA")]
        SantaClaraCA,
    }
}
