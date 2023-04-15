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
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }
        public void ResetControls ()
        {
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.Text= string.Empty;
            comboBox2.Text= string.Empty;
            comboBox3.Text= string.Empty;
            comboBox4.Text= string.Empty;
            textBox1.Text= string.Empty;
            textBox2.Text= string.Empty;
            textBox3.Text= string.Empty;
            textBox4.Text= string.Empty;
            textBox5.Text= string.Empty;
        }
        private void ImportForm_Load(object sender, EventArgs e)
        {
            var warehouses = from w in Form1.Ent.Warehouses select w;
            foreach (var w in warehouses)
            {
                comboBox2.Items.Add(w.Name);
            }
            var suppliers = from s in Form1.Ent.Suppliers select s;
            foreach (var s in suppliers)
            {
                comboBox3.Items.Add(s.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pnumber = int.Parse(comboBox1.Text);
            Permission p =Form1.Ent.Permissions.Find(pnumber);
            if (p != null)
            {
                textBox1.Text=p.Number.ToString();
                textBox2.Text=p.Date.ToString();
                comboBox2.Text = p.Warhouse_Name;
                comboBox3.Text = p.Supplier_Name;
                if (p.Permission_Product.Count > 0 && radioButton2.Checked)
                {
                    foreach (var product in p.Permission_Product)
                    {
                        comboBox4.Items.Add(product.Product_Code);
                    }
                }
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productcode = int.Parse(comboBox4.Text);
            int permissionnumber = int.Parse(comboBox1.Text);
            var pproduct = (from pp in Form1.Ent.Permission_Product where pp.Product_Code == productcode && pp.Permission_Number == permissionnumber select pp).FirstOrDefault();
            if (pproduct != null && radioButton2.Checked)
            {
                textBox3.Text = pproduct.Quantity.ToString();
                textBox4.Text = pproduct.Production_Date.ToString();
                textBox5.Text = pproduct.Expiration_Period.ToString();
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           ResetControls();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ResetControls();
            var permissions = (from p in Form1.Ent.Permissions select p).Where(p => p.Type == "Import");
            foreach (var p in permissions)
            {
                comboBox1.Items.Add(p.Number);
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ResetControls();
            var products = from p in Form1.Ent.Products select p;
            foreach (var p in products)
            {
                comboBox4.Items.Add(p.Code);
            }
            var permissions = (from p in Form1.Ent.Permissions select p).Where(p => p.Type == "Import");
            foreach (var p in permissions)
            {
                comboBox1.Items.Add(p.Number);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)//Add
            {
                int pnumber = int.Parse(textBox1.Text);
                Permission permission = Form1.Ent.Permissions.Find(pnumber);
                if (permission != null)
                {
                    MessageBox.Show("Permission Exists");
                }
                else
                {
                    try
                    {
                        Permission p = new Permission();
                        p.Number = pnumber;
                        p.Date = textBox2.Text;
                        p.Type = "Import";
                        p.Warhouse_Name = comboBox2.Text;
                        p.Supplier_Name = comboBox3.Text;
                        Form1.Ent.Permissions.Add(p);
                        Form1.Ent.SaveChanges();
                        MessageBox.Show("Permission Added");
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Date Format");
                    }

                }
            }
            else if (radioButton2.Checked)//Update
            {
                int pnumber = int.Parse(comboBox1.Text);
                Permission p = Form1.Ent.Permissions.Find(pnumber);
                if (p != null)
                {
                    try
                    {
                        p.Date = textBox2.Text;
                        p.Warhouse_Name = comboBox2.Text;
                        p.Supplier_Name = comboBox3.Text;
                        Form1.Ent.SaveChanges();
                        textBox1.Text = comboBox1.Text;
                        MessageBox.Show("Permission Updated");
                        if (comboBox4.Text != "")
                        {
                            int productcode = int.Parse(comboBox4.Text);
                            int permissionnumber = int.Parse(comboBox1.Text);
                            var pproduct = (from pp in Form1.Ent.Permission_Product where pp.Product_Code == productcode && pp.Permission_Number == permissionnumber select pp).FirstOrDefault();
                            //Permission_Product pproduct = Form1.Ent.Permission_Product.Find(int.Parse(comboBox4.Text));
                            pproduct.Quantity = int.Parse(textBox3.Text);
                            pproduct.Expiration_Period = int.Parse(textBox5.Text);
                            pproduct.Production_Date = textBox4.Text;
                            Form1.Ent.SaveChanges();
                            MessageBox.Show("Permission Product Updated");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Date Format");
                    }

                }
            }
            else if (radioButton3.Checked)//add product to permission
            {
                int pnumber = int.Parse(comboBox1.Text);
                Permission p = Form1.Ent.Permissions.Find(pnumber);
                if (p != null)
                {
                    Permission_Product pproduct = new Permission_Product();
                    pproduct.Permission_Number = pnumber;
                    pproduct.Product_Code = int.Parse(comboBox4.Text);
                    pproduct.Quantity = int.Parse(textBox3.Text);
                    pproduct.Production_Date = textBox4.Text;
                    pproduct.Expiration_Period = int.Parse(textBox5.Text);
                    Form1.Ent.Permission_Product.Add(pproduct);
                    Form1.Ent.SaveChanges();
                    MessageBox.Show("Permission Product Added");
                }
            }
        }
    }
}
