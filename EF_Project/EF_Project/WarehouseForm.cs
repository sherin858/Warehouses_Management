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
    public partial class WarehouseForm : Form
    {
        public WarehouseForm()
        {
            InitializeComponent();
        }

        private void WarehouseForm_Load(object sender, EventArgs e)
        {
            var warehouses = from w in Form1.Ent.Warehouses select w;
            foreach (var w in warehouses)
            {
                comboBox1.Items.Add(w.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string warehouseName=comboBox1.Text;
            Warehouse w = Form1.Ent.Warehouses.Find(warehouseName);
            if (w != null)
            {
                textBox1.Text = w.Name;
                textBox2.Text = w.Address;
                textBox3.Text = w.Manager;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Warehouse w =Form1.Ent.Warehouses.Find(textBox1.Text);
            if (w != null)
            {
                MessageBox.Show("Warehouse Exists");
            }
            else
            {
                Warehouse warehouse = new Warehouse();
                warehouse.Name = textBox1.Text;
                warehouse.Address = textBox2.Text;
                warehouse.Manager = textBox3.Text;
                Form1.Ent.Warehouses.Add(warehouse);
                Form1.Ent.SaveChanges();
                MessageBox.Show("Warehouse Inserted");
                comboBox1.Items.Add(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Warehouse w = Form1.Ent.Warehouses.Find(comboBox1.Text);
            w.Address = textBox2.Text;
            w.Manager = textBox3.Text;
            Form1.Ent.SaveChanges();
            textBox1.Text = comboBox1.Text;
            MessageBox.Show("Warehouse Updated");
        }
    }
}
