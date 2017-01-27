#region License
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
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public class ServiceRequest
    {
        public readonly string ServiceName;
        public readonly string ActionName;
        public readonly string ServiceType;
        public readonly IHeaderDictionary Headers;
        public readonly IEnumerable<Argument> Arguments;

        private ServiceRequest(string serviceName, string serviceType, string actionName, IHeaderDictionary headers, IEnumerable<Argument> arguments)
        {
            ServiceName = serviceName;
            ActionName = actionName;
            Headers = headers;
            Arguments = arguments;
            ServiceType = serviceType;
        }

        public string Signature => $"{ServiceName}.{ActionName}";

        public static ServiceRequest Create(string serviceName, string serviceType, string actionName, IHeaderDictionary headers, IEnumerable<Argument> arguments)
        {
            return new ServiceRequest(serviceName, serviceType, actionName, headers, arguments);
        }
    }
}