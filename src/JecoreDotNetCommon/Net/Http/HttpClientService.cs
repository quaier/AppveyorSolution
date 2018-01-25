using System;
using System.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using JecoreDotNetCommon.Models;

namespace JecoreDotNetCommon.Net.Http
{
    public class HttpClientService : IHttpClientService, IDisposable
    {
        public string DomainUrl { get; set; }

        public string RequestVerificationToken { get; set; }

        private HttpClient Client { get; set; }

        private const string _errorMessage = "执行请求出错，请联系管理员";

        private const string _header_json = "application/json";

        private const string _header_formdata = "multipart/form-data";

        private const string _header_formurlencoded = "application/x-www-form-urlencoded";

        public HttpClientService()
        {
            // 为了部署方便把SysPrams 中api 访问域名 放到 webconfig domainString字段中
            string domainString = ConfigurationManager.AppSettings["domainString"];
            if (string.IsNullOrEmpty(domainString))
            {
                throw new Exception("domainString未配置");
            }
            DomainUrl = domainString.ToString();
            string requestVerificationToken = ConfigurationManager.AppSettings["requestVerificationToken"];
            if (string.IsNullOrEmpty(requestVerificationToken))
            {
                throw new Exception("requestVerificationToken未配置");
            }
            RequestVerificationToken = requestVerificationToken;
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// 异常统一处理
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<Exception> ExceptiontHandler(HttpResponseMessage response)
        {
            string message = await response.Content.ReadAsStringAsync();
            try
            {
                if (response.Content.Headers.ContentType.MediaType != "text/html")
                {
                    HttpErrorMessage error = await response.Content.ReadAsAsync<HttpErrorMessage>();
                    if (error != null)
                    {
                        return new HttpException((int)response.StatusCode, error.Message);
                    }
                }
                return new HttpException((int)response.StatusCode, message);
            }
            catch (Exception)
            {
                return new HttpException((int)response.StatusCode, message);
            }
        }

        private void SetRequestVerificationToken(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("RequestVerificationToken", RequestVerificationToken);
        }

        public async Task<HttpClientOperationResult> Get(string apiUrl, string token = "", string domainUrl = "")
        {
            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_header_json));

                HttpResponseMessage response = await Client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string results = await response.Content.ReadAsStringAsync();
                    return new HttpClientOperationResult(HttpClientOperationResultType.Success,
                        response.StatusCode.ToString(), "", results);
                }

                throw await ExceptiontHandler(response);
            }
        }

        public async Task<T> Get<T>(string apiUrl, string token = "", string domainUrl = "")
        {
            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_header_json));

                HttpResponseMessage response = await Client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    T results = await response.Content.ReadAsAsync<T>();
                    return results;
                }

                throw await ExceptiontHandler(response);
            }
        }

        public async Task<HttpClientOperationResult> Post(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "")
        {
            string result;

            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_header_json));

                if (httpContent == null)
                {
                    httpContent = new StringContent(data);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(_header_json);
                }

                HttpResponseMessage response = await Client.PostAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return new HttpClientOperationResult(HttpClientOperationResultType.Success,
                        response.StatusCode.ToString(), "", result);
                }

                throw await ExceptiontHandler(response);
            }
        }

        public async Task<T> Post<T>(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "")
        {
            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_header_json));

                if (httpContent == null)
                {
                    httpContent = new StringContent(data);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(_header_json);
                }

                HttpResponseMessage response = await Client.PostAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }

                throw await ExceptiontHandler(response);
            }
        }

        public async Task<HttpClientOperationResult> Put(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "")
        {
            string result;

            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_header_json));

                if (httpContent == null)
                {
                    httpContent = new StringContent(data);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(_header_json);
                }

                HttpResponseMessage response = await Client.PutAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    return new HttpClientOperationResult(HttpClientOperationResultType.Success,
                        response.StatusCode.ToString(), "", result);
                }

                throw await ExceptiontHandler(response);
            }
        }

        public async Task<T> Put<T>(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "")
        {
            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_header_json));

                if (httpContent == null)
                {
                    httpContent = new StringContent(data);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue(_header_json);
                }

                HttpResponseMessage response = await Client.PutAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<T>();
                    return result;
                }

                throw await ExceptiontHandler(response);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="token"></param>
        /// <param name="domainUrl"></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadFile(string apiUrl, string data = "", HttpMethod method = null, string token = "", HttpContent httpContent = null, string domainUrl = "")
        {
            using (Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(!string.IsNullOrEmpty(domainUrl) ? domainUrl : DomainUrl);

                Client.DefaultRequestHeaders.Accept.Clear();

                SetRequestVerificationToken(Client);

                if (!string.IsNullOrEmpty(token))
                {
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                HttpResponseMessage response = new HttpResponseMessage();

                if (method == null || method == HttpMethod.Get)
                {
                    response = await Client.GetAsync(apiUrl);
                }
                else
                {
                    // POST、PUT、DELETE
                    if (httpContent == null)
                    {
                        httpContent = new StringContent(data);
                        httpContent.Headers.ContentType = new MediaTypeHeaderValue(_header_json);
                    }
                    response = await Client.PostAsync(apiUrl, httpContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();

                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();

                    return bytes;
                }

                throw await ExceptiontHandler(response);
            }
        }
    }
}
