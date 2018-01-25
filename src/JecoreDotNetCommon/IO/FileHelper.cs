using System;
using System.IO;
using System.Threading.Tasks;
using JecoreDotNetCommon.JSON;

namespace JecoreDotNetCommon.IO
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 往指定文件 写入字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void Write(string filePath, string data, bool append = true)
        {
            // 指定的文件是否存在
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            using (StreamWriter sw = new StreamWriter(filePath, append))
            {
                try
                {
                    sw.Write(data);

                    sw.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        /// <summary>
        /// 往指定文件 写入字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void WriteLine(string filePath, string data, bool append = true)
        {
            // 指定的文件是否存在
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            using (StreamWriter sw = new StreamWriter(filePath, append))
            {
                try
                {
                    sw.WriteLine(data);

                    sw.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        /// <summary>
        /// 往指定文件 写入模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void Write<T>(string filePath, T data, bool append = true)
        {
            // 指定的文件是否存在
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            using (StreamWriter sw = new StreamWriter(filePath, append))
            {
                try
                {
                    var dataStr = data.SerializeObject();

                    sw.Write(dataStr);

                    sw.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        /// <summary>
        /// 往指定文件 写入模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void WriteLine<T>(string filePath, T data, bool append = true)
        {
            // 指定的文件是否存在
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            using (StreamWriter sw = new StreamWriter(filePath, append))
            {
                try
                {
                    var dataStr = data.SerializeObject();

                    sw.WriteLine(dataStr);

                    sw.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        /// <summary>
        /// 读取指定文件的所有内容 返回字符串形式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadToEnd(string filePath)
        {
            // 文件不存在时 throw exception
            if (!File.Exists(filePath))
            {
                throw new Exception("指定的文件不存在");
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                try
                {
                    string data = sr.ReadToEnd();

                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        /// <summary>
        /// 异步读取指定文件的所有内容 返回字符串形式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> ReadToEndAsync(string filePath)
        {
            // 文件不存在时 throw exception
            if (!File.Exists(filePath))
            {
                throw new Exception("指定的文件不存在");
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                try
                {
                    string data = await sr.ReadToEndAsync();

                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        /// <summary>
        /// 读取指定文件的所有内容 返回指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadToEnd<T>(string filePath)
        {
            // 文件不存在时 throw exception
            if (!File.Exists(filePath))
            {
                throw new Exception("指定的文件不存在");
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                try
                {
                    string dataStr = sr.ReadToEnd();

                    // 对读取出的Json.net的reader流进行反序列化，并装载到模型中  
                    T data = dataStr.DeserializeObject<T>();

                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        /// <summary>
        /// 异步读取指定文件的所有内容 返回指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<T> ReadToEndAsync<T>(string filePath)
        {
            // 文件不存在时 throw exception
            if (!File.Exists(filePath))
            {
                throw new Exception("指定的文件不存在");
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                try
                {
                    string dataStr = await sr.ReadToEndAsync();

                    // 对读取出的Json.net的reader流进行反序列化，并装载到模型中  
                    T data = dataStr.DeserializeObject<T>();

                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
    }
}
