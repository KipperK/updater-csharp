using Newtonsoft.Json;
using NordInvasionUpdater.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NordInvasionUpdater
{
    class Module
    {
        public string name { get; }
        private string path { get; }
        public List<string> addresses { get; }
        public List<HashTable> hashTables { get; } = new List<HashTable>();

        public Module(string name, string path, List<string> addresses)
        {
            this.name = name;
            this.path = path;
            this.addresses = addresses;

            MessageBox.Show("Module: "+this.name);

            for (int i = 0; i < addresses.Count; i++)
            {
                try
                {
                    string response = Helpers.DownloadToString(addresses[i]);
                    HashTable table = JsonConvert.DeserializeObject<HashTable>(response);
                    this.hashTables.Add(table);
                }
                catch (WebException e)
                {
                    // Log web exception
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
