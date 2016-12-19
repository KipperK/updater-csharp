using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NordInvasionUpdater.Classes
{
    static class Helpers
    {
        public static string DownloadToString(string address)
        {
            WebRequest request = WebRequest.Create(address);
            Stream stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static string getFileHash(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    byte[] hash = sha1.ComputeHash(bs);
                    StringBuilder formatted = new StringBuilder(2 * hash.Length);
                    foreach (byte b in hash)
                    {
                        formatted.AppendFormat("{0:X2}", b);
                    }
                    return formatted.ToString();
                }
            }
        }


        /// <summary>
        /// Compare two version strings.
        /// </summary>
        /// <param name="version1">First version string</param>
        /// <param name="version2">Second version string</param>
        /// <returns>The number of the larger string (0 of equal)</returns>
        public static int CompareVersionStrings(string version1, string version2)
        {
            // Split the version strings into arrays
            int[] array1 = version1.Split('.').Select(int.Parse).ToArray();
            int[] array2 = version2.Split('.').Select(int.Parse).ToArray();
            // Get the longer decimal count of the two arrays
            int length = version1.Length > version2.Length ? version1.Length : version2.Length;

            for (int i = 0; i < length; i++)
            {
                // Handling for version arrays with unequal lengths
                int dec1 = i < version1.Length ? version1[i] : 0;
                int dec2 = i < version2.Length ? version2[i] : 0;
                // If the decimal place is equal, skip to the next one
                if (dec1 == dec2)
                {
                    continue;
                }
                // If the decimal place is not equal, set the newer one and break from the loop
                return dec1 > dec2 ? 1 : 2;
            }

            // If you get this far, the versions are equal
            return 0;
        }
    }
}
