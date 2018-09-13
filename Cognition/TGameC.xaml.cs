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
using System.IO;
using System.Threading;

namespace Cognition
{
    /// <summary>
    /// TGameC.xaml 的交互逻辑
    /// </summary>
    public partial class TGameC : Window
    {
        Boolean isWindow;
        double volume;

        int ans;
        int count = 10;
        int life = 3;
        int time;

        public TGameC()
        {
            InitializeComponent();

            XmlDocument setting = new XmlDocument();
            setting.Load("setting.xml");
            XmlNodeList xlist = setting.GetElementsByTagName("isWindow");
            isWindow = Boolean.Parse(xlist[0].InnerText);
            xlist = setting.GetElementsByTagName("volume");
            volume = double.Parse(xlist[0].InnerText);

            //添加自定义事件
            start.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(start_MouseLeftButtonDown), true);
            start.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(start_MouseLeftButtonUp), true);

            panlA.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(panlA_MouseLeftButtonDown), true);
            panlA.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(panlA_MouseLeftButtonUp), true);

            panlB.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(panlB_MouseLeftButtonDown), true);
            panlB.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(panlB_MouseLeftButtonUp), true);

            panlC.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(panlC_MouseLeftButtonDown), true);
            panlC.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(panlC_MouseLeftButtonUp), true);
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
                this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 4;
                this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 4;
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStyle = System.Windows.WindowStyle.None;
                this.ResizeMode = System.Windows.ResizeMode.CanMinimize;
                this.Left = 0.0;
                this.Top = 0.0;
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            TimeGameSelect tgs = new TimeGameSelect();
            tgs.Show();
            this.Close();
        }

        private void start_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetTime();
        }

        private void start_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            start.IsEnabled = false;
            wait();
        }

        private void judge(int flag)
        {
            if (ans == flag)
            {
                count--;
                msg.Content = "";
                if (count <= 0)
                {
                    msg.Content = "任务完成";
                    msg.Foreground = new SolidColorBrush(Colors.LightSkyBlue);
                }
                SetTime();
            }
            else
            {
                life--;
                chance.Content = life;
                if (life <= 0)
                {
                    msg.Content = "任务失败";
                    msg.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    msg.Content = "选择错误";
                    msg.Foreground = new SolidColorBrush(Colors.Yellow);
                }
            }
        }

        private void SetTime()
        {
            //panlA.IsEnabled = false;
            //panlB.IsEnabled = false;
            //panlC.IsEnabled = false;
            Random r = new Random();
            int a = r.Next() % 20 + 1;
            int b = r.Next() % 20 + 1;
            while (b == a)
            {
                b = r.Next() % 20 + 1;
            }
            int c = r.Next() % 20 + 1;
            while (c == a || c == b)
            {
                c = r.Next() % 20 + 1;
            }

            panlA.Content = a;
            panlB.Content = b;
            panlC.Content = c;

            ans = r.Next() % 3;
            switch (ans)
            {
                case 0:
                    time = a;
                    break;
                case 1:
                    time = b;
                    break;
                case 2:
                    time = c;
                    break;
            }
        }

        // 使用异步方式，防止主线程堵塞
        private async void wait()
        {
            panlA.IsEnabled = false;
            panlB.IsEnabled = false;
            panlC.IsEnabled = false;
            //Console.WriteLine(time);
            //Console.WriteLine(ans);
            await Task.Delay(time * 1000);
            //Thread.Sleep(time * 1000);
            panlA.IsEnabled = true;
            panlB.IsEnabled = true;
            panlC.IsEnabled = true;
        }

        private async void Change()
        {
            back.IsEnabled = false;
            await Task.Delay(3000);
            TimeGameSelect tgs = new TimeGameSelect();
            tgs.Show();
            this.Close();
        }

        private void panlA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            judge(0);
        }

        private void panlA_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (life <= 0 || count <= 0)
            {
                Change();
            }
            wait();
        }

        private void panlB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            judge(1);
        }

        private void panlB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (life <= 0 || count <= 0)
            {
                Change();
            }
            wait();
        }

        private void panlC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            judge(2);
        }

        private void panlC_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (life <= 0 || count <= 0)
            {
                Change();
            }
            wait();
        }
    }
}
