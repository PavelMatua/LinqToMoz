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
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace LINQTOMOZ
{
	internal class MozURLRequestWorker<TResult> : RequestWorker<TResult> where TResult : class
	{

		//private IQueryData _queryData;
		private String finalURL;
		private int _limit;
		private int _offset;
		public MozURLRequestWorker(int retry, AuthCalucalation auth, IQueryData queryData) : base(retry, auth)
		{
			//this._queryData = queryData;
			this.finalURL = this.GenerateURL(queryData);

		}

		public MozURLRequestWorker(AuthCalucalation auth, IQueryData queryData, int limit = 10, int offset = 0) : base(5, auth)
		{
			//this._queryData = queryData;
			this._limit = limit;
			this._offset = offset;
			this.finalURL = this.GenerateURL(queryData);

		}


		protected override Func<IQueryData, string> Query
		{
			get
			{
				return (qr) =>
				{
					if (qr.GetType() == typeof(URLMetrics))
					{
						URLMetrics urlRequester = (URLMetrics)qr;
						int metricsCalsValue = (int)urlRequester.SourceCols;
						String urlQuery = "url-metrics/" + urlRequester.SearchingURL.ToString() + "?Cols=" + metricsCalsValue.ToString() + "&";
						return urlQuery;
					}
					else
					{
						LinkMetrics linkMetrics = (LinkMetrics)qr;

						StringBuilder urlBuilder = new StringBuilder();

						urlBuilder.Append("links/" + linkMetrics.SearchingURL + "?Scope=" + linkMetrics.Scope);
						urlBuilder.Append("&Sort=" + linkMetrics.Sort);
						urlBuilder.Append(linkMetrics.SourceCols != 0 ? "&SourceCols=" + linkMetrics.SourceCols.ToString() : "");
						urlBuilder.Append(linkMetrics.TargetCols != 0 ? "&TargetCols=" + linkMetrics.TargetCols.ToString() : "");
						urlBuilder.Append(linkMetrics.LinkCols != 0 ? "&LinkCols=" + linkMetrics.LinkCols.ToString() : "");
						urlBuilder.Append(linkMetrics.SourceDomainURL != null ? "&SourceDomain=" + linkMetrics.SourceDomainURL : "");
						urlBuilder.Append(_limit != 0 ? "&Limit=" + _limit + "&Offset=" + _offset : "");

						// Don't forget to add a filters

						return urlBuilder.ToString();

					}

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
						return client.GetAsync(this.finalURL);
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
