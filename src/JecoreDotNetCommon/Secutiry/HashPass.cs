using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Secutiry
{
    public class HashPass
    {
        /// <summary>
        /// SHA,SHA1,MD5,SHA256,SHA256Managed,SHA-256,SHA384,SHA384Managed,SHA512,SHA512Managed,
        /// 新加密类
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="hashName"></param>
        /// <returns></returns>
        public static string HashString(string inputString, string hashName)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create(hashName);
            if (algorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name", "hashName");
            }
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            return Convert.ToBase64String(hash);
        }

        public static string HashFileMd5(Stream fileStream)
        {
            using (var md5 = MD5.Create())
            {

                byte[] bytes = md5.ComputeHash(fileStream);
                return BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", "").ToLower();
            }
        }


    }
}
