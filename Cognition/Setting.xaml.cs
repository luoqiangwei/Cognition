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
using System.Windows.Shapes;

using System.Xml;

namespace Cognition
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        Boolean isWindow;
        double volume;
        public Setting()
        {
            InitializeComponent();

            XmlDocument setting = new XmlDocument();
            setting.Load("setting.xml");
            XmlNodeList xlist = setting.GetElementsByTagName("isWindow");
            isWindow = Boolean.Parse(xlist[0].InnerText);
            xlist = setting.GetElementsByTagName("volume");
            volume = double.Parse(xlist[0].InnerText);
            realVolumeLabel.Content = volume.ToString("0.00");
            if (isWindow)
            {
                windowCheck.IsChecked = true;
            }

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            // 退出设置时保存数据
            XmlDocument setting = new XmlDocument();
            setting.Load("setting.xml");
            XmlNodeList xlist = setting.GetElementsByTagName("isWindow");
            xlist[0].InnerText = isWindow.ToString();
            xlist = setting.GetElementsByTagName("volume");
            xlist[0].InnerText = volume.ToString();
            setting.Save("setting.xml");

            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            windowResize();
        }

        private void volumeSilder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            volume = volumeSilder.Value;
            if(realVolumeLabel != null)
                realVolumeLabel.Content = volume.ToString("0.00");
        }

        private void windowCheck_Click(object sender, RoutedEventArgs e)
        {
            if (windowCheck.IsChecked == true)
            {
                isWindow = true;
                windowResize();
            }
            else
            {
                isWindow = false;
                windowResize();
            }
        }

        private void windowResize()
        {
            if (isWindow)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                this.ResizeMode = System.Windows.ResizeMode.CanResize;
                //this.Topmost = true;

                //设置窗口位置
                this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 4;
                this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 4;
                //设置窗口大小
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
            }
            else
            {
                //设置窗口是最小化、还原还是最大化窗口
                this.WindowState = System.Windows.WindowState.Normal;
                //设置窗口边框样式
                this.WindowStyle = System.Windows.WindowStyle.None;
                //设置窗口可以进行的操作
                this.ResizeMode = System.Windows.ResizeMode.CanMinimize;
                //设置为顶层窗口
                //this.Topmost = true;

                //设置窗口位置
                this.Left = 0.0;
                this.Top = 0.0;
                //设置窗口大小
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
        }
    }
}
