#region Imports
using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SharedLib.Entities;
using Frontend.Codes;
#endregion

namespace Frontend.Services {
	public class SProductsBlazorService {
		HttpClient httpClient;

		public SProductsBlazorService(HttpClient client) {
			httpClient = client;
		}

		public async Task<List<EProduct>> GetAll(string companyID, int listCount=-1, int pageNumber=0, string orderBy="id desc")  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"api/v1/SProducts/GetAll/{companyID}?listCount={listCount}&pageNumber={pageNumber}&orderBy={orderBy}");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			List<EProduct> result = JsonConvert.DeserializeObject<List<EProduct>>(responseBody);
			return result;
		}

	}
}
