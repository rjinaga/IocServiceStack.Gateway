# IocServiceStack.Gateway

[![Gitter](https://badges.gitter.im/IocServiceStack/Lobby.svg)](https://gitter.im/IocServiceStack/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=body_badge)

IocServiceStack.Gateway is a open source .NET library acts as conjunction between IocServiceStack.Client and IocServiceStack services.


### Supports
- .NET Core 1.0 (.NET Standard 1.6)
- .NET Framework 4.6


### [NuGet](https://www.nuget.org/packages/IocServiceStack.Gateway/)
```
PM> Install-Package IocServiceStack.Gateway -Pre
```
[![NuGet Pre Release](https://img.shields.io/badge/nuget-Pre%20Release-yellow.svg)](https://www.nuget.org/packages/IocServiceStack.Gateway/)


## Usage

### Setup

```c#
using IocServiceStack.Gateway;

app.UseMvc(route =>
{
     /* Default url template "ServiceApi/{service}/{operation}" */	
     route.MapServicesGateway();
});

```

### Web N Tier Architecture using IocServiceStack, IocServiceStack.Gateway, IocServiceStack.Client 

https://github.com/rjinaga/Web-N-Tier-Architecture





