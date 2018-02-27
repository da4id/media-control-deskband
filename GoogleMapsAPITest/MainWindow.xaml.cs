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

using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;

namespace GoogleMapsAPITest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            googleRequest();
        }

        public void googleRequest()
        {
            var req = new DistanceMatrixRequest();
            req.Destinations = new List<Location>{ new Location("4052 Basel")};
            req.Origins = new List<Location>{ new Location("Kägenstrasse 4153 Reinach")};
            req.DepartureTime = DateTime.Now;
            req.TravelMode = TravelMode.Driving;
            req.Key = "AIzaSyDT0dI1bjDapLg1PQ81V4dTNKqhbjngTE8";
            req.TrafficModel = TrafficModel.Pessimistic;
            
            var resp = GoogleApi.GoogleMaps.DistanceMatrix.Query(req);

            lbDuration.Content = resp.Rows.First().Elements.First().DurationInTraffic.Value;
        }
    }
}
