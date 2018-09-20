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
    /// SpaceGameB.xaml 的交互逻辑
    /// </summary>
    public partial class SpaceGameB : Window
    {
        int x;
        int y;
        Boolean flag = true;
        Boolean isWindow;
        double volume;
        int count = 50;
        int life = 3;
        Boolean cTo = true;

        public SpaceGameB()
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
            randomCircle();
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

                x = (int)System.Windows.SystemParameters.PrimaryScreenWidth / 2 / 8 * 7 - 10;
                y = (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2 / 8 * 7 - 10;
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

                x = (int)System.Windows.SystemParameters.PrimaryScreenWidth / 8 * 7 - 10;
                y = (int)System.Windows.SystemParameters.PrimaryScreenHeight / 8 * 7 - 10;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            SpatialGameSelect sgs = new SpatialGameSelect();
            sgs.Show();
            this.Close();
        }

        private void randomCircle()
        {
            Random r = new Random();
            int cx = r.Next() % x;
            int cy = r.Next() % y;
            int cr = r.Next() % 500 + 10;
            baseCanvas.Children.Clear();
            Ellipse e = new Ellipse();
            e.Height = cr;
            e.Width = cr;
            e.Stroke = new SolidColorBrush(Color.FromArgb((byte)(r.Next() % 200), (byte)(r.Next() % 200), (byte)(r.Next() % 200), (byte)(r.Next() % 200)));
            e.Fill = new SolidColorBrush(Color.FromArgb((byte)(r.Next() % 200), (byte)(r.Next() % 200), (byte)(r.Next() % 200), (byte)(r.Next() % 200)));
            e.SetValue(Canvas.LeftProperty, (double)cx);
            e.SetValue(Canvas.TopProperty, (double)cy);

            e.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(e_MouseLeftButtonDown), true);

            baseCanvas.Children.Add(e);
        }

        private void e_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;
            count--;
            msg.Content = "";
            if (count <= 0)
            {
                msg.Content = "任务完成";
                msg.Foreground = new SolidColorBrush(Colors.LightSkyBlue);
                Change();
            }
            randomCircle();
        }

        private void baseCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (flag)
            {
                life--;
                chanceTimes.Content = life;
                if (life <= 0)
                {
                    msg.Content = "任务失败";
                    msg.Foreground = new SolidColorBrush(Colors.Red);
                    Change();
                }
                else
                {
                    msg.Content = "选择错误";
                    msg.Foreground = new SolidColorBrush(Colors.Yellow);
                }
            }
            flag = true;
        }

        private async void Change()
        {
            if (cTo)
            {
                cTo = false;
                await Task.Delay(3000);
                SpatialGameSelect sgs = new SpatialGameSelect();
                sgs.Show();
                this.Close();
            }
        }
    }
}
