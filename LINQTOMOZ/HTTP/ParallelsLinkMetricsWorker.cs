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
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LINQTOMOZ
{
	internal class ParallelsLinkMetricsWorker 
	{
		private int limits = 0;
		private int offsets = 0;
		private List<LinkMetrics> _metrics;
		private AuthCalucalation _auth;
		internal ParallelsLinkMetricsWorker(List<LinkMetrics> metrics, AuthCalucalation auth, int limits, int offsets)
		{
			this.limits = limits;
			this.offsets = offsets;
			this._metrics = metrics;
			this._auth = auth;
		}

		internal List<LinkMetrics> GetMetricsResult()
		{
			List<LinkMetrics> metricsContexts = GetMetricsContext();

			if (metricsContexts.Count != 0 && this._metrics.Count != 0)
				return metricsContexts;
			else return new List<LinkMetrics>();

		}

		private List<LinkMetrics> GetMetricsContext()
		{
			List<LinkMetrics> resultFromServer = new List<LinkMetrics>();

			List<Task<List<URLMetricsContext>>> tasksPerChunk;
			foreach (var lnkMetrics in _metrics)
			{
				List<URLMetricsContext> urlMetricsContext = null;
				tasksPerChunk = new List<Task<List<URLMetricsContext>>>();
				// Create Limits logic here:
				if (limits > 10)
				{
					for (int i = 10; i < limits + 10; i += 10)
					{
						MozURLRequestWorker<List<URLMetricsContext>> urlRequesterTen;
						if (limits - i > 0)
						{
							urlRequesterTen = new MozURLRequestWorker<List<URLMetricsContext>>(this._auth, lnkMetrics, 10,i-10);

						}
						else
						{
							int lastNumber = limits - (i - 10);
							urlRequesterTen = new MozURLRequestWorker<List<URLMetricsContext>>(this._auth, lnkMetrics, lastNumber,i-10);
						}

						tasksPerChunk.Add(urlRequesterTen.Execute());


					}


				}
				else
				{
					MozURLRequestWorker<List<URLMetricsContext>> urlRequester = new MozURLRequestWorker<List<URLMetricsContext>>(this._auth, lnkMetrics, limits);
					tasksPerChunk.Add(urlRequester.Execute());
				}



					// Run tasks in Parallel
					var finalResult = tasksPerChunk.AsParallel().Select((thrd) => thrd.Result).SelectMany(x => x).ToList();
				
		
				foreach (var result in finalResult)
				{
					LinkMetrics linkMetrics = new LinkMetrics();
					linkMetrics.SearchingURL = HttpUtility.UrlDecode(lnkMetrics.SearchingURL);
					linkMetrics.MetricsResult = result;
					linkMetrics.Filters = lnkMetrics.Filters;
					linkMetrics.LinkCols = lnkMetrics.LinkCols;
					linkMetrics.Scope = lnkMetrics.Scope;
					linkMetrics.Sort = lnkMetrics.Sort;
					linkMetrics.SourceCols = lnkMetrics.SourceCols;
					linkMetrics.SourceDomainURL = lnkMetrics.SourceDomainURL;
					linkMetrics.TargetCols = lnkMetrics.TargetCols;

					resultFromServer.Add(linkMetrics);
				}
					
					
			}

			return resultFromServer;


		}




	}
}
