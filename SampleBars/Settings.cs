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

namespace SampleBars
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            cb_Mode.Items.Add("Pessimistisch");
            cb_Mode.Items.Add("Standard");
            cb_Mode.Items.Add("Optimistisch");
            cb_Mode.SelectedIndex = cb_Mode.FindStringExact(Properties.Settings.Default.TrafficMode);

            tb_Origin.Text = Properties.Settings.Default.Abfahrtsort;
            tb_Destination.Text = Properties.Settings.Default.Zielort;
            tb_Key.Text = Properties.Settings.Default.ApiKey;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Abfahrtsort = tb_Origin.Text;
            Properties.Settings.Default.Zielort = tb_Destination.Text;
            Properties.Settings.Default.ApiKey = tb_Key.Text;
            Properties.Settings.Default.TrafficMode = cb_Mode.SelectedItem.ToString();
            this.Close();
            

        }
    }
}
