// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting
{
    internal class SatelliteResourcesLoader
    {
        private readonly WebAssemblyJSRuntimeInvoker _invoker;
        private readonly CultureInfo _initialCulture;
        private string[] _availableCultures;

        public SatelliteResourcesLoader(WebAssemblyJSRuntimeInvoker invoker)
        {
            _invoker = invoker;
            _initialCulture = CultureInfo.CurrentCulture;
        }

        public ValueTask LoadDefaultCultureResourcesAsync()
        {
            if (_initialCulture == CultureInfo.InvariantCulture)
            {
                return default;
            }

            _availableCultures = _invoker.InvokeUnmarshalled<object, object, object, string[]>(
                "window.Blazor._internal.getSatelliteAssemblyCultures",
                null, null, null);

            return LoadSatelliteAssembliesForCurrentCultureAsync();
        }

        public ValueTask LoadUserCultureResourcesAsync()
        {
            if (CultureInfo.CurrentCulture == _initialCulture)
            {
                // Nothing to do since the culture wasn't configured.
                return default;
            }

            return LoadSatelliteAssembliesForCurrentCultureAsync();
        }

        private async ValueTask LoadSatelliteAssembliesForCurrentCultureAsync()
        {
            if (_availableCultures is null)
            {
                return;
            }

            var culturesToLoad = new List<string>();

            var cultureInfo = CultureInfo.CurrentCulture;
            while (cultureInfo != null)
            {
                if (_availableCultures.Contains(cultureInfo.Name))
                {
                    culturesToLoad.Add(cultureInfo.Name);
                }

                if (cultureInfo.Parent == cultureInfo)
                {
                    break;
                }

                cultureInfo = cultureInfo.Parent;
            }

            if (culturesToLoad.Count != 0)
            {
                await _invoker.InvokeUnmarshalled<string[], object, object, Task>(
                    "window.Blazor._internal.loadSatelliteAssemblies",
                    culturesToLoad.ToArray(),
                    null,
                    null);
            }
        }
    }
}
