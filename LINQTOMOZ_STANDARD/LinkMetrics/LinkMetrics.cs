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
	public class LinkMetrics : IQueryData
	{
		public URLMetricsCols SourceCols { get; set; }

		public URLMetricsCols TargetCols { get; set; }

		public LinkMetricsCols LinkCols { get; set; }

		public String Scope { get; set; }

		public String Sort { get; set; }

		public String Filters { get; set; }

		public String SearchingURL { get; set; }

		public String SourceDomainURL { get; set; }

		public URLMetricsContext MetricsResult { get; set; }

	}

	public static class ScopeType
	{
		public const string PageToPage = "page_to_page";
		public const string PageToSubDomain = "page_to_subdomain";
		public const string PageToDomain = "page_to_domain";
		public const string SubDomainToPage = "subdomain_to_page";
		public const string SubDomainToSubDomain = "subdomain_to_subdomain";
		public const string SubDomainToDomain = "subdomain_to_domain";
		public const string DomainToPage = "domain_to_page";
		public const string DomainToSubDomain = "domain_to_subdomain";
		public const string DomainToDomain = "domain_to_domain";
	}

	public static class Sort
	{
		public const string PageAuthority = "page_authority";
		public const string DomainAuthority = "domain_authority";
		public const string DomainsLinkingDomain = "domains_linking_domain";
		public const string DomainsLinkingPage = "domains_linking_page";
		public const string SpamScore = "spam_score";

	}

	public static class Filters
	{
		public const string Internal = "internal";
		public const string External = "external";
		public const string Follow = "follow";
		public const string NoFollow = "nofollow";
		public const string Nonequity = "nonequity";
		public const string Equity = "equity";
		public const string RelCanonical = "rel_canonical";
		public const string ThreeHundredOne = "301";
		public const string ThreeHundredTwo = "302";

	}


}
