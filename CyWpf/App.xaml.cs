using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Utils;

namespace CyWpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            JsonConvert.DefaultSettings = () => { return new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss" }; };
            DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        }
        public void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception.InnerException != null)
            {
                Logger.Exception("app.UnhandledException", e.Exception.InnerException);
            }
            else
            {
                Logger.Exception("app.UnhandledException", e.Exception);
            }
            MessageBox.Show(e.Exception.FullMessage());
            e.Handled = true;
            //Application.Current.Shutdown();
        }
    }
}
