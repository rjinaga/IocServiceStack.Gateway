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

namespace IocServiceStack.Gateway.Tests
{
    using Microsoft.AspNetCore.Http;
    using NUnit.Framework;
    using Contracts;
    using System.Text;
    using System.IO;

    public class ServiceGatewayHandlerTests
    {
        [Test]
        public void GetCustomerMethod()
        {
            //Arrange
            var stream = new MemoryStream(Encoding.UTF8.GetBytes( @"{ ""Metadata"":{""id"":""System.Int32"",""type"":""System.String""},""Data"":[1,""Local""]}"));
            var args = new RequestBodyParser().Parse(stream);

            ServiceRequest request = ServiceRequest.Create(
                "Contracts.ICustomer", null,
                "GetCustomer", 
                new HeaderDictionary() { ["module"] = "Contracts" } , args);

            ServiceGatewayHandler handler = new ServiceGatewayHandler();

            //Act
            var response = handler.Process(ServiceManager.Instance, request);

            //Assert
            Assert.IsNotNull(response.Body);
            var customer  = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(Encoding.UTF8.GetString(response.Body));
            Assert.IsNotEmpty(customer.Name);

        }

        [Test]
        public void SaveMethod()
        {
            //Arrange
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(@"{""Metadata"":null,""Data"":null}"));
            var args = new RequestBodyParser().Parse(stream);
            
            ServiceRequest request = ServiceRequest.Create("Contracts.ICustomer", null, "Save",
              new HeaderDictionary() { ["module"] = "Contracts" }
              ,args );
            ServiceGatewayHandler handler = new ServiceGatewayHandler();

            //Act
            var response = handler.Process(ServiceManager.Instance, request);

            //Assert
            Assert.IsNull(response?.Body);
        }
    }
}
