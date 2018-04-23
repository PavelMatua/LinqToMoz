
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
					throw new StandartHTTPExceptions("Forbidden. The API Key was not supplied.");
				case HttpStatusCode.InternalServerError:
					throw new StandartHTTPExceptions("Server Error.");
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



