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

namespace MapApp01Selection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MapViewModel = ((MapViewModel)this.Resources["MapViewModel"]);
        }

        public MapViewModel MapViewModel { get; set; }
        private void MapView_GeoViewTapped(object sender, Esri.ArcGISRuntime.UI.Controls.GeoViewInputEventArgs e)
        {

            MapViewModel.HandleNewLocation(MyMapView, e.Position);


        }

        private void MyMapView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                MapViewModel.HandleNewLocation(MyMapView, e.GetPosition(MyMapView));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
