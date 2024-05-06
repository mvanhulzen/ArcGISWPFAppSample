using System.Configuration;
using System.Data;
using System.Windows;

namespace MapApp01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "AAPKced3cf99acda4b1daa333f6670e3c642mE7HmC_gXfA8G3PXOv1S1eZYNT1rLgIIcIgs1w_wlVbQaLZCZ-CDSBu6xmVhUiPb";
        }
    }
}
