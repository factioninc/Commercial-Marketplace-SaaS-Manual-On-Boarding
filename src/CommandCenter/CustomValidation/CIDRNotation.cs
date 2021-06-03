// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.CustomValidation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// CIDR Notation Validation.
    /// </summary>
    public class CIDRNotation : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIDRNotation"/> class.
        /// </summary>
        /// <param name="maxNumberBits">The maximum value for the number of bits.</param>
        public CIDRNotation(int maxNumberBits)
        {
            this.MaxNumberBits = maxNumberBits;
        }

        /// <summary>
        /// Gets the maximum number of bits to be used for validation.
        /// </summary>
        public int MaxNumberBits { get; }

        /// <summary>
        /// Validates the string presented to ensure that it is in CIDR notation format.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="validationContext">The model that contains this CIDR object.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueToValidate = value as string;

            if (string.IsNullOrWhiteSpace(valueToValidate))
            {
                return new ValidationResult("Please enter a value.");
            }

            var cidrSplit = valueToValidate.Split('/');
            if (cidrSplit.Length != 2)
            {
                return new ValidationResult("Please enter a valid CIDR formatted value.");
            }

            var testAddress = IPAddress.None;
            if (!IPAddress.TryParse(cidrSplit[0], out testAddress))
            {
                // Could not decode first part into IP Address
                return new ValidationResult("IP address portion of CIDR value must be valid.");
            }

            var cidrValue = 1024;
            if (!int.TryParse(cidrSplit[1], out cidrValue))
            {
                return new ValidationResult("CIDR subnet bits must be an integer value.");
            }

            if (cidrValue <= 0 || cidrValue > this.MaxNumberBits)
            {
                return new ValidationResult($"CIDR subnet bits <0 or greater than >{this.MaxNumberBits}.");
            }

            return ValidationResult.Success;
        }
    }
}
