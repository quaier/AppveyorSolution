using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using JecoreDotNetCommon.Models;
using System.ComponentModel;

namespace JecoreDotNetCommon.Net.Http
{
    public interface IHttpClientService
    {
        /// <summary>
        /// 请求的host
        /// </summary>
        string DomainUrl { set; get; }

        string RequestVerificationToken { set; get; }

        Task<HttpClientOperationResult> Get(string apiUrl, string token = "", string domainUrl = "");

        Task<T> Get<T>(string apiUrl, string token = "", string domainUrl = "");

        Task<HttpClientOperationResult> Post(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "");

        Task<T> Post<T>(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "");

        Task<HttpClientOperationResult> Put(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "");

        Task<T> Put<T>(string apiUrl, string data, string token = "", HttpContent httpContent = null, string domainUrl = "");

        Task<byte[]> DownloadFile(string apiUrl, string data = "", HttpMethod method = null, string token = "", HttpContent httpContent = null, string domainUrl = "");
    }
}
