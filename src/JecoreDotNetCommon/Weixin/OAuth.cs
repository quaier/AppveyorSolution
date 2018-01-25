using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Weixin
{
    public static class OAuth
    {
        /// <summary>
        /// 使用access_token获取用户信息
        /// </summary>
        /// <param name="accessToken">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <returns></returns>
        public static async Task<UserInfo> GetUserInfo(string accessToken, string openId)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(
                "https://api.weixin.qq.com/sns/userinfo?access_token" + accessToken + "&openId=" + openId);
            if (response.IsSuccessStatusCode)
            {
                string str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserInfo>(str);
            }
            return new UserInfo { };
        }

        /// <summary>
        /// 用户授权并获取code
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="redirectUri"></param>
        /// <param name="responseType"></param>
        /// <param name="state"></param>
        /// <param name="scope"></param>
        /// <param name="wechat_redirect"></param>
        /// <returns></returns>
        public static string GetAuthorize(string appId, string redirectUri, string responseType = "code", string state = "", Scope scope = Scope.snsapi_base, bool wechat_redirect = false)
        {
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appId + "&redirect_uri=" + redirectUri + "&response_type=" + responseType + "&scope=" + scope.ToString() + "&state=" + state + "#wechat_redirect";
        }

        /// <summary>
        /// 使用code换取access_token和openid
        /// </summary>
        /// <param name="appId">appId</param>
        /// <param name="secret">appKey</param>
        /// <param name="code">code</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static async Task<GetAccessTokenResponse> GetAccessToken(string appId, string secret, string code, GrantType grantType = GrantType.authorization_code)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(
                "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appId +
                "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code" + grantType.ToString());
            if (response.IsSuccessStatusCode)
            {
                string str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetAccessTokenResponse>(str);
            }
            return new GetAccessTokenResponse { };
        }
    }
}
