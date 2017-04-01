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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LINQTOMOZ
{
	internal class MozURLsRequestWorker : RequestWorker<List<URLMetricsContext>>
	{

		private List<IQueryData> _queryData;
		private String finalURL;
		internal MozURLsRequestWorker(int retry, AuthCalucalation auth, List<IQueryData> queryData) : base(retry,auth)
		{
			this._queryData = queryData;


		}

		internal MozURLsRequestWorker(AuthCalucalation auth, List<IQueryData> queryData) : base(5, auth)
		{
			this._queryData = queryData;
			this.finalURL = this.GenerateURL(queryData[0]);
		}


		protected override Func<IQueryData, string> Query
		{
			get
			{
				return (qr) =>
				{
					URLMetrics urlRequester = (URLMetrics)_queryData[0];
					int metricsCalsValue = (int)urlRequester.SourceCols;
					String urlQuery = "url-metrics/?Cols=" + metricsCalsValue.ToString() + "&";
					return urlQuery;

				};
			}
		}



		protected override Func<HttpClient, Task<HttpResponseMessage>> HttpRequest
		{
			get
			{
				return (client) =>
				{
					if (this.finalURL != String.Empty)
					{
						List<String> searchingUrlLst = new List<string>();
						foreach (var url in _queryData)
						{
							URLMetrics metricsUrl = (URLMetrics)url;
							searchingUrlLst.Add(metricsUrl.SearchingURL);
						}
						var content = new StringContent(JsonConvert.SerializeObject(searchingUrlLst.ToArray()), Encoding.UTF8, "application/json");
						return client.PostAsync(this.finalURL, content);
					}
					else
					{
						throw new Exception("URL was not initialised");
					}

				};

			}
		}


	}
}