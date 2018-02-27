using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SampleBars;
using GoogleApi.Entities.Maps.Common.Enums;

namespace SampleBars
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof(TrafficModel)))
            {
                cb_Mode.Items.Add(item);
            }

            cb_Mode.SelectedItem = Properties.Settings.Default.TrafficMode;

            tb_Origin.Text = Properties.Settings.Default.Abfahrtsort;
            tb_Destination.Text = Properties.Settings.Default.Zielort;
            tb_Key.Text = Properties.Settings.Default.ApiKey;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Abfahrtsort = tb_Origin.Text;
            Properties.Settings.Default.Zielort = tb_Destination.Text;
            Properties.Settings.Default.ApiKey = tb_Key.Text;
            Properties.Settings.Default.TrafficMode = cb_Mode.SelectedItem as TrafficModel;
            this.Close();
            

        }
    }
}
