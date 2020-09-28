﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.WindowsAzure.Storage;
    using Serilog;
    using Serilog.Events;

    /// <summary>
    /// Entrypoint.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Create the web host builder.
        /// </summary>
        /// <param name="args">argument list.</param>
        /// <returns>Web host builder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }

        /// <summary>
        /// Application entrypoint.
        /// </summary>
        /// <param name="args">arguments list.</param>
        /// <returns>int.</returns>
        public static int Main(string[] args)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=ercmktplc;AccountKey=9XPS5+eorM4eST0pp3XH98jtNwS5aomqYwU6w4hoTnJT89bYX9X3Tjgd4RrQ4TK8ktEByV4gPnIYT/l+O/yuVQ==;EndpointSuffix=core.windows.net");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.AzureTableStorage(storage)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}