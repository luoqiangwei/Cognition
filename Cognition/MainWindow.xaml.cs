using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Cognition
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //设置窗口是最小化、还原还是最大化窗口
            this.WindowState = System.Windows.WindowState.Normal;
            //设置窗口边框样式
            this.WindowStyle = System.Windows.WindowStyle.None;
            //设置窗口可以进行的操作
            this.ResizeMode = System.Windows.ResizeMode.CanMinimize;
            //设置为顶层窗口
            this.Topmost = true;

            //设置窗口位置
            this.Left = 0.0;
            this.Top = 0.0;
            //设置窗口大小
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            Setting settingWindow = new Setting();
            settingWindow.Show();
            //this.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}
