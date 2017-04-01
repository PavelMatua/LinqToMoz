# LinqToMoz
Linq provider to MOZ API

The principle of library works based on [MOZ Documentation](https://moz.com/help/guides/moz-api/mozscape/overview)

Implemented metrics:

* URL Metrics
* Link Metrics

## How use it?
For use this library you should just add LINQTOMOZ and LINQ namespace to your class:

```c#
using System.Linq;
using LINQTOMOZ;
```
Next step is instantiate MOZService class. Constructor of this class takes 2 arguments:

* access id
* security key

```c#
MOZService service = new MOZService("your access key","your security key");
```
This class provide you two main services. Link and Url metrcis services:
```c#
var urlMetrics = service.QueryURLMetrics();

//or

var linkMetrics = service.QueryLinkMetrics();
```
These variables hold the type of QueryableMozData which implement the IQueryable interface, so don't forget include System.Linq namespace

## Example

URL Metrics:
```c#
string[] sites = { "yandex.ru", "google.com", "yahoo.com", "amazon.com", "microsoft.com"};

try
{
	MOZService service = new MOZService("your access key","your security key");
	var urlMetrics = service.QueryURLMetrics(
	var result = urlMetrics.Where((arg) => sites.Contains(arg.SearchingURL) && arg.SourceCols == URLMetricsCols.FREE)
		.Select(x => new { equityLinkNumber = x.MetricsResult.ueid, cononicalURL = x.MetricsResult.uu })
		.OrderByDescending(x => x.equityLinkNumber);	
	foreach (var webSite in result)
	{
		Console.WriteLine("For: " + webSite.cononicalURL + " was found: " + webSite.equityLinkNumber + " external 		  equity  links");

	}


}
catch (LINQTOMOZ.Exception e)
{
	Console.WriteLine(e.Message);
}

//		For: google.com / was found: 2695897 external equity links
// 		For: microsoft.com / was found: 478486 external equity links
//		For: yahoo.com / was found: 233997 external equity links
//		For: yandex.ru / was found: 146768 external equity links
//		For: amazon.com / was found: 141064 external equity links
```
Link Metrics:
```c#
MOZService service = new MOZService("your access key","your security key");
var linkMetrics = service.QueryLinkMetrics();

var result = linkMetrics.Where(x => x.SearchingURL == "www.quick.fr" && x.Scope == ScopeType.PageToDomain && x.Sort == Sort.DomainAuthority && x.SourceCols == URLMetricsCols.FREE && x.LinkCols == (LinkMetricsCols.Flags | LinkMetricsCols.AnchorText | LinkMetricsCols.NormalizedAnchorText | LinkMetricsCols.MozRankPassed))
			.Select(x => new
				{
					sourceURL = x.MetricsResult.uu,
					targetURL = x.MetricsResult.luuu,
					domainAuthoritySource = x.MetricsResult.pda,
					linkFlags = x.MetricsResult.lf,
					anchorText = x.MetricsResult.lt,
					normalisedAnchorText = x.MetricsResult.lnt
				}).Take(12);
StringBuilder sb = new StringBuilder();
int i = 1;
foreach (var metrics in result)
{
	sb.AppendLine("URL number: " + i);
	sb.AppendLine("Source url : " + metrics.sourceURL);
	sb.AppendLine("Target url : " + metrics.targetURL);
	sb.AppendLine("domain authority % : " + metrics.domainAuthoritySource);
	sb.AppendLine("link flags : " + metrics.linkFlags);
	sb.AppendLine("anchor text : " + metrics.anchorText);
	sb.AppendLine("normalised anchor text : " + metrics.normalisedAnchorText);
	sb.AppendLine("================");
	i++;

Console.WriteLine(sb.ToString());

/*
URL number: 1
Source url : www.huffingtonpost.com / thrillist / the - 16 - best - international_b_6062214.html
Target url : www.quick.fr /
domain authority % : 96,7003753122298
link flags : NoFollow
anchor text: Quick
normalised anchor text : Quick
================
URL number: 2
Source url : www.dmoz.org / World / Fran % C3 % A7ais / Commerce_et_ % C3 % A9conomie / Gastronomie_et_alimentation / Restaurants_et_bars /
Target url: www.quick.fr /
domain authority % : 90,6354758566414
link flags : 
anchor text : < div class="site-title">Quick</div>
normalised anchor text : Quick
================
URL number: 3
Source url : www.lemonde.fr/societe/article/2011/01/24/fermeture-d-un-fast-food-pres-d-avignon-apres-la-mort-suspecte-d-un-adolescent_1469591_3224.html
Target url : groupe.quick.fr/fr/le-groupe/message-quick-avignon-cap-sud
domain authority % : 90,4031713782802
link flags : 
anchor text : site Internet
normalised anchor text : site Internet
================
.......
*/
```
## Useful information

Follow classes maybe usfull for you:

| Class         	| Type          | Use case  							|
| -------------------- 	|:-------------:| -------------------------------------------------------------:|
| URLMetricsContext     | parsed object | Contains all metrics information. Each metrics has description|
| URLMetricsCols      	| enum	        | The Cols parameter uses bit flags to specify which URL metrics Mozscape returns. All and FREE is really useful.|
| LinkMetricsCols 	| enum      	| LinkCols bit flags.			|
| LinkFlags             | enul          | Bit flags referencing to 'fl' from  LinkMetricsCols |





