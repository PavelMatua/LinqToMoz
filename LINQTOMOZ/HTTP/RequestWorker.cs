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
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LINQTOMOZ
{
	internal abstract class RequestWorker<THttpResponse> : HttpWorker<THttpResponse> where THttpResponse : class
	{

		protected abstract Func<IQueryData, String> Query { get; }

		private AuthCalucalation authentication { get; set; }


		public RequestWorker(int retry, AuthCalucalation authCalc)
			: base(retry)
		{
			authentication = authCalc;
		}

		protected String GenerateURL(IQueryData queryData)
		{

			String authPart = authentication.GetCurrentURLAuth();

			String searchingPart = Query(queryData);

			return "http://lsapi.seomoz.com/linkscape/" + searchingPart + "&" + authPart;

		}




		protected override async Task<THttpResponse> Response(System.Net.Http.HttpResponseMessage httpResponseMessage)
		{
			switch ((int)httpResponseMessage.StatusCode)
			{
				case (int)HttpStatusCode.OK:
					var content = await httpResponseMessage.Content.ReadAsStringAsync();
					var response = JsonConvert.DeserializeObject<THttpResponse>(content);
					return response;
					break;
				case (int)HTTPStatusMOZ.BinaryContent:
					throw new CustomMOZException("Binary Content");
					break;
				case (int)HTTPStatusMOZ.BlockedByRequest:
					throw new CustomMOZException("Blocked by request");
					break;
				case (int)HTTPStatusMOZ.BlockedByRobotsTXT:
					throw new CustomMOZException("Blocked by robots.txt");
					break;
				case (int)HTTPStatusMOZ.CurrentlyUnused:
					throw new CustomMOZException("Currently unused");
					break;
				case (int)HTTPStatusMOZ.FailedToFetchRobotsTXT:
					throw new CustomMOZException("Failed to fetch robots.txt");
					break;
				case (int)HTTPStatusMOZ.InternalStatusCodeToCrawler:
					throw new CustomMOZException("Internal status code to crawler");
					break;
				case (int)HTTPStatusMOZ.InvalidHTTPstatusCode:
					throw new CustomMOZException("Invalid HTTP status code");
					break;
				case (int)HTTPStatusMOZ.NetworkError:
					throw new CustomMOZException("Network error");
					break;
				case (int)HTTPStatusMOZ.NotCrawled:
					throw new CustomMOZException("Not Crawled");
					break;
				case (int)HTTPStatusMOZ.PageMetaNonIndexedTag:
					throw new CustomMOZException("Page has a meta no-index tag");
					break;
				case (int)HTTPStatusMOZ.PageTooBig:
					throw new CustomMOZException("Page too big");
					break;
				case (int)HTTPStatusMOZ.TranscodeFailure:
					throw new CustomMOZException("Transcode failure (failure detecting content type)");
					break;
				default:
					return await base.Response(httpResponseMessage);

			}
			return await base.Response(httpResponseMessage);
		}




	}
}

