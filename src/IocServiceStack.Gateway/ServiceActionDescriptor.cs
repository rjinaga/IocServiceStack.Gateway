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
    using System.Reflection;

    public class ServiceActionDescriptor
    {
        private MethodInfo _methodInfo;

        public Type ContractType;
        public string MethodName;

        public object Execute(params Argument[] arguments)
        {
            var serviceInstance = ServiceManager.GetService(ContractType);

            _methodInfo = ContractType.GetMethod(MethodName);
            var result =_methodInfo.Invoke(serviceInstance, GetArguments(arguments));

            return result;
        }

        private object[] GetArguments(Argument[] arguments)
        {
            if (arguments == null)
                return null;

            object[] args = new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                args[i] = arguments[i].Value;
            }
            return args;
        }

    }
    
}
