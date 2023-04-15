using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EF_Project
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            var products = from p in Form1.Ent.Products select p;
            foreach (var p in products)
            {
                comboBox1.Items.Add(p.Code);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product p = Form1.Ent.Products.Find(int.Parse(textBox1.Text));
            if (p != null)
            {
                MessageBox.Show("Product Exists");
            }
            else
            {
                Product Product = new Product();
                Product.Code = int.Parse(textBox1.Text);
                Product.Name = textBox3.Text;
                Form1.Ent.Products.Add(Product);
                Form1.Ent.SaveChanges();
                MessageBox.Show("Product Inserted");
                comboBox1.Items.Add(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product p = Form1.Ent.Products.Find(int.Parse(comboBox1.Text));
            p.Name = textBox3.Text;
            Form1.Ent.SaveChanges();
            textBox1.Text = comboBox1.Text;
            MessageBox.Show("Product Updated");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productCode = int.Parse(comboBox1.Text);
            Product p = Form1.Ent.Products.Find(productCode);
            if (p != null)
            {
                textBox1.Text = p.Code.ToString();
                textBox3.Text = p.Name;
            }
        }
    }
}
