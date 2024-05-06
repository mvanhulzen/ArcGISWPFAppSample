using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace MapApp01Selection
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            _map = new Map(SpatialReferences.WebMercator)
            {
                InitialViewpoint = new Viewpoint(new MapPoint(6.1,52.5, SpatialReferences.Wgs84),100000),

                Basemap = new Basemap(BasemapStyle.ArcGISStreets)
            };

            BuurtenLayer = new FeatureLayer(new ServiceFeatureTable(new Uri("https://services.arcgis.com/nSZVuSZjHpEZZbRo/arcgis/rest/services/CBS_Buurt_actueel/FeatureServer/0")));

            Map.OperationalLayers.Add(BuurtenLayer);


            
            
        }

        private Map _map;

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get => _map;
            set { _map = value; OnPropertyChanged(); }
        }




        public FeatureLayer BuurtenLayer { get; set; }
        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        internal async void HandleNewLocation(MapView view, System.Windows.Point? screenlocation)
        {
            
            if (screenlocation != null)
            {
                IdentifyLayerResult ilr = await view.IdentifyLayerAsync(BuurtenLayer, screenlocation.Value, 1, false);
                if (ilr.GeoElements.Count>0)
                {
                    BuurtenLayer.ClearSelection();
                }
                ilr.GeoElements.ToList().ForEach(ge =>
                {
                    if (ge is Feature f)
                    {
                        BuurtenLayer.SelectFeature(f);
                    }
                }); 
            }
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
