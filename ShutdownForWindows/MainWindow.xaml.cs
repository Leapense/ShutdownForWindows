using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using MicaWPF.Controls;
using WPFUI.Controls;

namespace ShutdownForWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MicaWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ListView_Selected(object sender, RoutedEventArgs e)
        {
            if(PowerOpt.SelectedIndex == 0)
            {
                System.Diagnostics.Process.Start("Rundll32.exe", "User32.dll,LockWorkStation");
                this.Close();
            }
            else if(PowerOpt.SelectedIndex == 1)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = "powershell.exe",
                    Arguments = "tsdiscon"
                };
                Process.Start(startInfo);
                this.Close();
            }
            else if(PowerOpt.SelectedIndex == 2)
            {
                
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = "powershell.exe",
                    Arguments = @"shutdown /l"
                };
                Process.Start(startInfo);
                this.Close();
            }
            else if(PowerOpt.SelectedIndex == 3)
            {
                Process.Start("taskmgr.exe");
                this.Close();
            }
        }
    }
}
