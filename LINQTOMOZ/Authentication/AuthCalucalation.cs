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
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace LINQTOMOZ
{
	public class AuthCalucalation : MozAuth
	{
		private string experationEpochTime = String.Empty; 

		public AuthCalucalation (String aplicationId,String securityKey, int timeRation = 5) : 
			base(aplicationId,securityKey,timeRation)
		{
			
		}

		public String GetCurrentURLAuth()
		{
			String signature = GetSignatureCode ();

			if (signature == null) {
				throw new Exception("Signature can't be generated. Check your AccessId or try again");
			}

			String URL = String.Format ("AccessID={0}&Expires={1}&Signature={2}",this.AplicationID,experationEpochTime,signature);

			return URL;
		}

		private String GetSignatureCode()
		{
			String resultFormated = String.Empty;
			// Getting epoch time
			experationEpochTime = DateTimeEpochExtensions.ToEpochTime(DateTime.Now.AddMinutes(this.TimeRation)).ToString();

			// Convert Secret to byte 

			byte[] securityByteCode = Encoding.ASCII.GetBytes(this.SecurityKey);


			using (HMACSHA1 hmac = new HMACSHA1(securityByteCode))
			{
				String dataSecret = this.AplicationID + "\n" + experationEpochTime;

				byte[] byteAppId = Encoding.ASCII.GetBytes(dataSecret);

				var hmacHash = hmac.ComputeHash(byteAppId);

				String signatureNonFormated = Convert.ToBase64String(hmacHash);

				resultFormated = HttpUtility.UrlEncode (signatureNonFormated);


			}


			return resultFormated;


		}

	}
}

