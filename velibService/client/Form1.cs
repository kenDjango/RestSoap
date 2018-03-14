using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace client
{
    public partial class Form1 : Form
    {
        Service1Client client;

        public Form1(Service1Client client)
        {
            InitializeComponent();
            this.client = client;
            LoadContract();
            comboBox1.SelectionChangeCommitted += new EventHandler(this.choose_town);
            button3.Click += new EventHandler(this.button3_Click);
        }

        private void LoadContract()
        {
            string[] res = client.GetAllContract();
            comboBox1.Items.AddRange(res);
        }

        private void choose_town(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(client.GetStations(comboBox1.GetItemText(comboBox1.SelectedItem)));
            comboBox2.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string town = comboBox1.GetItemText(comboBox1.SelectedItem);
            string station = comboBox2.GetItemText(comboBox2.SelectedItem);
            label3.Text = client.GetAvaibleBike(town, station);
        }
    }
}
