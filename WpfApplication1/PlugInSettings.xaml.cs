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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for PlugInSettgins.xaml
    /// </summary>
    public partial class PlugInSettgins : Window
    {

        List<PhotoBoothPlugInSDK.Parameter> _params;

        public PlugInSettgins(List<PhotoBoothPlugInSDK.Parameter> parameters)
        {
            InitializeComponent();

            _params = parameters;

            foreach (PhotoBoothPlugInSDK.Parameter param in parameters)
            {
                switch(param.getDisplayType())
                {
                    case PhotoBoothPlugInSDK.parameterDisplayType.number:                       
                        break;
                    case PhotoBoothPlugInSDK.parameterDisplayType.range:
                        Label l = new Label();
                        l.Content = param.getName().Replace(" ",string.Empty) + ":";
                        l.Visibility = Visibility.Visible;
                        Slider s = new Slider();
                        s.Name = param.getName().Replace(" ",string.Empty);
                        s.Visibility = Visibility.Visible;
                        s.Minimum = param.getMin();
                        s.Maximum = param.getMax();
                        s.SmallChange = param.getTick();
                        s.LargeChange = param.getTick() * 10;
                        s.Value = param.getDefault();
                        s.ValueChanged += slider_ValueChanged;
                        s.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.TopLeft;
                        stackPanel.Children.Add(l);
                        stackPanel.Children.Add(s);
                        break;
                    case PhotoBoothPlugInSDK.parameterDisplayType.text:
                        break;
                    case PhotoBoothPlugInSDK.parameterDisplayType.list:
                        break;
                }
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach(PhotoBoothPlugInSDK.Parameter par in _params)
            {
                if(par.getName().Replace(" ", string.Empty) == ((Slider)sender).Name)
                {
                    par.setValue((int)e.NewValue);
                    par.getTick();
                }
            }
        }
    }
}
