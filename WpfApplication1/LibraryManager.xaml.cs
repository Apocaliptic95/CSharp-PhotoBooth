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
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for LibraryManager.xaml
    /// </summary>
    public partial class LibraryManager : Window
    {
        public LibraryManager()
        {
            InitializeComponent();
            this.Show();
            textBlock.Text = "Ładowanie...";
            textBlock.Refresh();
            button.Visibility = Visibility.Hidden;
            button.Refresh();
            /*PhotoBooth.Other.LibraryManager lm = new PhotoBooth.Other.LibraryManager();
            if(lm.LibraryCheck().Count > 0)
            {
                System.Threading.Thread.Sleep(1000);
                textBlock.Text = "Brak wymaganych bibliotek.";
                textBlock.Refresh();
                button.Visibility = Visibility.Visible;
                button.Refresh();
            }
            else
            {*/
                System.Threading.Thread.Sleep(1000);
                textBlock.Text = "Wczytano pomyślnie.";
                textBlock.Refresh();
                System.Threading.Thread.Sleep(1000);
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            //}
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
