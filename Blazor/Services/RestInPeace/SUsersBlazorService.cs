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
	public class SUsersBlazorService {
		HttpClient httpClient;

		public SUsersBlazorService(HttpClient client) {
			httpClient = client;
		}

		public async Task<List<EUser>> GetAll(string companyID, int listCount=-1, int pageNumber=0, string orderBy="id desc")  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"api/v1/SUsers/GetAll/{companyID}?listCount={listCount}&pageNumber={pageNumber}&orderBy={orderBy}");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			List<EUser> result = JsonConvert.DeserializeObject<List<EUser>>(responseBody);
			return result;
		}

		public async Task<EUser> GetByID(string id)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"api/v1/SUsers/GetByID/{id}");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			EUser result = JsonConvert.DeserializeObject<EUser>(responseBody);
			return result;
		}

		public async Task<EUser> Authenticate(EUser eUser)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"api/v1/SUsers/Authenticate");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			req.Content = new StringContent(JsonConvert.SerializeObject(eUser), Encoding.UTF8, "application/json");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			EUser result = JsonConvert.DeserializeObject<EUser>(responseBody);
			return result;
		}

		public async Task<string> Save(EUser eClient)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, $"api/v1/SUsers/Save");
			req.Headers.Add("Authorization", $"bearer {ClientDefines.Token}");
			req.Content = new StringContent(JsonConvert.SerializeObject(eClient), Encoding.UTF8, "application/json");
			var response = await httpClient.SendAsync(req);
			ClientDefines.Loading = false;
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)throw new UnauthorizedAccessException();
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return responseBody;
		}

		public async Task<bool> Remove(string id)  {
			ClientDefines.Loading = true;
			HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Delete, $"api/v1/SUsers/Remove/{id}");
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
