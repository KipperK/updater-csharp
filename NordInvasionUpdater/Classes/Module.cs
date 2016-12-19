using Newtonsoft.Json;
using NordInvasionUpdater.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordInvasionUpdater
{
    class Module
    {
        private string name;
        private List<string> addresses;

        private List<HashTable> hashTables = new List<HashTable>();
        private string path;

        public Module(string name, List<string> addresses)
        {
            this.name = name;
            this.addresses = addresses;

            for (int i = 0; i < addresses.Count; i++)
            {
                string response = Helpers.DownloadToString(addresses[i]);
                HashTable table = JsonConvert.DeserializeObject<HashTable>(response);
                this.hashTables.Add(table);
            }
        }
    }
}
