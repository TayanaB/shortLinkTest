using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLinks
{
    public static class Utils
    {
        public static string Domain;

        #region Link
        public static string GetLinkWithDomain(string str)
        {
            return $"{Domain}/{str}";
        }

        public static bool CheckUrlValid(string strUrl)
        {
            return Uri.IsWellFormedUriString(strUrl, UriKind.RelativeOrAbsolute);
        }

        public static string GetValidLink(string str)
        {
            var uri = new Uri(str, UriKind.RelativeOrAbsolute);
            return uri.IsAbsoluteUri ? str : $"http://{str}";
        }
        #endregion

        public static string RandomString(int len)
        {
            var utc = len > 30 ? DateTime.Now.ToFileTimeUtc().ToString() : "";
            len = len - utc.Length;
            var str = "";
            for (var i = 0; i < len / 11 + 1; i++)
            {
                str += Path.GetRandomFileName().Replace(".", "");
            }
            return str.Substring(0, len) + utc;
        }



    }
}
