using System;
using System.Linq;
using Npgsql;
using System.Windows.Forms;
using System.Data.Entity;

namespace CompanyCRM
{
    public partial class Form1 : Form
    {
        CompanyCRMContext context = new CompanyCRMContext();
        BindingSource bi = new BindingSource();

        public Form1()
        {
            InitializeComponent();

            /*try
            {
                using (var context = new CompanyCRMContext())
                {
                    var products = context.productsItem.ToList();
                    foreach (var prd in products)
                    {
                        MessageBox.Show(prd.Producte);
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }*/

           // try
          // {
            ////   using (var context = new CompanyCRMContext())
            //   {
            //       var products = context.productsItem.ToList();
                    // READ
                   /* MessageBox.Show(products[0].Producte);
                    foreach (var prd in products)
                    {
                        MessageBox.Show(prd.Producte);
                    }*/

                    //context.SaveChanges();

                    // MODIFY 
                 //   var produ = context.productsItem.Where(s => s.id_Producte == 1500).First();
                    /*produ.Preu = 250;
                    context.SaveChanges();*/
                  //  var productee = new Productes() {id_Producte=1700,Producte="TEST",Preu=0 };
                    //context.productsItem.Attach(productee);
                    //context.SaveChanges();
             //   }  
          //  }
          // catch (Exception x)
          // {
         ///      MessageBox.Show(x.Message);
         //  }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            context = new CompanyCRMContext();
            //bi.DataSource = context.productsItem.ToList();
            bi.DataSource = context.Login.ToList();
            dataGridView1.DataSource = bi;
            dataGridView1.Refresh();
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
            context.SaveChanges();
            MessageBox.Show("Save");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            context = new CompanyCRMContext();
            bi.DataSource = context.Login.ToList();
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.DataSource = bi;
            dataGridView1.Refresh();

            var log = context.Login.ToList();
            foreach (var cust in log)
            {
                MessageBox.Show(cust.id + " " + cust.user + " " + cust.password);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (context = new CompanyCRMContext())
            {
                var customers = context.Set<LoginUser>();
                customers.Add(new LoginUser { user = "JohnDoe", password = "hola" });
                context.SaveChanges();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            context = new CompanyCRMContext();
            var customers = new LoginUser { id = 1 };
            context.Entry(customers).State = EntityState.Deleted;
            context.SaveChanges();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                string jobId = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
                string userId = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;

                textBox1.Text = jobId;
                textBox2.Text = userId;
            }
        }
    }
}
//https://stackoverflow.com/questions/21121971/why-savechanges-save-only-modification-of-existing-rows-but-not-deleted-or-ad
//https://stackoverflow.com/questions/5843537/filtering-datagridview-without-changing-datasource
//https://www.youtube.com/watch?v=RaZeeHRRCGA