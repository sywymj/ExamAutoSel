using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.Security;
using System.Net;

using System.Threading;

namespace WebQQ.Util
{
    public class CommonClass
    {
        private static Random myRandom = new Random();
        private Encoding pageEncode = Encoding.Default;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string getACSRFToken(string skey)
        {
            string token = string.Empty;
            UInt32 hash = 5381;
            for (int i = 0; i < skey.Length; i++)
            {
                hash += (hash << 5) + Convert.ToUInt32(skey[i]);
            }
            hash = hash & 0x7fffffff;

            token = hash.ToString();
            return token;
        }
        public static String GetRandMd5Val()
        {
            return GetMD5str(myRandom.NextDouble().ToString());
        }

        public static string GetSha1Str(byte[] buf)
        {
            string hr = string.Empty;
            SHA1 sha = new SHA1CryptoServiceProvider();
            try
            {
                byte[] buffer = sha.ComputeHash(buf);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    builder.Append(buffer[i].ToString("x2"));
                }
                hr = builder.ToString();
            }
            catch (System.Exception e)
            {
                hr = null;
            }
            return hr;
        }

        public static string md5_3(string input)
        {
            MD5 md = MD5.Create();
            byte[] buffer = md.ComputeHash(Encoding.Default.GetBytes(input));
            buffer = md.ComputeHash(buffer);
            buffer = md.ComputeHash(buffer);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            } return builder.ToString();
        }

        public static byte[] GetMD5str(string input, string s)
        {
            MD5 md = MD5.Create();
            byte[] buffer = md.ComputeHash(Encoding.Default.GetBytes(input));
            //byte[] buffer = md.ComputeHash(Encoding.UTF8.GetBytes(input));
            //buffer = md.ComputeHash(buffer);
            return buffer;
        }
        public static string GetMD5str(Byte[] buffer)
        {
            MD5 md = MD5.Create();
            buffer = md.ComputeHash(buffer);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            } return builder.ToString();
        }

        public static string QQPassEncry(string password,string account,string verifyCode)
        {
            byte[] buf1 = GetMD5str(password, string.Empty);
            byte[] uinByte = BitConverter.GetBytes(Convert.ToInt64(account));

            byte[] buf2 = new byte[24];
            buf1.CopyTo(buf2, 0);
            for (int i = 0; i < 8; i++)
            {
                buf2[16 + i] = uinByte[7 - i];
            }
            string encryPass = GetMD5str(GetMD5str(buf2).ToUpper() + verifyCode.ToUpper());

            return encryPass;
        }

        public static string GetMD5str(string _str)
        {
            return (FormsAuthentication.HashPasswordForStoringInConfigFile(_str.Trim(), "MD5"));
        }

        public static List<KeyValuePair<string,string>> ImportAccount(string fileName,string spareStr)
        {
            List<KeyValuePair<string, string>> hrLs = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(spareStr))
            {
                spareStr = "----";
            }

            try
            {
                string lines = string.Empty;
                using(StreamReader sr=new StreamReader(fileName))
                {
                    lines = sr.ReadToEnd();
                    sr.Close();
                }
                string pattern=string.Format(@"(?<name>\S+?){0}(?<val>\S+)",spareStr);

                MatchCollection mc = Regex.Matches(lines, pattern, RegexOptions.IgnoreCase);
                foreach (Match _m in mc)
                {
                    hrLs.Add(new KeyValuePair<string, string>(_m.Groups["name"].Value, _m.Groups["val"].Value));
                }
            }
            catch (System.Exception e)
            {
                hrLs = null;
                Console.WriteLine(e.Message);
            }
            return hrLs;
        }

        public static string escape(string s)
        {
            string codeS = System.Web.HttpUtility.UrlEncodeUnicode(s);
            codeS = Regex.Replace(codeS, @"%u", @"\u", RegexOptions.IgnoreCase);
            codeS = HttpUtility.UrlDecode(codeS);
            return codeS;
        }

        public static string GetTimeStamp()
        {
            string stampString = string.Empty;
            try
            {
                TimeSpan ts = DateTime.Now.AddHours(-8) - new DateTime(1970, 1, 1, 0, 0, 0);
                //stampString = ((int)ts.TotalSeconds).ToString();
                stampString = ((long)ts.TotalMilliseconds).ToString();
            }
            catch (System.Exception)
            {
            }
            return stampString;
        }
        public static string GetTimeStampShort()
        {
            string stampString = string.Empty;
            try
            {
                TimeSpan ts = DateTime.Now.AddHours(-8) - new DateTime(1970, 1, 1, 0, 0, 0);
                stampString = ((int)ts.TotalSeconds).ToString();
                //stampString = ((long)ts.TotalMilliseconds).ToString();
            }
            catch (System.Exception)
            {
            }
            return stampString;
        }

    }
}
