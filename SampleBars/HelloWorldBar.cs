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
    [BandObject("Verkehrsaufkommen", BandObjectStyle.Horizontal | BandObjectStyle.ExplorerToolbar | BandObjectStyle.TaskbarToolBar, HelpText = "Shows bar that says hello.")]
    public class HelloWorldBar : BandObject
    {
        private System.ComponentModel.Container components = null;
        private Label lblDiff;
        private Label lblRoute;
        private Label lblAmpel;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Timer tmr1 = new Timer();

        public HelloWorldBar()
        {
            InitializeComponent();
            if (Convert.ToUInt32(DateTime.Now.ToString("HH")) >= 15)
            {
                tmr1.Interval = 60 * 1000;
            }
            else
            {
                tmr1.Interval = 60 * 1000 * 5;
            }

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
                int traffic = 0;
                int normal = 0;
                int difference = 0;
                double prozent = 0.0;
                req.Destinations = new List<Location> { new Location(Properties.Settings.Default.Zielort) };
                req.Origins = new List<Location> { new Location(Properties.Settings.Default.Abfahrtsort) };
                req.TravelMode = TravelMode.Driving;
                req.DepartureTime = DateTime.Now;
                req.Key = Properties.Settings.Default.ApiKey;
                if (Properties.Settings.Default.TrafficMode == "Pessimistisch")
                {
                    req.TrafficModel = TrafficModel.Pessimistic;
                }
                else if (Properties.Settings.Default.TrafficMode == "Standard")
                {
                    req.TrafficModel = TrafficModel.Best_Guess;
                }
                else if (Properties.Settings.Default.TrafficMode == "Optimistisch")
                {
                    req.TrafficModel = TrafficModel.Optimistic;
                }

                var test = req.GetQueryStringParameters();
                DateTime now = new DateTime();

                now = DateTime.Now;

                var resp = GoogleApi.GoogleMaps.DistanceMatrix.Query(req);

                var parsedResponse = JsonConvert.DeserializeObject<DistanceMatrixParser.myDistanceMatrixResponse>(resp.RawJson);

                if (resp.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
                {
                    if (parsedResponse.Rows.First().Elements.First().DurationInTraffic != null)
                    {
                        traffic = Convert.ToInt32(Math.Round(parsedResponse.Rows.First().Elements.First().DurationInTraffic.Value / 60.0).ToString());
                        normal = Convert.ToInt32(Math.Round(parsedResponse.Rows.First().Elements.First().Duration.Value / 60.0).ToString());
                        difference = traffic - normal;
                        prozent = difference / normal;
                        if (difference > 0)
                        {
                            lblDiff.Text = "+" + Convert.ToString(difference) + " min  (norm:" + Convert.ToString(normal) + "min)";
                        }
                        else
                        {
                            lblDiff.Text = "keine Verspätung " + Convert.ToString(normal) + "min";
                        }

                        lblRoute.Text = Properties.Settings.Default.Abfahrtsort + " to " + Properties.Settings.Default.Zielort + " (" + parsedResponse.Rows.First().Elements.First().Distance.Text + ")";
                        if (prozent < 0.1)
                        {
                            lblAmpel.ForeColor = Color.Green;
                        }
                        else if (prozent < 0.2)
                        {
                            lblAmpel.ForeColor = Color.Orange;
                        }
                        else
                        {
                            lblAmpel.ForeColor = Color.Red;
                        }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelloWorldBar));
            this.lblDiff = new System.Windows.Forms.Label();
            this.lblRoute = new System.Windows.Forms.Label();
            this.lblAmpel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.Font = new System.Drawing.Font("E+H Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiff.ForeColor = System.Drawing.Color.White;
            this.lblDiff.Location = new System.Drawing.Point(18, -1);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(48, 20);
            this.lblDiff.TabIndex = 0;
            this.lblDiff.Text = "label1";
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.Font = new System.Drawing.Font("E+H Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoute.ForeColor = System.Drawing.Color.White;
            this.lblRoute.Location = new System.Drawing.Point(3, 22);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(37, 14);
            this.lblRoute.TabIndex = 1;
            this.lblRoute.Text = "label2";
            // 
            // lblAmpel
            // 
            this.lblAmpel.AutoSize = true;
            this.lblAmpel.BackColor = System.Drawing.Color.Transparent;
            this.lblAmpel.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblAmpel.ForeColor = System.Drawing.Color.White;
            this.lblAmpel.Location = new System.Drawing.Point(2, 1);
            this.lblAmpel.Name = "lblAmpel";
            this.lblAmpel.Size = new System.Drawing.Size(20, 17);
            this.lblAmpel.TabIndex = 2;
            this.lblAmpel.Text = "l";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(231, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 15);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(231, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 15);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // HelloWorldBar
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAmpel);
            this.Controls.Add(this.lblRoute);
            this.Controls.Add(this.lblDiff);
            this.MinSize = new System.Drawing.Size(250, 35);
            this.Name = "HelloWorldBar";
            this.Size = new System.Drawing.Size(250, 35);
            this.Title = "";
            this.Load += new System.EventHandler(this.tmr1_Tick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HelloWorldBar_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion



        private void HelloWorldBar_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new Settings().Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            requestTrafficFromGoogle();
        }
    }
}
