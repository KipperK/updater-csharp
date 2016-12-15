using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NordInvasionUpdater
{
    class Module
    {
        private string name;
        private string path;
        private int[] versionAvailable;
        private Dictionary<string, string> localHashes;

        public Module(string name, string version)
        {
            this.name = name;
            this.versionAvailable = version.Split('.').Select(int.Parse).ToArray();
        }

        public void getLocalInfo()
        {

        }

        public void hashDirectoryContents(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            foreach(string directory in directories)
            {
                hashDirectoryContents(directory);
            }

            string[] files = Directory.GetFiles(path);
            foreach(string file in files)
            {
                
            }
        }

        private string getFileHash(string path)
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
    }
}
