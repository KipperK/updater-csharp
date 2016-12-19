using Microsoft.Win32;
using Newtonsoft.Json;
using NordInvasionUpdater.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NordInvasionUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Configurations
        protected string masterIndexAddress = "";
        protected string settingsRegistryKeyPath = @"SOFTWARE\NordInvasionUpdater";
        // End Configuration

        BackgroundWorker downloadWorker = new BackgroundWorker();
        BackgroundWorker hashWorker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunTest(object sender, RoutedEventArgs e)
        {
            
        }

        public void LoadMasterIndex()
        {
            string response = Helpers.DownloadToString(masterIndexAddress);
            List<Module> modules = JsonConvert.DeserializeObject<List<Module>>(response);
        }

        public void SetSelectedWarbandPath(string value)
        {
            Registry.SetValue(settingsRegistryKeyPath, "SelectedWarbandPath", value);
        }

        public string GetSelectedWarbandPath()
        {
            return (string)Registry.GetValue(settingsRegistryKeyPath, "SelectedWarbandPath", null);
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
    }
}
