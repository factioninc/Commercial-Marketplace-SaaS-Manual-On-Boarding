// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CommandCenter.Models;
using System.Threading.Tasks;

namespace CommandCenter.CustomBundle
{
    public interface ICustomBundleStorageSizeService
    {
        Task<string[]> GetStorageSizesForPerformanceTier(FactionCustomPerformanceTier tier);
    }
}