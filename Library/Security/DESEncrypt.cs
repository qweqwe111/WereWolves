/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Library
{
    /// <summary>
    /// DES加密、解密帮助类
    /// </summary>
    public class DESEncrypt
    {
        private static string DESKey = "nfine_desencrypt_2016";

        #region ========加密========
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, DESKey);
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                return Decrypt(Text, DESKey);
            }
            else
            {
                return "";
            }
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="srcBytes"></param>
        /// <returns></returns>
        public byte[] EncryptNew(Byte[] srcBytes)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            MemoryStream ms = new MemoryStream();
            ICryptoTransform ssd = SymmetricAlgorithm.Create().CreateEncryptor(utf8.GetBytes(DESKey), utf8.GetBytes(DESKey));
            CryptoStream encStream = new CryptoStream(ms, ssd, CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(encStream);

            foreach (byte tempByte in srcBytes)
            {
                sw.WriteLine(tempByte);
            }

            #region 退出释放资源
            sw.Close();
            byte[] buffer = ms.ToArray();
            encStream.Close();
            ms.Close();
            #endregion

            return buffer;
        }

        /// <summary>
        /// 返回一个加密后的字符串
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public string EncryptNew(string src)
        {
            if (src == null) return null;
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] srcBytes = utf8.GetBytes(src);

            byte[] buffer = EncryptNew(srcBytes);

            StringBuilder cipherTextSb = new StringBuilder();
            foreach (byte tempCipherText in buffer)
            {
                cipherTextSb.AppendFormat("{0}", tempCipherText.ToString("x2"));
            }

            return cipherTextSb.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public byte[] DecryptNew(byte[] src)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            MemoryStream ms = new MemoryStream(src);
            ICryptoTransform ssd = SymmetricAlgorithm.Create().CreateDecryptor(utf8.GetBytes(DESKey), utf8.GetBytes(DESKey));
            CryptoStream encStream = new CryptoStream(ms, ssd, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(encStream);

            #region 把密文转化为List<byte>
            string val = sr.ReadToEnd();
            List<byte> srcByteList = new List<byte>();
            while (true)
            {
                int i = val.IndexOf("\n");
                if (i > 0)
                {
                    srcByteList.Add(Convert.ToByte(val.Substring(0, i)));
                    val = val.Remove(0, i + 1);
                }
                else
                    break;
            }
            if (val.Trim().Length > 0)
                srcByteList.Add(Convert.ToByte(val.Substring(0)));
            #endregion

            #region 把List<byte> 转换为 byte[]
            byte[] srcBytes = new byte[srcByteList.Count];
            for (int i = 0; i < srcByteList.Count; i++)
            {
                srcBytes[i] = srcByteList[i];
            }
            #endregion

            #region 退出,并释放资源
            sr.Close();
            encStream.Close();
            ms.Close();
            #endregion

            return srcBytes;
        }

        /// <summary>
        /// 解密,返回一个解密后的字符串
        /// </summary>
        /// <param name="src">
        /// 这个密文是由EncryptToString方法生成的</param>
        /// <returns></returns>
        public string DecryptNew(string src)
        {
            if (src == null) return null;
            System.Text.UTF8Encoding utf8 = new UTF8Encoding();

            byte[] srcBytes = new byte[src.Length / 2];

            for (int idx = 0, i = 0; i < src.Length - 1; idx++)
            {
                srcBytes[idx] = Convert.ToByte(int.Parse(src.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                i += 2;
            }

            Byte[] bytes = DecryptNew(srcBytes);

            Char[] srcChars = new char[bytes.Length];
            srcChars = utf8.GetChars(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (char tempChar in srcChars)
            {
                sb.Append(tempChar);
            }
            return sb.ToString();
        }
    }
}
