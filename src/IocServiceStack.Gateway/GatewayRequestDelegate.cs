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
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System.Threading.Tasks;

    public class GatewayRequestProcessor
    {
        public static async Task Process(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;

            IRequestBodyParser parser = new RequestBodyParser();
            var arguments =parser.Parse(context.Request.Body);

            var serviceRequest = ServiceRequest.Create(routeValues["service"].ToString(), routeValues["operation"].ToString(), context.Request.Headers, arguments);
            ServiceResponse response = new ServiceGatewayHandler().Process(serviceRequest);

            if (response != null && response.Body != null)
            {
                await context.Response.Body.WriteAsync(response.Body, 0, response.Body.Length);
            }
        }
    }
}
