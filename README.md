# RollerCoaster.Account.Proxy

<a href="https://www.nuget.org/packages/RollerCoaster.Account.Proxy/">
    <img src="https://img.shields.io/nuget/v/RollerCoaster.Account.Proxy">
</a>

Account Proxy

Features
* All API End Points from Account API
* Polciy based retrys and timeouts.
* Logs for all successful and exceptional runs
* Telemetry for all calls

<a href="https://dev.azure.com/marksamdickinson/RollerCoaster/_build?definitionScope=%5CRollerCoaster.Account.Proxy">Builds</a>

<h2>Example Usage</h2>

```C#
 var restResponse = await accountProxyService.LogAsync();
```
