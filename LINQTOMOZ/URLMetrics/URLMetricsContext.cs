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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace LINQTOMOZ
{
	/// <summary>
	/// Reponse object by URL Metrics request
	/// </summary>
	public class URLMetricsContext
	{


		/// <summary>The email address for the target entity, if found on the site. Emails are collected automatically and are not CAN-SPAM compliant -- they cannot be used in outbound mail campaigns </summary>
		public String fem { get; set; }
		/// <summary>Facebook account for the target entity, if found on the site </summary>
		public String ffb { get; set; }
		/// <summary>The Google+ account for the target entity, if found on the site </summary>
		public String fg { get; set; }
		/// <summary>Language of the subdomain </summary>
		public String flan { get; set; }
		/// <summary>Epoch time when the subdomain was last crawled </summary>
		public int fsplc { get; set; }
		/// <summary>Bit field of triggered spam flags </summary>
		public object fspf { get; set; }
		/// <summary>List of pages used for the subdomain's spam crawl </summary>
		[JsonConverter(typeof(JsonCustomConverter))]
		public List<String> fspp { get; set; }
		/// <summary>HTTP status code: The HTTP status code of the spam crawl </summary>
		public int fsps { get; set; }
		/// <summary>Spam Score: The Spam Score for the page's subdomain </summary>
		public int fspsc { get; set; }
		/// <summary>The Twitter handle for the target entity, if found on the site </summary>
		public String ftw { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozTrust of the subdomain of the target URL </summary>
		public double ftrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozTrust of the subdomain of the target URL </summary>
		public double ftrr { get; set; }
		/// <summary>The number of internal and external, equity and non-equity links to the subdomain of the target URL </summary>
		public int fuid { get; set; }
		/// <summary>Internal ID of the link </summary>
		public String lrid { get; set; }
		/// <summary>Internal ID of the source URL </summary>
		public String lsrc { get; set; }
		/// <summary>Internal ID of the target URL </summary>
		public String ltgt { get; set; }
		/// <summary>The number of external (from other subdomains), equity links to pages on the target URLs subdomain </summary>
		public int lufeid { get; set; }
		/// <summary>The normalized (logarithmically-scaled) sum of MozRank from external links on all pages of the subdomain of the target URL </summary>
		public int lufejp { get; set; }
		/// <summary>The raw (linearly-scaled) sum of MozRank from external links on all pages of the subdomain of the target URL </summary>
		public int lufejr { get; set; }
		/// <summary>The number of domains with at least one link to any page on the subdomain of the target URL </summary>
		public int lufid { get; set; }
		/// <summary>The number of domains with at least one link to any page on the subdomain of the target URL </summary>
		public int lufipl { get; set; }
		/// <summary>The normalized (logarithmically-scaled) sum of the MozRank of all the pages of the subdomain of the target URL </summary>
		public int lufjp { get; set; }
		/// <summary>The raw (linearly-scaled) sum of the MozRank of all the pages of the subdomain of the target URL </summary>
		public int lufjr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) measure of the MozRank of the subdomain of the target URL </summary>
		public double lufmrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) measure of the MozRank of the subdomain of the target URL </summary>
		public double lufmrr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) measure of the MozRank of the subdomain </summary>
		public double fmrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) measure of the MozRank of the subdomain </summary>
		public double fmrr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) measure of the MozTrust of the subdomain of the target URL </summary>
		public double luftrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) measure of the MozTrust of the subdomain of the target URL </summary>
		public double luftrr { get; set; }
		/// <summary>The number of internal and external equity and non-equity links to the subdomain of the target URL </summary>
		public int lufuid { get; set; }
		/// <summary>The normalized (zero to one hundred, logarithmically-scaled) domain authority of the target URL's paid-level domain </summary>
		public double lupda { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) domain authority of the target URL's paid-level domain </summary>
		public double lupdar { get; set; }
		/// <summary>The number of external equity links from other root domains to pages on the target URL's root domain </summary>
		public int lupeid { get; set; }
		/// <summary>The normalized (logarithmically-scaled) sum of MozRank gained from external links on all pages of the paid-level domain of the target URL </summary>
		public double lupejp { get; set; }
		/// <summary>The raw (linearly-scaled) sum of MozRank gained from external links on all pages of the paid-level domain of the target URL </summary>
		public double lupejr { get; set; }
		/// <summary>The number of domains with at least one link to any page on the paid-level domain of the target URL </summary>
		public int lupid { get; set; }
		/// <summary>The normalized (logarithmically-scaled) sum of MozRank gained from all pages on the paid-level domain of the target URL </summary>
		public double lupjp { get; set; }
		/// <summary>he raw (linearly-scaled) sum of MozRank gained from all pages on the paid-level domain of the target URL </summary>
		public int lupjr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) measure of the MozRank of the paid-level domain of the target URL </summary>
		public double lupmrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) measure of the MozRank of the paid-level domain of the target URL </summary>
		public double lupmrr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) measure of the MozTrust of the paid-level domain of the target URL </summary>
		public int luptrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) measure of the MozTrust of the paid-level domain of the target URL </summary>
		public int luptrr { get; set; }
		/// <summary>The number of internal and external, equity and non-equity links to the root domain of the target URL </summary>
		public long lupuid { get; set; }
		/// <summary>The number of external (from other subdomains) equity links to the target URL </summary>
		public int luueid { get; set; }
		/// <summary>last crawl luu </summary>
		public String luulc { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozRank gained from external links of the target URL </summary>
		public double luuemrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozRank of the target URL gained from external links </summary>
		public int luuemrr { get; set; }
		/// <summary>The fully-qualified domain name ('subdomain') </summary>
		public String luufq { get; set; }
		/// <summary>The number of internal and external equity and non-equity links to the target URL </summary>
		public int luuid { get; set; }
		/// <summary>The number of subdomains with at least one link to the target URL </summary>
		public int luuifq { get; set; }
		/// <summary>The number of paid-level domains with at least one link to the target URL </summary>
		public int luuipl { get; set; }
		/// <summary>The number of equity links (internal or external) to the target URL\n </summary>
		public int luujid { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozRank of the target URL </summary>
		public double luumrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozRank of the target URL </summary>
		public double luumrr { get; set; }
		/// <summary>The normalized (zero to one hundred, logarithmically-scaled) page authority of the target URL </summary>
		public double luupa { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) page authority of the target URL </summary>
		public double luupar { get; set; }
		/// <summary>The paid-level domain name </summary>
		public String luupl { get; set; }
		/// <summary>Internal ID of the canonical URL </summary>
		public String luurrid { get; set; }
		/// <summary>HTTP status of the target URL </summary>
		public int luus { get; set; }
		/// <summary>The title of the target URL, if available </summary>
		public String luut { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozTrust of the target URL </summary>
		public double luutrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozTrust of the target URL </summary>
		public double luutrr { get; set; }
		/// <summary>The canonical form of the target URL </summary>
		public String luuu { get; set; }
		/// <summary>The normalized (zero to one hundred, logarithmically-scaled) domain authority of the source URL's paid-level domain </summary>
		public double pda { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) domain authority of the source URL's paid-level domain </summary>
		public double pdar { get; set; }
		/// <summary>the number of external equity links to pages on the source URL's root domain </summary>
		public int peid { get; set; }
		/// <summary>The normalized (logarithmically-scaled) sum of the MozRank gained from external links on all pages in the paid-level domain of the source URL </summary>
		public double pejp { get; set; }
		/// <summary>The raw (linearly-scaled) sum of the MozRank gained from external links on all pages in the paid-level domain of the source URL </summary>
		public double pejr { get; set; }
		/// <summary>The number of domains with at least one link to any page on the paid-level domain of the source URL </summary>
		public int pid { get; set; }
		/// <summary>The normalized (logarithmically-scaled) sum of the MozRank of all pages in the paid-level domain of the source URL </summary>
		public double pjp { get; set; }
		/// <summary>The raw (linearly-scaled) sum of the MozRank of all the pages in the paid-level domain of the source URL </summary>
		public double pjr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozRank of the paid-level domain of the source URL </summary>
		public double pmrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) measure of the MozRank of the paid-level domain of the source URL </summary>
		public double pmrr { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozTrust of the paid-level domain of the source URL </summary>
		public double ptrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozTrust of the paid-level domain of the source URL </summary>
		public double ptrr { get; set; }
		/// <summary>The number of internal and external equity and non-equity links to the root domain of the source URL </summary>
		public long puid { get; set; }
		/// <summary>The number of external links to the target URL, including nofollowed links </summary>
		public int ued { get; set; }
		/// <summary>The number of external, equity links to the target URL </summary>
		public int ueid { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozRank of the target URL gained from external links </summary>
		public double uemrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozRank of the target URL gained from external links </summary>
		public double uemrr { get; set; }
		/// <summary>The fully qualified domain (subdomain) name </summary>
		public String ufq { get; set; }
		/// <summary>The number of internal and external equity and non-equity links to the target URL </summary>
		public int uid { get; set; }
		/// <summary>The number of subdomains with at least one link to the target URL </summary>
		public int uifq { get; set; }
		/// <summary>The number of paid-level domains with at least one link to the target URL </summary>
		public int uipl { get; set; }
		/// <summary>The number of equity links, internal or external, to the target URL </summary>
		public int ujid { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozRank of the target URL </summary>
		public double umrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozRank of the target URL </summary>
		public double umrr { get; set; }
		/// <summary>The normalized (zero to one hundred, logarithmically-scaled) page authority of the target URL </summary>
		public double upa { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) page authority of the target URL </summary>
		public double upar { get; set; }
		/// <summary>The paid-level domain name </summary>
		public String upl { get; set; }
		/// <summary>The canonical target URL, if there are any canonicalization tags on the URL (for example, a 301 redirect). </summary>
		public String ur { get; set; }
		/// <summary>Internal ID of the URL </summary>
		public String urid { get; set; }
		/// <summary>Internal ID of the canonical URL </summary>
		public String urrid { get; set; }
		/// <summary>The HTTP status of the target URL </summary>
		public int us { get; set; }
		/// <summary>The protocols Mozscape encountered for the target URL, and whether a canonical tag indicates a specific protocol </summary>
		public String usch { get; set; }
		/// <summary>The title of the target URL, if a title is available </summary>
		public String ut { get; set; }
		/// <summary>The normalized (ten-point, logarithmically-scaled) MozTrust of the target URL </summary>
		public double utrp { get; set; }
		/// <summary>The raw (zero to one, linearly-scaled) MozTrust of the target URL </summary>
		public double utrr { get; set; }
		/// <summary>The canonical form of the source URL </summary>
		public String uu { get; set; }
		/// <summary>Epoch last crawl source </summary>
		public String ulc { get; set; }
		/// <summary>Link cols. Return array of string which contains attributes of link which was found for provided url </summary>
		[JsonConverter(typeof(JsonCustomConverter))]
		public List<string> lf { get; set; }
		//[Description("Link cols. Return array of string which contains attributes of link which was found for provided url")]
		//public int lf { get; set; }

		public String lt { get; set; }

		public String lnt { get; set; }



	}
}
