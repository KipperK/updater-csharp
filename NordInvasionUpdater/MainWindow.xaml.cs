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
        protected string masterIndexAddress { get; } = @"http://odin.nordinvasion.com/mod/updater.php";
        protected string settingsRegistryKeyPath { get; } = @"SOFTWARE\NordInvasionUpdater";
        // End Configuration

        List<Module> modules;
        string versionAvailable;

        BackgroundWorker initModulesWorker = new BackgroundWorker();
        BackgroundWorker hashWorker = new BackgroundWorker();
        class initModulesWorker_Result
        {
            public string version { get; set; }
            public List<Module> modules { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            initModulesWorker.WorkerReportsProgress = false;
            initModulesWorker.WorkerSupportsCancellation = false;
            initModulesWorker.DoWork += new DoWorkEventHandler(InitModules_DoWork);
            initModulesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(InitModules_RunWorkerCompleted);
            initModulesWorker.RunWorkerAsync();
        }

        private void RunTest(object sender, RoutedEventArgs e)
        {
            
        }

        private void InitModules_DoWork(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            //try
            //{
                string response = Helpers.DownloadToString(masterIndexAddress);
                args.Result = JsonConvert.DeserializeObject<initModulesWorker_Result>(response);
            //}
            //catch (WebException e) //when (e.Status == WebExceptionStatus.ProtocolError)
            //{
            //    using (var response = e.Response as HttpWebResponse)
            //    {
            //        int statusCode = response != null ? (int)response.StatusCode : 0;
            //    }
            //}
        }

        private void InitModules_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            if (args.Error != null)
            {
                MessageBox.Show(args.Error.Message);
            }
            else
            {
                initModulesWorker_Result result = args.Result as initModulesWorker_Result;
                this.modules = result.modules;
                this.versionAvailable = result.version;
                MessageBox.Show("Success: "+this.modules.Count);
            }
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
