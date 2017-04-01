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
MOZService service = new MOZService("your access if","your security key");
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
				MOZService service = new MOZService("your access if","your security key");
				var urlMetrics = service.QueryURLMetrics();


				var result = urlMetrics.Where((arg) => sites.Contains(arg.SearchingURL) && arg.SourceCols == URLMetricsCols.FREE)
									   .Select(x => new { equityLinkNumber = x.MetricsResult.ueid, cononicalURL = x.MetricsResult.uu })
									   .OrderByDescending(x => x.equityLinkNumber);

				foreach (var webSite in result)
				{
					Console.WriteLine("For: " + webSite.cononicalURL + " was found: " + webSite.equityLinkNumber + " external equity  links");

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

