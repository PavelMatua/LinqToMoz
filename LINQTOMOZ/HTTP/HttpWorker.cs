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
using System.Net;

namespace LINQTOMOZ
{
	internal abstract class HttpWorker<THttpResponse>
	{
		protected abstract Func<HttpClient, Task<HttpResponseMessage>> HttpRequest { get; }
		private int retry = 0;
		protected HttpWorker(int retry = 2)
		{
			this.retry = retry;
		}

		public async Task<THttpResponse> Execute()
		{
			return await Task.Run(async () =>
			{
				while (true)
				{
					try
					{
						using (var client = new HttpClient())
						{
							client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
							client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
							var responseMessage = await HttpRequest(client);
							return await Response(responseMessage);
						}

					}
					catch (Exception e)
					{
						if (retry == 0)
						{
							throw;
						}
						retry--;
					}
				}
			});
		}

		protected virtual Task<THttpResponse> Response(HttpResponseMessage httpResponseMessage)
		{
           
			switch (httpResponseMessage.StatusCode)
			{
				case HttpStatusCode.BadRequest:
					throw new StandartHTTPExceptions("Bad Request");
				case HttpStatusCode.Forbidden:
					throw new StandartHTTPExceptions("Forbidden – The API Key was not supplied, or it was invalid, or it is not authorized to access the service.");
				case HttpStatusCode.InternalServerError:
					throw new StandartHTTPExceptions("Server Error – An internal server error has occurred which has been logged.");
				case HttpStatusCode.MultipleChoices:
					throw new StandartHTTPExceptions("Redirection: Request has been received, but needs to perform an additional step to complete the request.");
                case (HttpStatusCode)429:
                    throw new InvalidQueryException("Permission denied. Probably you are performing too match thread per request. " + httpResponseMessage.ReasonPhrase);
				default:
					throw new NotSupportedException(
						"Status code {(int)httpResponseMessage.StatusCode} returned by the server is not supported");
			}
		}
	}
}



