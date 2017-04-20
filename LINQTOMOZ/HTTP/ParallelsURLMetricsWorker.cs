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


namespace LINQTOMOZ
{
	internal class ParallelsURLMetricsWorker
	{
		List<URLMetrics> _metricsData;
		AuthCalucalation _authentication;

		internal ParallelsURLMetricsWorker(List<URLMetrics> metricsData, AuthCalucalation auth)
		{
			this._metricsData = metricsData;
			this._authentication = auth;
		}

		internal List<URLMetrics> GetMetricsResult()
		{
			List<URLMetricsContext> metricsContexts = GetMetricsContext();

			if (metricsContexts.Count != 0 && this._metricsData.Count != 0)
				return metricsContexts.Select((context) => new URLMetrics
				{
					SearchingURL = this._metricsData.Where(arg => arg.SearchingURL.Replace("/", "") == context.uu.Replace("/", "")).FirstOrDefault() != null ? this._metricsData.Where(arg => arg.SearchingURL.Replace("/", "") == context.uu.Replace("/", "")).FirstOrDefault().SearchingURL : "",
					SourceCols = this._metricsData[0].SourceCols,
					MetricsResult = context
				}).ToList();
			else return new List<URLMetrics>();

		}

		private List<URLMetricsContext> GetMetricsContext()
		{
			List<URLMetricsContext> resultFromServer = new List<URLMetricsContext>();
			if (this._metricsData.Count == 1)
			{
				MozURLRequestWorker<URLMetricsContext> urlRequester = new MozURLRequestWorker<URLMetricsContext>(this._authentication, this._metricsData[0]);
				var executedResult = urlRequester.Execute();
				var finalResult = executedResult.Result;
				resultFromServer.Add(finalResult);

			}
			else if (this._metricsData.Count > 1)
			{
				List<List<URLMetrics>> tenSizeRequests = new List<List<URLMetrics>>();

				for (int i = 0; i < this._metricsData.Count; i += 10)
				{
					tenSizeRequests.Add(this._metricsData.GetRange(i, Math.Min(10, this._metricsData.Count - i)));
				}

				var result = tenSizeRequests.AsParallel().Select((chunk) =>
			   {


				   MozURLsRequestWorker urlRequester = new MozURLsRequestWorker(this._authentication, chunk.ToList<IQueryData>());
				   var executedResult = urlRequester.Execute();

						//Thread.Sleep(1000);
						return executedResult.Result;


			   }).ToList();

				resultFromServer.AddRange(result.SelectMany(x => x).ToList());

			}

			return resultFromServer;

		}

	}
}
