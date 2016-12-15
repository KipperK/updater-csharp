using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace NordInvasionUpdater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Central storage for certain key values
        protected string settingsRegistryKeyPath = @"SOFTWARE\NordInvasionUpdater";
        protected string versionWebAddress = @"https://nordinvasion.com/mod/master.json";

        

        public void SetSelectedWarbandPath(string value)
        {
            Registry.SetValue(settingsRegistryKeyPath, "SelectedWarbandPath", value);
        }

        public string GetSelectedWarbandPath()
        {
            return (string) Registry.GetValue(settingsRegistryKeyPath, "SelectedWarbandPath", null);
        }

        public List<string> GetWarbandPaths()
        {
            RegistryKey muiCache = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\Shell\MuiCache");
            List<string> warbandPaths = new List<string>();
            foreach (string key in muiCache.GetSubKeyNames())
            {
                if (key.Contains("Warband"))
                {
                    warbandPaths.Add(key);
                }
            }

            return warbandPaths;
        }

        public void LoadRemoteVersionInfo()
        {
            WebRequest request = WebRequest.Create(versionWebAddress);
            Stream stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String response = reader.ReadToEnd();
        }


    }
}
