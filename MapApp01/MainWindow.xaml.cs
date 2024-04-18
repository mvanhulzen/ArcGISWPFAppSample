using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapApp01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            SetupMap();
            
        }

        private void CurrentMap_Loaded(object? sender, EventArgs e)
        {
            MainMapView.SetViewpoint(new Viewpoint(52.501208, 6.079295, 8000));
        }

        private void SetupMap()
        {
               // Create a new map with the basemap style
            CurrentMap = new Map(BasemapStyle.ArcGISTopographic);
            CurrentMap.Loaded += CurrentMap_Loaded;
            //create graphics overlay for the map
            graphicsOverlay = new GraphicsOverlay();
            //add the graphics overlay to the map view
            MainMapView.GraphicsOverlays.Add(graphicsOverlay);

        }
        
        public GraphicsOverlay graphicsOverlay { get; set; }

        public Map CurrentMap { get; set; }

        private void btnNewPoint_Click(object sender, RoutedEventArgs e)
        {
            // Create a new map point to define the graphic location.
            var pierPoint = new MapPoint(6.079295, 52.501208, SpatialReferences.Wgs84);

            // Create a new symbol for the graphic.
            var redCircleSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 20);

            // Create a new graphic.
            graphicsOverlay.Graphics.Add(new Graphic(pierPoint, redCircleSymbol));

        }

        private void btnNewRandomPoint_Click(object sender, RoutedEventArgs e)
        {
            List<Graphic> newPointGraphics = new List<Graphic>();
            for(int i=0;i<50;i++) {
                newPointGraphics.Add(AddRandomPoint());
            }

            PolylineBuilder polylineBuilder = new PolylineBuilder(SpatialReferences.WebMercator);
            //create line from points
            foreach (var item in newPointGraphics)
            {
                polylineBuilder.AddPoint(item.Geometry as MapPoint);
            }
            var lineSYmbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Blue, 2);
            graphicsOverlay.Graphics.Add(new Graphic(polylineBuilder.ToGeometry(), lineSYmbol));
            
        }

        private void btnMonuments_Click(object sender, RoutedEventArgs e)
        {

            FeatureLayer fl = new FeatureLayer(new ServiceFeatureTable(new Uri("https://services.arcgis.com/nSZVuSZjHpEZZbRo/arcgis/rest/services/Monumenten/FeatureServer/0")));

            CurrentMap.OperationalLayers.Add(fl);
                    //FeatureLayer.CreateFeatureTable(new Uri("https://services.arcgis.com/nSZVuSZjHpEZZbRo/arcgis/rest/services/Monumenten/FeatureServer/0")));

        }

        private Graphic AddRandomPoint()
        {
            Envelope currentExtent = MainMapView.GetCurrentViewpoint(ViewpointType.BoundingGeometry).TargetGeometry.Extent;

            double xrandom = currentExtent.XMin + (currentExtent.XMax - currentExtent.XMin) * new Random().NextDouble();
            double yrandom = currentExtent.YMin + (currentExtent.YMax - currentExtent.YMin) * new Random().NextDouble();

            // Create a new map point to define the graphic location.
            var pierPoint = new MapPoint(xrandom, yrandom, SpatialReferences.WebMercator);

            //get random color
            System.Drawing.Color randomColor = System.Drawing.Color.FromArgb(new Random().Next(255), new Random().Next(255), new Random().Next(255));
            var redCircleSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, randomColor, 10);
            Graphic g = new Graphic(pierPoint, redCircleSymbol);
            graphicsOverlay.Graphics.Add(g);

            return g;
        }

        private void btnBackGround_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in CurrentMap.Basemap.BaseLayers)
            {
                item.IsVisible = !item.IsVisible;            }
        }
    }
}