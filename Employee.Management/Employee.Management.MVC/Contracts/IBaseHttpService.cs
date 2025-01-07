using Employee.Management.MVC.Models.Common;

namespace Employee.Management.MVC.Contracts
{
    public interface IBaseHttpService
    {
        public HttpClient _httpClient { get; set; }
        public bool includeToken { get; set; }
        void AddBearerToken();
        Task<GenericResponse<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class where TResponse : class;
        Task<GenericResponse<object>> PostAsync<TRequest>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class;
        Task<GenericResponse<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class where TResponse : class;
        Task<GenericResponse<object>> PutAsync<TRequest>(string endpoint, TRequest requestBody, CancellationToken cancellationToken) where TRequest : class;
        Task<GenericResponse<TResponse>> DeleteAsync<TResponse>(string endpoint, CancellationToken cancellationToken) where TResponse : class;
        Task<GenericResponse<object>> DeleteAsync(string endpoint, CancellationToken cancellationToken);
        Task<GenericResponse<TResponse>> GetAsync<TResponse>(string endpoint, CancellationToken cancellationToken) where TResponse : class;
    }
}
