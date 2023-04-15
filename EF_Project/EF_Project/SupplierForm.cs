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
    public partial class SupplierForm : Form
    {
        public SupplierForm()
        {
            InitializeComponent();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            var clients = from s in Form1.Ent.Suppliers select s;
            foreach (var s in clients)
            {
                comboBox1.Items.Add(s.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Supplier s = Form1.Ent.Suppliers.Find(textBox1.Text);
            if (s != null)
            {
                MessageBox.Show("Supplier Exists");
            }
            else
            {
                Supplier upplier = new Supplier();
                upplier.Name = textBox1.Text;
                upplier.Telephone = textBox2.Text;
                upplier.Mobile = textBox3.Text;
                upplier.Fax = textBox4.Text;
                upplier.Email = textBox5.Text;
                upplier.Website = textBox6.Text;
                Form1.Ent.Suppliers.Add(upplier);
                Form1.Ent.SaveChanges();
                MessageBox.Show("Supplier Inserted");
                comboBox1.Items.Add(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Supplier s = Form1.Ent.Suppliers.Find(comboBox1.Text);
            s.Telephone = textBox2.Text;
            s.Mobile = textBox3.Text;
            s.Fax = textBox4.Text;
            s.Email = textBox5.Text;
            s.Website = textBox6.Text;
            Form1.Ent.SaveChanges();
            textBox1.Text = comboBox1.Text;
            MessageBox.Show("Supplier Updated");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clientName = comboBox1.Text;
            Supplier s = Form1.Ent.Suppliers.Find(clientName);
            if (s != null)
            {
                textBox1.Text = s.Name;
                textBox2.Text = s.Telephone;
                textBox3.Text = s.Mobile;
                textBox4.Text = s.Fax;
                textBox5.Text = s.Email;
                textBox6.Text = s.Website;
            }
        }
    }
}
