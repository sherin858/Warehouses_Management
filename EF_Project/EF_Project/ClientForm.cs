using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Project
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            var clients = from c in Form1.Ent.Clients select c;
            foreach (var c in clients)
            {
                comboBox1.Items.Add(c.Client_Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client c = Form1.Ent.Clients.Find(textBox1.Text);
            if (c != null)
            {
                MessageBox.Show("Client Exists");
            }
            else
            {
                Client client = new Client();
                client.Client_Name = textBox1.Text;
                client.Telephone = textBox2.Text;
                client.Mobile = textBox3.Text;
                client.Fax = textBox4.Text;
                client.Email = textBox5.Text;
                client.Website = textBox6.Text;
                Form1.Ent.Clients.Add(client);
                Form1.Ent.SaveChanges();
                MessageBox.Show("Client Inserted");
                comboBox1.Items.Add(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Client c = Form1.Ent.Clients.Find(comboBox1.Text);
            c.Telephone = textBox2.Text;
            c.Mobile = textBox3.Text;
            c.Fax = textBox4.Text;
            c.Email = textBox5.Text;
            c.Website = textBox6.Text;
            Form1.Ent.SaveChanges();
            textBox1.Text = comboBox1.Text;
            MessageBox.Show("Client Updated");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clientName = comboBox1.Text;
            Client c = Form1.Ent.Clients.Find(clientName);
            if (c != null)
            {
                textBox1.Text = c.Client_Name;
                textBox2.Text = c.Telephone;
                textBox3.Text = c.Mobile;
                textBox4.Text = c.Fax;
                textBox5.Text = c.Email;
                textBox6.Text = c.Website ;
            }
        }
    }
}
