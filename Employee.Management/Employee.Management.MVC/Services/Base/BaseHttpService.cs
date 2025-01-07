using Employee.Management.MVC.Contracts;
using Employee.Management.MVC.Models.Common;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Employee.Management.MVC.Services.Base
{
    public class BaseHttpService : IBaseHttpService
    {
        protected readonly ILocalStorageService _localStorage;
        public HttpClient _httpClient { get; set; }
        public bool includeToken { get; set; }

        public BaseHttpService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;

            _localStorage = localStorageService;
        }

        public async Task<GenericResponse<TResponse>> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken) where TResponse : class
        {
            GenericResponse<TResponse> response = new();

            try
            {

                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.GetAsync(endpoint, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;
                }

                string httpContent = await httpResponse.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<GenericResponse<TResponse>>(httpContent);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<GenericResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class where TResponse : class
        {
            GenericResponse<TResponse> response = new();

            try
            {
                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.PostAsJsonAsync(endpoint, requestBody, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;
                }

                string httpContent = await httpResponse.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<GenericResponse<TResponse>>(httpContent);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<GenericResponse<object>> PostAsync<TRequest>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class
        {
            GenericResponse<object> response = new();

            try
            {
                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.PostAsJsonAsync(endpoint, requestBody, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;
                }
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<GenericResponse<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class where TResponse : class
        {
            GenericResponse<TResponse> response = new();

            try
            {
                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.PutAsJsonAsync(endpoint, requestBody, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;
                }

                string httpContent = await httpResponse.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<GenericResponse<TResponse>>(httpContent);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<GenericResponse<object>> PutAsync<TRequest>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class
        {
            GenericResponse<object> response = new();

            try
            {
                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.PutAsJsonAsync(endpoint, requestBody, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;

                    string ed = await httpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<GenericResponse<TResponse>> DeleteAsync<TResponse>(string endpoint, CancellationToken cancellationToken) where TResponse : class
        {
            GenericResponse<TResponse> response = new();

            try
            {
                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.DeleteAsync(endpoint, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;
                }

                string httpContent = await httpResponse.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<GenericResponse<TResponse>>(httpContent);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<GenericResponse<object>> DeleteAsync(string endpoint, CancellationToken cancellationToken)
        {
            GenericResponse<object> response = new();

            try
            {
                if (includeToken)
                    AddBearerToken();

                var httpResponse = await _httpClient.DeleteAsync(endpoint, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = httpResponse.ReasonPhrase;
                }
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public void AddBearerToken()
        {
            if (_localStorage.Exists("token"))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
        }
    }
}
