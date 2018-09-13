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
    /// TimeGameSelect.xaml 的交互逻辑
    /// </summary>
    public partial class TimeGameSelect : Window
    {
        Boolean isWindow;
        double volume;
        public TimeGameSelect()
        {
            InitializeComponent();

            XmlDocument setting = new XmlDocument();
            setting.Load("setting.xml");
            XmlNodeList xlist = setting.GetElementsByTagName("isWindow");
            isWindow = Boolean.Parse(xlist[0].InnerText);
            xlist = setting.GetElementsByTagName("volume");
            volume = double.Parse(xlist[0].InnerText);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            windowResize();
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

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void gameA1_Click(object sender, RoutedEventArgs e)
        {
            TGameA1 g1 = new TGameA1();
            g1.Show();
            this.Close();
        }

        private void gameB_Click(object sender, RoutedEventArgs e)
        {
            TGameB gb = new TGameB();
            gb.Show();
            this.Close();
        }
    }
}
