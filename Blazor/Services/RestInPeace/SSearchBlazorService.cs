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
	public class SSearchBlazorService {
		HttpClient httpClient;

		public SSearchBlazorService(HttpClient client) {
			httpClient = client;
		}

		public async Task<List<EProduct>> Search(ESearch eSearch)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"api/v1/SSearch/Search");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			req.Content = new StringContent(JsonConvert.SerializeObject(eSearch), Encoding.UTF8, "application/json");
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
