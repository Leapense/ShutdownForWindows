using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using MicaWPF.Services;
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

            //string getCurrentCulture = null;

            OSVer.Content = "        " + Environment.OSVersion;
            OSVer.FontSize = 14;
            
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
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetSuspendState(false, true, true);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dialog.Content = "Before you shutdown, Please check" + Environment.NewLine + "your unsaved documents or works!";
            dialog.FontSize = 20;
            dialog.ButtonLeftName = "Ok. I'll check.";
            dialog.Visibility = Visibility.Visible;
            dialog.ButtonRightName = "I saved all works.";
            dialog.ButtonLeftClick += Dialog_ButtonLeftClick;
            dialog.ButtonRightClick += Dialog_ButtonRightClick;

            dialog.Show();
        }

        private void Dialog_ButtonRightClick(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown", "/s /t 0");
            this.Close();
        }

        private void Dialog_ButtonRightClick2(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown", "/r /t 0");
            this.Close();
        }

        private void Dialog_ButtonLeftClick(object sender, RoutedEventArgs e)
        {
            dialog.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dialog.Content = "Before you restart, Please check" + Environment.NewLine + "your unsaved documents or works!";
            dialog.FontSize = 20;
            dialog.ButtonLeftName = "Ok. I'll check.";
            dialog.Visibility = Visibility.Visible;
            dialog.ButtonRightName = "I saved all works.";
            dialog.ButtonLeftClick += Dialog_ButtonLeftClick;
            dialog.ButtonRightClick += Dialog_ButtonRightClick2;
            dialog.Show();
        }

        private void MicaWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                OSVer.FontSize = 20;
            }
            else if(this.WindowState == WindowState.Normal)
            {
                OSVer.FontSize = 14;
            }
        }

        private void ThemeChanger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var themeService = ThemeService.GetCurrent();
            var tag_num = ThemeChanger.SelectedIndex;
            if (tag_num == 1)
                themeService.EnableMica(this, MicaWPF.BackdropType.Acrylic);
            else if (tag_num == 2)
                themeService.EnableMica(this, MicaWPF.BackdropType.Mica);
            else if (tag_num == 3)
                themeService.EnableMica(this, MicaWPF.BackdropType.Tabbed);
            else if (tag_num == 4)
                themeService.EnableMica(this, MicaWPF.BackdropType.None);
        }
    }
}
