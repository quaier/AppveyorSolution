﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Data
{
    public static class DataConvert
    {
        #region 全角转换半角以及半角转换为全角  
        ///转全角的函数(SBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248  
        public static string ToSBC(string input)
        {
            // 半角转全角：  
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127)
                {
                    array[i] = (char)(array[i] + 65248);
                }
            }
            return new string(array);
        }

        ///转半角的函数(DBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248//   
        public static string ToDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }
        #endregion

        /// <summary>
        /// 序列化URL参数
        /// </summary>
        /// <param name="parametersObj"></param>
        /// <returns></returns>
        public static string SerializeUrlParameters(dynamic parametersObj)
        {
            if (parametersObj != null)
            {
                var array = new List<string>();
                foreach (var v in parametersObj)
                {
                    if (v != null)
                    {
                        array.Add(v.Name.ToString() + "=" + v.Value.ToString());
                    }
                }
                var arrayStr = string.Join("&", array.ToArray());
                if (arrayStr.Length > 0)
                {
                    return "?" + arrayStr;
                }
            }
            return "";
        }
    }
}
