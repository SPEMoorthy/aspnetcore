// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace StandaloneApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddBaseAddressHttpClient();

            Console.WriteLine(CultureInfo.CurrentCulture);

            foreach (DictionaryEntry x in Resource2.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true))
            {
                Console.WriteLine(x.Key + " " + x.Value);
            }


            //System.Console.WriteLine(Resource.Test);

            await builder.Build().RunAsync();
        }
    }

    internal static partial class Resource2
    {
        private static global::System.Resources.ResourceManager s_resourceManager;
        internal static global::System.Resources.ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new global::System.Resources.ResourceManager("StandaloneApp.Resource", typeof(Resource2).Assembly));
        internal static global::System.Globalization.CultureInfo Culture { get; set; }

        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static string GetResourceString(string resourceKey, string defaultValue = null) => ResourceManager.GetString(resourceKey, CultureInfo.CurrentCulture);

        private static string GetResourceString(string resourceKey, string[] formatterNames)
        {

           

            var value = GetResourceString(resourceKey);
            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }
            return value;
        }

        /// <summary>Hello</summary>
        internal static string Hello => GetResourceString("Hello");

    }
}
