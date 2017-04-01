/* Copyright 2017 Pavel MATUA

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
namespace LINQTOMOZ
{
	[Flags]
	public enum URLMetricsCols : long
	{
		None = 0,
		Title = 1,
		CanonicalURL = 4,
		Subdomain = 8,
		RootDomain = 16,
		ExternalEquityLinks = 32,
		SubdomainExternalLinks = 64,
		RootDomainExternalLinks = 128,
		EquityLinks = 256,
		SubdomainsLinking = 512,
		RootDomainsLinking = 1024,
		Links = 2048,
		SubdomainsLink = 4096,
		RootDomainRootDomainsLinking = 8192,
		MozRankURL = 16384,
		MozRankSubdomain = 32768,
		MozRankRootDomain = 65536,
		MozTrust = 131072,
		MozTrustSubdomain = 262144,
		MozTrustRootDomain = 524288,
		MozRankExternalEquity = 1048576,
		MozRankSubdomainExternalEquity = 2097152,
		MozRankRootDomainExternalEquity = 4194304,
		MozRankSubdomainCombined = 8388608,
		MozRankRootDomainCombined = 16777216,
		SubdomainSpamScore = 67108864,
		Social = 134217728,
		HTTPStatusCode = 536870912,
		LinkstoSubdomain = 4294967296,
		LinksToRootDomain = 8589934592,
		RootDomainsLinkingToSubdomain = 17179869184,
		PageAuthority = 34359738368,
		DomainAuthority = 68719476736,
		ExternalLinks = 549755813888,
		ExternalLinksToSubdomain = 140737488355328,
		ExternalLinksToRootDomain = 2251799813685248,
		LinkingCBlocks = 36028797018963968,
		TimeLastCrawled = 144115188075855872,

		// Can be modified based on business logic
		All = 141421159907325,
		FREE = Title | CanonicalURL | ExternalEquityLinks | Links | MozRankURL | MozRankSubdomain | HTTPStatusCode | PageAuthority | DomainAuthority | TimeLastCrawled


	}
}
