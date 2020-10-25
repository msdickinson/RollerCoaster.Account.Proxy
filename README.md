# RollerCoaster.Account.Proxy
<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/marksamdickinson/rollercoaster/78/master"> </a> <a href="https://dev.azure.com/marksamdickinson/rollercoaster/_build/latest?definitionId=78&amp;branchName=master"> <img alt="Azure DevOps coverage (branch)" src="https://img.shields.io/azure-devops/coverage/marksamdickinson/rollercoaster/78/master"> </a><a href="https://dev.azure.com/marksamdickinson/rollercoaster/_release?_a=releases&view=mine&definitionId=1"> <img alt="Azure DevOps releases" src="https://img.shields.io/azure-devops/release/marksamdickinson/b03f3ce8-c619-424d-a382-2fe249467527/1/1"> </a><a href="https://www.nuget.org/packages/RollerCoaster.Account.Proxy/"><img src="https://img.shields.io/nuget/v/RollerCoaster.Account.Proxy"></a>


Account Proxy

Features
* All API End Points from Account API
* Policy based retrys and timeouts.
* Logs for all successful and exceptional runs
* Telemetry for all calls

<h2>Example Usage</h2>

```C#
 var restResponse = await accountProxyService.LogAsync();
```
