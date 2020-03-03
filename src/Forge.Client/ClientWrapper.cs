using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Forge.Client.Models;
using RestSharp;

namespace Forge.Client
{
    public class ClientWrapper : IClientWrapper
    {
        private readonly ForgeClientArgs _args;
        private readonly RestClient _client;
        public string Token { get; private set; } = string.Empty;
        private DateTime? _tokenExpirationDate = null;

        public ClientWrapper(ForgeClientArgs args)
        {
            _args = args;
            _client = new RestClient(args.BaseUrl);
        }

        public async Task RefreshToken()
        {
            var request = new RestRequest("/authentication/v1/authenticate", Method.POST);
            request.AddParameter("client_id", _args.ClientId);
            request.AddParameter("client_secret", _args.ClientSecret);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", _args.Scope);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("x-ads-region", "EMEA");

            var result = await ExecuteRequest<TwoLeggedAuthorizationResult>(request);
            _tokenExpirationDate = DateTime.Now + TimeSpan.FromMinutes(59);
            Token = result.AccessToken;
        }

        public async Task<T> Execute<T>(IRestRequest request) where T : new()
        {
            if (_tokenExpirationDate.GetValueOrDefault(DateTime.MinValue) < DateTime.Now)
                await RefreshToken();

            request.AddHeader("Authorization", $"Bearer {Token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-ads-region", "EMEA");

            return await ExecuteRequest<T>(request);
        }

        public async Task Execute(IRestRequest request)
        {
            if (_tokenExpirationDate.GetValueOrDefault(DateTime.MinValue) < DateTime.Now)
                await RefreshToken();

            request.AddHeader("Authorization", $"Bearer {Token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-ads-region", "EMEA");
            var response = await _client.ExecuteAsync(request);

            if (response.ErrorException == null)
                return;

            const string message = "Error retrieving response.  Check inner details for more info.";
            throw new Exception(message, response.ErrorException);
        }

        private async Task<T> ExecuteRequest<T>(IRestRequest request) where T : new()
        {
            var response = await _client.ExecuteAsync<T>(request);

            if (response.ErrorException == null && response.Data != null)
                return response.Data;
            if (response.ErrorException is null && !string.IsNullOrEmpty(response.Content))
                throw new Exception(response.Content);
            const string message = "Error retrieving response.  Check inner details for more info.";
            throw new Exception(message, response.ErrorException);
        }
    }
}
