﻿#region License
// Copyright (c) 2016 Rajeswara-Rao-Jinaga
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

namespace IocServiceStack.Gateway
{
    using System;
    using Microsoft.AspNetCore.Routing;

    public static class RouterBuilderExtensions
    {
        /// <summary>
        /// Maps gateway route to template "ServiceApi/{service}/{operation}/{serviceType}"
        /// 
        /// internal meaning of: {service} is type (class/interface) name of the contract. 
        /// {type} is type of the service implementation and {type} is optional.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceManager">Specify the <see cref="IServiceManager"/> (IocContainer) to be used for access the services</param>
        public static void MapServicesGateway(this IRouteBuilder builder, IServiceManager serviceManager)
        {
            string urlTemplate = "ServiceApi/{service}/{operation}/{serviceType?}";
            MapServicesGateway(builder, urlTemplate, serviceManager);
        }

        public static void MapServicesGateway(this IRouteBuilder builder, string urlTemplate, IServiceManager serviceManager)
        {
            if (string.IsNullOrEmpty(urlTemplate))
                throw new ArgumentNullException(nameof(urlTemplate));

            var processor = new GatewayRequestProcessor(serviceManager);

            builder.MapRoute(urlTemplate, processor.Process);
        }
    }
}
