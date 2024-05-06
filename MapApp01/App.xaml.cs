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
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "<<MyAPIKey>>";
        }
    }
}
