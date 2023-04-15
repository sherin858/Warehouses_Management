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
    public partial class Form1 : Form
    {
        public static WModel Ent = new WModel();
        List<RadioButton> RadioButtons = new List<RadioButton>();
        public Form1()
        {
            InitializeComponent();
            RadioButtons.Add(radioButton1);
            RadioButtons.Add(radioButton2);
            RadioButtons.Add(radioButton3);
            RadioButtons.Add(radioButton4);
            RadioButtons.Add(radioButton5);
            RadioButtons.Add(radioButton6);
            RadioButtons.Add(radioButton7);
            RadioButtons.Add(radioButton8);
            RadioButtons.Add(radioButton9);
            RadioButtons.Add(radioButton10);
            RadioButtons.Add(radioButton11);
            RadioButtons.Add(radioButton12);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string text="";
            foreach (RadioButton radioButton in RadioButtons)
            {
                if(radioButton.Checked) text = radioButton.Text;
            }
            if (text == "Warehouse")
            {
                WarehouseForm warehouseForm = new WarehouseForm();
                warehouseForm.ShowDialog();
            }
            else if (text == "Product")
            {
                ProductForm productForm= new ProductForm();
                productForm.ShowDialog();
            }
            else if (text=="Client")
            {
                ClientForm clientForm= new ClientForm();
                clientForm.ShowDialog();
            }
            else if(text == "Supplier")
            {
                SupplierForm supplierForm= new SupplierForm();
                supplierForm.ShowDialog();
            }
            else if (text=="Import Permission")
            {
                ImportForm importForm = new ImportForm();
                importForm.ShowDialog();
            }
            else if(text=="Export Permission")
            {
                ExportForm exportForm = new ExportForm();
                exportForm.ShowDialog();
            }
            else if(text == "Transaction")
            {
                TransactionForm transactionForm= new TransactionForm();
                transactionForm.ShowDialog();
            }


        }
        private void button7_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach (RadioButton radioButton in RadioButtons)
            {
                if (radioButton.Checked) text = radioButton.Text;
            }
            if (text == "Warehouse")
            {
                var warehouses = from w in Ent.Warehouses select new { w.Name, w.Address, w.Manager };
                dataGridView1.DataSource = warehouses.ToList();
            }
            else if (text == "Product")
            {
                var products = from p in Ent.Products select new { p.Code, p.Name };
                dataGridView1.DataSource = products.ToList();
            }
            else if (text == "Client")
            {
                var clients = from c in Ent.Clients select new { c.Client_Name, c.Telephone, c.Mobile, c.Fax, c.Email, c.Website };
                dataGridView1.DataSource = clients.ToList();
            }
            else if (text == "Supplier")
            {
                var suppliers = from s in Ent.Suppliers select new { s.Name, s.Telephone, s.Mobile, s.Fax, s.Email, s.Website };
                dataGridView1.DataSource = suppliers.ToList();
            }
            else if (text == "Import Permission")
            {
                var permissions = from p in Ent.Permissions where p.Type == "Import" select new { p.Number, p.Date, p.Type, p.Warhouse_Name, p.Supplier_Name };
                dataGridView1.DataSource = permissions.ToList();
            }
            else if (text == "Export Permission")
            {
                var permissions = from p in Ent.Permissions where p.Type == "Export" select new {p.Number,p.Date,p.Type,p.Warhouse_Name,p.Supplier_Name};
                dataGridView1.DataSource = permissions.ToList();
            }
            else if (text == "Transaction")
            {
                var transactions = from t in Ent.Transactions select new {t.ID,t.To_Warhouse,t.From_Warhouse,t.Supplier_Name };
                dataGridView1.DataSource = transactions.ToList();
            }
            else if(text== "Warehouse Products")
            {
                DataTable dt = new DataTable();
                var warehouseinfo = (from w in Ent.Warehouses where w.Name == comboBox1.Text select w).FirstOrDefault();
                dt.Columns.Add("Name");
                dt.Columns.Add("Address");
                dt.Columns.Add("Manager");
                dt.Columns.Add("Product");
                dt.Columns.Add("Product Code");
                foreach (var product in warehouseinfo.Products)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = warehouseinfo.Name;
                    dr["Address"]=warehouseinfo.Address;
                    dr["Manager"]=warehouseinfo.Manager;
                    dr["Product"]=product.Name;
                    dr["Product Code"] = product.Code;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            else if (text == "Product Warehouses")
            {
                DataTable dt = new DataTable();
                int pcode=int.Parse(comboBox2.Text);
                var productinfo = (from p in Ent.Products where p.Code == pcode select p).FirstOrDefault();
                dt.Columns.Add("Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("Warehouse Name");
                dt.Columns.Add("Warehouse Address");
                dt.Columns.Add("Warehouse Manager");
                foreach (var warehouse in productinfo.Warehouses)
                {
                    DataRow dr = dt.NewRow();
                    dr["Code"] = productinfo.Code;
                    dr["Name"] = productinfo.Name;
                    dr["Warehouse Name"]=warehouse.Name;
                    dr["Warehouse Address"] = warehouse.Address;
                    dr["Warehouse Manager"]=warehouse.Manager;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            else if (text== "Product Transaction in Warehouses")
            {
                DataTable dt = new DataTable();
                int pcode = int.Parse(comboBox3.Text);
                var transactioninfo = from t in Ent.Product_Transaction where t.Product_Code == pcode select t;
                dt.Columns.Add("Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("To Warehouse Name");
                dt.Columns.Add("From Warehouse Address");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Supplier");
                dt.Columns.Add("Production Date");
                dt.Columns.Add("Expiration Date");
                foreach (var transaction in transactioninfo)
                {
                    DataRow dr = dt.NewRow();
                    dr["Code"] = transaction.Product_Code;
                    dr["Name"]=transaction.Product.Name;
                    dr["Quantity"] = transaction.Quantity;
                    dr["Production Date"]=transaction.Production_Date;
                    dr["Expiration Date"]=transaction.Expiration_Period;
                    dr["To Warehouse Name"] = transaction.Transaction.To_Warhouse;
                    dr["From Warehouse Address"] = transaction.Transaction.From_Warhouse;
                    dr["Supplier"]= transaction.Transaction.Supplier_Name;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            else if (text== "Product Period in Warehouse")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Product Code");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Import Permission Number");
                dt.Columns.Add("Import Permission Date");
                dt.Columns.Add("Warehouse");
                dt.Columns.Add("Supplier Name");
                dt.Columns.Add("Production Date");
                dt.Columns.Add("Expiration Period");
                int days = int.Parse(textBox1.Text);
                var products = from p in Ent.Permission_Product select p;
                foreach(var p in products)
                {
                    DateTime importpermissiondate;
                    DateTime epermissiondate;
                    if (p.Permission.Type == "Import")
                    {
                        importpermissiondate = Convert.ToDateTime(p.Permission.Date);
                        importpermissiondate=importpermissiondate.AddDays(days);
                        var ExportPermissiondate = (from ep in Ent.Permission_Product where ep.Permission.Type == "Export" && ep.Product.Code == p.Product.Code && ep.Production_Date ==p.Production_Date select new { ep.Permission.Date }).FirstOrDefault();
                        if (ExportPermissiondate != null)
                        {
                            epermissiondate = Convert.ToDateTime(ExportPermissiondate.Date.ToString());
                            if (importpermissiondate.Date < epermissiondate.Date)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Product Code"] = p.Product_Code;
                                dr["Product Name"] = p.Product.Name;
                                dr["Import Permission Number"] = p.Permission_Number;
                                dr["Import Permission Date"] = p.Permission.Date.ToString();
                                dr["Warehouse"] = p.Permission.Warhouse_Name;
                                dr["Supplier Name"] = p.Permission.Supplier_Name;
                                dr["Production Date"] = p.Production_Date.ToString();
                                dr["Expiration Period"] = p.Expiration_Period;
                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr["Product Code"] = p.Product_Code;
                            dr["Product Name"] = p.Product.Name;
                            dr["Import Permission Number"] = p.Permission_Number;
                            dr["Import Permission Date"] = p.Permission.Date.ToString();
                            dr["Warehouse"] = p.Permission.Warhouse_Name;
                            dr["Supplier Name"] = p.Permission.Supplier_Name;
                            dr["Production Date"] = p.Production_Date.ToString();
                            dr["Expiration Period"] = p.Expiration_Period;
                            dt.Rows.Add(dr);
                        }
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            else if (text== "Products Close to Expiration Date")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Product Code");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Import Permission Number");
                dt.Columns.Add("Import Permission Date");
                dt.Columns.Add("Warehouse");
                dt.Columns.Add("Supplier Name");
                dt.Columns.Add("Production Date");
                dt.Columns.Add("Expiration Period");
                DateTime today = DateTime.Now;
                int days = int.Parse(textBox2.Text);
                var products = from p in Ent.Permission_Product select p;
                foreach (var p in products)
                {
                    DateTime importpermissiondate;
                    if (p.Permission.Type == "Import")
                    {
                        importpermissiondate = Convert.ToDateTime(p.Production_Date);
                        importpermissiondate = importpermissiondate.AddMonths((int)p.Expiration_Period);
                        var ExportPermissiondate = (from ep in Ent.Permission_Product where ep.Permission.Type == "Export" && ep.Product.Code == p.Product.Code && ep.Production_Date == p.Production_Date select new { ep.Permission.Date }).FirstOrDefault();
                        if (ExportPermissiondate == null)
                        {
                            if ((importpermissiondate.Date - today.Date).Days <= days )
                            {
                                DataRow dr = dt.NewRow();
                                dr["Product Code"] = p.Product_Code;
                                dr["Product Name"] = p.Product.Name;
                                dr["Import Permission Number"] = p.Permission_Number;
                                dr["Import Permission Date"] = p.Permission.Date.ToString();
                                dr["Warehouse"] = p.Permission.Warhouse_Name;
                                dr["Supplier Name"] = p.Permission.Supplier_Name;
                                dr["Production Date"] = p.Production_Date.ToString();
                                dr["Expiration Period"] = p.Expiration_Period;
                                dt.Rows.Add(dr);
                            }
                        }
                        dataGridView1.DataSource = dt;
                    }
                }

                }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var warehouses = from w in Form1.Ent.Warehouses select w;
            foreach (var w in warehouses)
            {
                comboBox1.Items.Add(w.Name);
                
            }
            var products = from p in Form1.Ent.Products select p;
            foreach (var p in products)
            {
                comboBox2.Items.Add(p.Code);
                comboBox3.Items.Add(p.Code);
            }
        }
    }
}
