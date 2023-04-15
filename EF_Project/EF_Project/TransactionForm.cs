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
    public partial class TransactionForm : Form
    {
        public TransactionForm()
        {
            InitializeComponent();
        }
        public void ResetControls()
        {
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
            comboBox3.Text = string.Empty;
            comboBox4.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }
        private void ImportForm_Load(object sender, EventArgs e)
        {
            var warehouses = from w in Form1.Ent.Warehouses select w;
            foreach (var w in warehouses)
            {
                comboBox2.Items.Add(w.Name);
                comboBox3.Items.Add(w.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tnumber = int.Parse(comboBox1.Text);
            Transaction t = Form1.Ent.Transactions.Find(tnumber);
            if (t != null)
            {
                textBox1.Text = t.Supplier_Name;
                textBox2.Text = t.ID.ToString();
                comboBox2.Text = t.To_Warhouse;
                comboBox3.Text = t.From_Warhouse;
                if (t.Product_Transaction.Count > 0 && radioButton2.Checked)
                {
                    foreach (var product in t.Product_Transaction)
                    {
                        comboBox4.Items.Add(product.Product_Code);
                    }
                }
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productcode = int.Parse(comboBox4.Text);
            int transactionnumber = int.Parse(comboBox1.Text);
            var ptransaction = (from pt in Form1.Ent.Product_Transaction where pt.Product_Code == productcode && pt.Transaction_ID == transactionnumber select pt).FirstOrDefault();
            if (ptransaction!= null && radioButton2.Checked)
            {
                textBox3.Text = ptransaction.Quantity.ToString();
                textBox4.Text = ptransaction.Production_Date.ToString();
                textBox5.Text = ptransaction.Expiration_Period.ToString();
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ResetControls();
            var transactions = (from t in Form1.Ent.Transactions select t);
            foreach (var t in transactions)
            {
                comboBox1.Items.Add(t.ID);
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
            var transaction = (from t in Form1.Ent.Transactions select t);
            foreach (var t in transaction)
            {
                comboBox1.Items.Add(t.ID);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)//Add
            {
                int tnumber = int.Parse(textBox2.Text);
                Transaction transaction = Form1.Ent.Transactions.Find(tnumber);
                if (transaction != null)
                {
                    MessageBox.Show("Transaction Exists");
                }
                else
                {
                    try
                    {
                        Transaction t = new Transaction();
                        t.ID = int.Parse(textBox2.Text);
                        t.Supplier_Name= textBox1.Text;
                        t.To_Warhouse= comboBox2.Text;
                        t.From_Warhouse = comboBox3.Text;
                        Form1.Ent.Transactions.Add(t);
                        Form1.Ent.SaveChanges();
                        MessageBox.Show("Transaction Added");
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Date Format");
                    }

                }
            }
            else if (radioButton2.Checked)//Update
            {
                int tnumber = int.Parse(comboBox1.Text);
                Transaction t = Form1.Ent.Transactions.Find(tnumber);
                if (t != null)
                {
                    try
                    {
                        t.Supplier_Name = textBox1.Text;
                        t.To_Warhouse = comboBox2.Text;
                        t.From_Warhouse = comboBox3.Text;
                        Form1.Ent.SaveChanges();
                        textBox1.Text = comboBox1.Text;
                        MessageBox.Show("Transaction Updated");
                        if (comboBox4.Text != "")
                        {
                            int productcode = int.Parse(comboBox4.Text);
                            int transactionnumber = int.Parse(comboBox1.Text);
                            var ptransaction = (from pt in Form1.Ent.Product_Transaction where pt.Product_Code == productcode && pt.Transaction_ID == transactionnumber select pt).FirstOrDefault();
                            ptransaction.Quantity = int.Parse(textBox3.Text);
                            ptransaction.Expiration_Period = int.Parse(textBox5.Text);
                            ptransaction.Production_Date = textBox4.Text;
                            Form1.Ent.SaveChanges();
                            MessageBox.Show("Transaction Product Updated");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Date Format");
                    }

                }
            }
            else if (radioButton3.Checked)//add product to Transaction
            {
                int tnumber = int.Parse(comboBox1.Text);
                Transaction t = Form1.Ent.Transactions.Find(tnumber);
                if (t != null)
                {
                    Product_Transaction ptransaction = new Product_Transaction();
                    ptransaction.Transaction_ID = tnumber;
                    ptransaction.Product_Code = int.Parse(comboBox4.Text);
                    ptransaction.Quantity = int.Parse(textBox3.Text);
                    ptransaction.Production_Date = textBox4.Text;
                    ptransaction.Expiration_Period = int.Parse(textBox5.Text);
                    Form1.Ent.Product_Transaction.Add(ptransaction);
                    Form1.Ent.SaveChanges();
                    MessageBox.Show("Transaction Product Added");
                }
            }
        }
    }
}
