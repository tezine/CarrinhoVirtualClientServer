#region Imports
using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Codes;
using SharedLib.Entities;
#endregion

namespace Frontend.Services {
	public class SAdminUsersBlazorService {
		HttpClient httpClient;

		public SAdminUsersBlazorService(HttpClient client) {
			httpClient = client;
		}

		public async Task<EAdminUser> GetByID(string id)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"api/v1/SAdminUsers/GetByID/{id}");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			EAdminUser result = JsonConvert.DeserializeObject<EAdminUser>(responseBody);
			return result;
		}

		public async Task<EAdminUser> Authenticate(EAdminUser eAdminUser)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"api/v1/SAdminUsers/Authenticate");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			req.Content = new StringContent(JsonConvert.SerializeObject(eAdminUser), Encoding.UTF8, "application/json");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			EAdminUser result = JsonConvert.DeserializeObject<EAdminUser>(responseBody);
			return result;
		}

		public async Task<string> Save(EAdminUser eAdminUser)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, $"api/v1/SAdminUsers/Save");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			req.Content = new StringContent(JsonConvert.SerializeObject(eAdminUser), Encoding.UTF8, "application/json");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return responseBody;
		}

		public async Task<bool> Remove(string id)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Delete, $"api/v1/SAdminUsers/Remove/{id}");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return bool.Parse(responseBody);
		}

	}
}
