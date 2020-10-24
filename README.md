# RollerCoaster.Account.Proxy

<a href="https://dev.azure.com/marksamdickinson/rollercoaster/_build/latest?definitionId={BUILDID}&amp;branchName=master"> <img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/marksamdickinson/rollercoaster/{BUILDID}/master"> </a> <a href="https://dev.azure.com/marksamdickinson/rollercoaster/_build/latest?definitionId={BUILDID}&amp;branchName=master"> <img alt="Azure DevOps coverage (branch)" src="https://img.shields.io/azure-devops/coverage/marksamdickinson/rollercoaster/{BUILDID}/master"> </a><a href="https://dev.azure.com/marksamdickinson/rollercoaster/_release?_a=releases&view=mine&definitionId={RELEASEID}"> <img alt="Azure DevOps releases" src="https://img.shields.io/azure-devops/release/marksamdickinson/{RELEASECODE}"> </a><a href="https://www.nuget.org/packages/{PACKAGENAME}/"><img src="https://img.shields.io/nuget/v/{PACKAGENAME}"></a>

Account Proxy

Features
* All API End Points from Account API
* Polciy based retrys and timeouts.
* Logs for all successful and exceptional runs
* Telemetry for all calls

<h2>Example Usage</h2>

```C#
 var restResponse = await accountProxyService.LogAsync();
```
