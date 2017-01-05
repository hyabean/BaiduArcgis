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
using ArcgisBaiduMap.ArcgisExtend;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;

namespace ArcgisBaiduMap
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

        private void RadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            RadioButton r = sender as RadioButton;

            if (MyMap == null)
            {
                return;
            }

            if (MyMap.Layers.Count > 1)
            {
                MyMap.Layers.Clear();
            }

            if (r.Content.ToString() == "WMS")
            {

                //中国范围  
                Envelope FullExtent = new ESRI.ArcGIS.Client.Geometry.Envelope(121.156, 30.895442, 121.938455, 31.3189)
                {
                    SpatialReference = new SpatialReference(4326)
                };
                this.MyMap.ZoomTo(FullExtent);

                BaidumapWmsLayer Alayer = new BaidumapWmsLayer();
                Alayer.Visible = true;
                this.MyMap.Layers.Insert(0, Alayer);
                
                this.MyMap.ZoomTo(FullExtent);
            }
            else
            {
                //中国范围  
                Envelope FullExtent = new ESRI.ArcGIS.Client.Geometry.Envelope(9001735.65795624, 2919532.04645186, 19020489.8293508, 8346937.81802098)
                {
                    SpatialReference = new SpatialReference(102100)
                };
                this.MyMap.ZoomTo(FullExtent);

                BaidumapWmtsLayer Alayer = new BaidumapWmtsLayer();
                Alayer.MapType = "Map";
                Alayer.Visible = true;
                this.MyMap.Layers.Insert(0, Alayer);

                //中国范围  
                FullExtent = new ESRI.ArcGIS.Client.Geometry.Envelope(9001735.65795624, 2919532.04645186, 19020489.8293508, 8346937.81802098)
                {
                    SpatialReference = new SpatialReference(102100)
                };
                this.MyMap.ZoomTo(FullExtent);
            }
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            BaidumapWmsLayer Alayer = new BaidumapWmsLayer();
            Alayer.Visible = true;
            this.MyMap.Layers.Insert(0, Alayer);

            //中国范围  
            Envelope FullExtent = new ESRI.ArcGIS.Client.Geometry.Envelope(121.156, 30.895442, 121.938455, 31.3189)
            {
                SpatialReference = new SpatialReference(4326)
            };
            this.MyMap.ZoomTo(FullExtent);
        }

        private void MyMap_OnMouseClick(object sender, Map.MouseEventArgs e)
        {
            MessageBox.Show("坐标为：" + e.MapPoint.X + ";" + e.MapPoint.Y);
            Clipboard.SetText(e.MapPoint.X + ";" + e.MapPoint.Y);
        }
    }
}
