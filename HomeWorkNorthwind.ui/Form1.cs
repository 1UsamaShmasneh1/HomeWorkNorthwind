using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWorkNorthwind.ui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label.Text = "1. Write a query to get Product name and quantity/unit.";
            using(var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductName, QuantityPerUnit
                //from Products
                //where Discontinued = 'False'
                //go
                var prd = db.Products
                    .Where(prod => prod.Discontinued == false)
                    .Select(prod => new {prod.ProductName, prod.QuantityPerUnit})
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label.Text = "2.Write a query to get current Product list(Product ID and name).";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductID, ProductName
                //from Products
                //order by ProductID
                //go
                var prd = db.Products
                    .Select(prod => new { prod.ProductId, prod.ProductName })
                    .OrderBy(prod => prod.ProductId)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label.Text = "3.Write a query to get discontinued Product list(Product ID and name).";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductName, ProductId
                //from Products
                //where Discontinued = 'true'
                //order by ProductName
                //go
                var prd = db.Products
                    .Where(prod => prod.Discontinued == true)
                    .Select(prod => new { prod.ProductName, prod.ProductId })
                    .OrderBy(prod => prod.ProductName)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label.Text = "4.Write a query to get most expense to least expensive Product list(name and unit price).";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductName, UnitPrice
                //from Products
                //order by UnitPrice desc
                //go
                var prd = db.Products
                    .Select(prod => new { prod.ProductName, prod.UnitPrice })
                    .OrderByDescending(prod => prod.UnitPrice)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label.Text = "5.Write a query to get Product list(id, name, unit price) where current products cost less than $20.";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductID, ProductName, UnitPrice
                //from Products
                //where UnitPrice < 20 AND Discontinued = 'False'
                //order by ProductID
                //go
                var prd = db.Products
                    .Where(prod => prod.UnitPrice < 20 &&  prod.Discontinued == false)
                    .Select(prod => new {prod.ProductId, prod.ProductName, prod.UnitPrice })
                    .OrderBy(prod => prod.ProductId)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label.Text = "6.Write a query to get Product list(name, unit price) where products cost between $15 and $25.";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductName, UnitPrice
                //from Products
                //where UnitPrice >= 15 And UnitPrice <= 25 and Discontinued = 'False'
                //order by ProductName
                //go
                var prd = db.Products
                    .Where(prod => prod.UnitPrice >= 15 && prod.UnitPrice <= 25 && prod.Discontinued == false)
                    .Select(prod => new { prod.ProductName, prod.UnitPrice })
                    .OrderBy(prod => prod.ProductName)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label.Text = "7.Write a query to get Product list(id, name, unit price) of above average price.";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select distinct ProductName, UnitPrice
                //from Products
                //where UnitPrice > (select avg(UnitPrice) from Products)
                //order by ProductName
                //go
                var prd = db.Products
                    .Where(prod => prod.UnitPrice > db.Products.Average(prod => prod.UnitPrice))
                    .Select(prod => new { prod.ProductName, prod.UnitPrice })
                    .OrderBy(prod => prod.ProductName)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label.Text = "8.Write a query to get Product list(name, unit price) of twenty most expensive products.";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select distinct top 20 ProductName, UnitPrice
                //from Products
                //order by UnitPrice desc
                //go
                var prd = db.Products
                    .Select(prod => new { prod.ProductName, prod.UnitPrice })
                    .OrderBy(prod => prod.ProductName)
                    .Take(20)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label.Text = "9.Write a query to count current and discontinued products.";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select count(ProductName)
                //from Products
                //group by Discontinued
                //go
                var prd = db.Products
                    .Select ( prod => new
                    {
                        discountinued = db.Products.Where(prod => prod.Discontinued == true).Count(),
                        current = db.Products.Where(prod => prod.Discontinued == false).Count()
                    })
                    .Take(1)
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label.Text = "10.Write a query to get Product list(name, units on order , units in stock) of stock is less than the quantity on order.";
            using (var db = new HomeWorkNorthwind.ui.Models.NorthwindContext())
            {
                //select ProductName, UnitsOnOrder, UnitsInStock
                //from Products
                //where Discontinued = 'False' and UnitsInStock<UnitsOnOrder
                //go
                var prd = db.Products
                    .Where(prod => prod.Discontinued == false && prod.UnitsInStock < prod.UnitsOnOrder)
                    .Select(prod => new { prod.ProductName, prod.UnitsOnOrder, prod.UnitsInStock })
                    .ToList();
                dataGridView.DataSource = prd;
            }
        }
    }
}
