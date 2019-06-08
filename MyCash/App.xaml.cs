using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace MyCash
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int cmdShow);
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr handle);

        private Mutex mutex = new Mutex(false, "WpfApplication");

        public App()
        {
            if (!mutex.WaitOne(500, false))
            {
                MessageBox.Show("Копия какого-то приложения работает!", "Ошибка");
                string processName = Process.GetCurrentProcess().ProcessName;
                Process process = Process.GetProcesses().Where(p => p.ProcessName == processName).FirstOrDefault();
                if (process != null)
                {
                    IntPtr handle = process.MainWindowHandle;
                    ShowWindow(handle, 1);
                    SetForegroundWindow(handle);
                }
                this.Shutdown();
                return;
            }
            InitializeComponent();
        }
    }
}
