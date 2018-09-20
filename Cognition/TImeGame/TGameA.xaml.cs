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
    /// TGameA.xaml 的交互逻辑
    /// </summary>
    public partial class TGameA : Window
    {
        Boolean isWindow;
        double volume;
        int ans;
        int count = 20;
        int life = 3;

        List<string> timeList = new List<string>();
        public TGameA()
        {
            InitializeComponent();

            XmlDocument setting = new XmlDocument();
            setting.Load("setting.xml");
            XmlNodeList xlist = setting.GetElementsByTagName("isWindow");
            isWindow = Boolean.Parse(xlist[0].InnerText);
            xlist = setting.GetElementsByTagName("volume");
            volume = double.Parse(xlist[0].InnerText);

            // 读取图片资源
            ReadAllFileName();

            // 随机显示图片
            RandomShow();
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
            TimeGameSelect tgs = new TimeGameSelect();
            tgs.Show();
            this.Close();
        }

        private void ReadAllFileName()
        {
            String[] files = Directory.GetFiles("Resource/Time/", "*.jpg");
            foreach (String file in files)
            {
                //string[] temp = file.Split(new char[2] { '/', '.' });
                //Console.WriteLine(temp[temp.Length - 2].Insert(2, ":"));
                timeList.Add(file);
            }
        }

        //  摘要:
        //  随机产生3个数，通过这3个数在timeList图片路径，并显示相应的图片
        private void RandomShow()
        {
            Random r = new Random();
            int t1 = r.Next() % timeList.Count();
            int t2 = r.Next() % timeList.Count();
            while (t2 == t1)
            {
                t2 = r.Next() % timeList.Count();
            }
            int t3 = r.Next() % timeList.Count();
            while (t3 == t1 || t3 == t2)
            {
                t3 = r.Next() % timeList.Count();
            }
            ans = r.Next() % 3;
            string[] temp = null;
            switch (ans)
            {
                case 0:
                    temp = timeList[t1].Split(new char[2] { '/', '.' });
                    goal.Content = temp[temp.Length - 2].Insert(2, ":");
                    break;
                case 1:
                    temp = timeList[t2].Split(new char[2] { '/', '.' });
                    goal.Content = temp[temp.Length - 2].Insert(2, ":");
                    break;
                case 2:
                    temp = timeList[t3].Split(new char[2] { '/', '.' });
                    goal.Content = temp[temp.Length - 2].Insert(2, ":");
                    break;
            }
            pic1.Source = new BitmapImage(new Uri("../" + timeList[t1], UriKind.Relative));
            pic2.Source = new BitmapImage(new Uri("../" + timeList[t2], UriKind.Relative));
            pic3.Source = new BitmapImage(new Uri("../" + timeList[t3], UriKind.Relative));
        }

        private void pic1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            judge(0);
        }

        private void pic2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            judge(1);
        }

        private void pic3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            judge(2);
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
                RandomShow();
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

        private async void Change()
        {
            back.IsEnabled = false;
            await Task.Delay(3000);
            TimeGameSelect tgs = new TimeGameSelect();
            tgs.Show();
            this.Close();
        }

        private void pic1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (life <= 0 || count <= 0)
            {
                Change();
            }
        }

        private void pic2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (life <= 0 || count <= 0)
            {
                Change();
            }
        }

        private void pic3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (life <= 0 || count <= 0)
            {
                Change();
            }
        }
    }
}
