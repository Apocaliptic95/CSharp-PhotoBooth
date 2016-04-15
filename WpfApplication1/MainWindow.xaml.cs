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
using System.Drawing;
using PhotoBooth;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Threading;
using BasicPhotoEffects;
using PhotoBooth.Database.Controllers;
using PhotoBooth.PlugIn.Controllers;
using PhotoBooth.Image.Controllers;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebCameraController wcc;
        ImageProcessor ip;
        PhotoBooth.Other.FPS_Counter fps;
        List<PhotoBoothPlugInSDK.IProcessingImagePlugIn> pluglist;

        public MainWindow()
        {
            InitializeComponent();
            PlugInManager pim = new PlugInManager(new PlugInChecker(), new DataBase());
            foreach(string pl in pim.CheckPlugIns())
            {
               ListBoxItem itm = new ListBoxItem();
               itm.Content = pl;
               listBox.Items.Add(itm);
            }
            ip = new ImageProcessor();
            wcc = new WebCameraController();
            fps = new PhotoBooth.Other.FPS_Counter();
            List<string> list = wcc.getCameraList();
            foreach (string s in list)
            {
                comboBox.Items.Add(s);
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
                this.Title = (string) comboBox.Items[0];
                wcc.setCamera((string)comboBox.Items[0]);
                wcc.FrameGrabbedEvent += imagecc;

                pluglist = pim.getImagePlugInsList();
                foreach(PhotoBoothPlugInSDK.IProcessingImagePlugIn plugIn in pluglist)
                {
                    comboBox1.Items.Add(plugIn.getName());
                }
            }
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            label1.Content = fps.FPS;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            wcc.startLiveView();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            wcc.stopLiveView();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            wcc.setCamera((string)comboBox.SelectedItem);
        }

        public void imagecc(Bitmap bitmap)
        {
            bitmap = ip.processImage(bitmap);
            this.Dispatcher.Invoke((Action)(() =>
            {
                image.Source = BitmapToImageSource(bitmap);
            }));
            fps.countFrame();
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory,ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            wcc.Dispose();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1(wcc.TakePhoto());
            w1.Show();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Queue<PhotoBoothPlugInSDK.IProcessingImagePlugIn> effects = new Queue<PhotoBoothPlugInSDK.IProcessingImagePlugIn>();
            effects.Enqueue(pluglist[comboBox1.SelectedIndex]);
            ip.setFrameProcessing(effects);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                PlugInSettgins ps = new PlugInSettgins(pluglist[comboBox1.SelectedIndex].getParameters());
                ps.Show();
            }    
        }
    }
}
