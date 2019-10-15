using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WrightWayRestaurant.Framework.Utility
{
    public class EncryptAndDecrypt
    {
        private static Encoding encoding = Encoding.GetEncoding(936);      //GBK 编码格式（指定编码格式），可以传输中文

        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };


        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey = null)
        {
            try
            {
                byte[] rgbKey = !string.IsNullOrEmpty(encryptKey) ? encoding.GetBytes(encryptKey.Substring(0, 8)) : new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
                byte[] rgbIV = Keys;
                byte[] inputByteArray = encoding.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey = null)
        {
            try
            {
                byte[] rgbKey = !string.IsNullOrEmpty(decryptKey) ? encoding.GetBytes(decryptKey) : new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return encoding.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string EncryptMD5(string encryptString)
        {
            string password = string.Empty;
            if (!string.IsNullOrEmpty(encryptString))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bs = Encoding.UTF8.GetBytes(encryptString);
                bs = md5.ComputeHash(bs);
                StringBuilder s = new StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToUpper());
                }
                password = s.ToString();
            }
            return password;
        }
    }
}
