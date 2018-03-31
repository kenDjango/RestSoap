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

        private async void LoadContract()
        {
            string[] res = await Task.Run(() => client.GetAllContract());
            comboBox1.Items.AddRange(res);
        }

        private async void choose_town(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string town = comboBox1.GetItemText(comboBox1.SelectedItem);
            string[] res = await Task.Run(() => client.GetStations(town));
            comboBox2.Items.AddRange(res);
            comboBox2.SelectedIndex = 0;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string town = comboBox1.GetItemText(comboBox1.SelectedItem);
            string station = comboBox2.GetItemText(comboBox2.SelectedItem);
            label3.Text = await Task.Run(() => client.GetAvaibleBike(town, station));
        }
    }
}
