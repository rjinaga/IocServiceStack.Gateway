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
    using System;
    using System.Linq;

    public sealed class ServiceGatewayHandler : IServiceGatewayHandler
    {
        public ServiceResponse Process(IServiceManager serviceManager, ServiceRequest request)
        {
            if (!ServiceActionsDictionary.Instance.ContainsKey(request.Signature))
            {
                var @namespace = request.Headers["ns"].ToString();
                var module = request.Headers["module"];

                //concatenate the dot at end in order to make full name of the class .
                @namespace = !string.IsNullOrEmpty(@namespace) ? @namespace.Trim() + "." : "";

                ServiceActionsDictionary.Instance[request.Signature] = new ServiceActionDescriptor
                {
                    ContractType = Type.GetType($"{@namespace}{request.ServiceName}, {module}"),
                    MethodName = request.ActionName,
                };
            }

            ServiceActionDescriptor descriptor = ServiceActionsDictionary.Instance[request.Signature];
            var result = descriptor.Execute(serviceManager, request.ServiceType, request.Arguments.ToArray());

            if (result != null)
            {
                var response = new ServiceResponse();
                response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                return response;
            }

            return null;
        }
    }
}
