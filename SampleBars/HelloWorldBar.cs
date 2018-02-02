using System;
using System.ComponentModel;
using System.Windows.Forms;
using BandObjectLib;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using Newtonsoft.Json;

namespace SampleBars
{
    [Guid("AE07101B-46D4-4a98-AF68-0333EA26E113")]
    [BandObject("Hello World Bar", BandObjectStyle.Horizontal | BandObjectStyle.ExplorerToolbar | BandObjectStyle.TaskbarToolBar, HelpText = "Shows bar that says hello.")]
    public class HelloWorldBar : BandObject
    {
        private System.ComponentModel.Container components = null;
        private Timer tmr1 = new Timer();

        public HelloWorldBar()
        {
            InitializeComponent();
            tmr1.Interval = 15*1000;
            tmr1.Tick += tmr1_Tick;
            tmr1.Start();
            requestTrafficFromGoogle();
        }

        void tmr1_Tick(object sender, EventArgs e)
        {
            tmr1.Start();
            requestTrafficFromGoogle();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void requestTrafficFromGoogle()
        {
            try
            {
                var req = new DistanceMatrixRequest();
                req.Destinations = new List<Location> { new Location("4052 Basel") };
                req.Origins = new List<Location> { new Location(Properties.Settings.Default.Abfahrtsort) };
                req.TravelMode = TravelMode.Driving;
                req.DepartureTime = DateTime.Now;
                req.Key = "API Key";
                req.TrafficModel = TrafficModel.Pessimistic;
                var test = req.GetQueryStringParameters();

                var resp = GoogleApi.GoogleMaps.DistanceMatrix.Query(req);

                var parsedResponse = JsonConvert.DeserializeObject<DistanceMatrixParser.myDistanceMatrixResponse>(resp.RawJson);

                if (resp.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
                {
                    if(parsedResponse.Rows.First().Elements.First().DurationInTraffic != null)
                    {
                        this.Title = parsedResponse.Rows.First().Elements.First().DurationInTraffic.Text;
                    }
                }
            }
            catch
            {
                this.Title = "Fehler";
            }
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HelloWorldBar
            // 
            this.MinSize = new System.Drawing.Size(0, 30);
            this.Name = "HelloWorldBar";
            this.Size = new System.Drawing.Size(0, 30);
            this.Title = "Hello Bar";
            this.ResumeLayout(false);

        }
        #endregion

    }
}
