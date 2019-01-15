using CompanyCRM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
	public partial class Main : Form
	{
        /* VARIABLES */
        CompanyCRMContext context,ctxProducts,ctxLogin,
            ctxCustomers,ctxInvoice,ctxInvoiceDetails;
        BindingSource bi = new BindingSource();
        BindingSource biLogin = new BindingSource();
        BindingSource biProducts = new BindingSource();
        BindingSource biCustomers = new BindingSource();
        BindingSource biInvoice = new BindingSource();
        BindingSource biInvoiceDetails = new BindingSource();
        String cmbBoxValue = "", enter = Environment.NewLine;
        User user;
        Date log = new Date();
        StringBuilder sbLog = new StringBuilder();
        int valcmbInvoice_Customer = 0,valcmbCustomer=0,valcmbProduct=0,valcmbUser=0
            ,valcmbDelInvoice_Invoice=0,valcmbDelInvoice_Customer=0,
            valcmbAddDetailID_Invoice=0, valcmbAddDetailID_Product = 0, 
            valcmbDelDetailID_Invoice = 0, valcmbDelDetailID_Product = 0;
        Boolean orderCol = false;

        /* MAIN CONSTRUCTOR */
        public Main(User n,string msg)
		{
			InitializeComponent();
            user = n;
            if (user.Role == "read")
            {
                tableLayoutPanelSettings.Hide();
                tableLayoutPanelProducts.Hide();
                tableLayoutPanelCustomer.Hide();
                tableLayoutPanelInvoice.Hide();
                tableLayoutPanelInvoiceDetails.Hide();
            }
            else if (user.Role == "write")
            {
                tableLayoutPanelSettings.Hide();
            }
            else if (user.Role == "admin") { }

            updateLog(log.showLog() + "Hi "+user.Name + enter, log.showLog() + "Hi " + user.Name);
        }

        /* MAIN LOAD */ 
        private void Main_Load(object sender, EventArgs e)
        {
            /* MAIN */
            context = new CompanyCRMContext();
            cmbRefresh();
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            /* TAB GESTIÓN */
            cmbBox.Items.Add("Products");
            cmbBox.Items.Add("Customers");
            cmbBox.Items.Add("Invoice");
            cmbBox.Items.Add("Invoice-Details");
            cmbTypeXML.Items.Add("Rows");
            cmbTypeXML.Items.Add("All");

            /* TAB SETTINGS */
            // CURRENT USER
            textBoxCurrentUserId.Text = user.Id.ToString();
            textBoxCurrentUserName.Text = user.Name.ToString();
            textBoxCurrentUserPassword.Text = user.Password.ToString();
            textBoxCurrentUserRole.Text = user.Role.ToString();
            // TABLE USERS
            ctxLogin = new CompanyCRMContext();
            biLogin.DataSource = ctxLogin.Login.ToList();
            dataGridViewLogin.DataSource = biLogin;
            dataGridViewLogin.Refresh();
            // OTHERS

            /* TAB PRODUCTS */
            // TABLE PRODUCTS
            ctxProducts = new CompanyCRMContext();
            biProducts.DataSource = ctxProducts.productsItem.ToList();
            dataGridViewProducts.DataSource = biProducts;
            dataGridViewProducts.Refresh();
            // OTHERS

            /* TAB CUSTOMERS */
            // TABLE CUSTOMERS
            ctxCustomers = new CompanyCRMContext();
            biCustomers.DataSource = ctxCustomers.Customers.ToList();
            dataGridViewCustomers.DataSource = biCustomers;
            dataGridViewCustomers.Refresh();
            // OTHERS

            /* TAB INVOICES */
            // TABLE INVOICES
             ctxInvoice = new CompanyCRMContext();
             biInvoice.DataSource = ctxInvoice.Invoice.ToList();
             dataGridViewInvoice.DataSource = biInvoice;
             dataGridViewInvoice.Refresh();
            // OTHERS
            dateTimePicker1.CustomFormat = "yyyy-MMM-dd";

            /* TAB INVOICES-DETAILS */
            // TABLE INVOICES-DETAILS
            ctxInvoiceDetails = new CompanyCRMContext();
            biInvoiceDetails.DataSource = ctxInvoiceDetails.Invoice_Detail.ToList();
            dataGridViewInvoiceDetails.DataSource = biInvoiceDetails;
            dataGridViewInvoiceDetails.Refresh();
            // OTHERS
 
        }

        // SHOW A TABLE
        private void cmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBox.SelectedItem)
            {
                case "Products":
                    cmbBoxValue = "Products";
                    context = new CompanyCRMContext();
                    bi.DataSource = context.productsItem.ToList();
                    dataGridView1.DataSource = bi;
                    dataGridView1.Refresh();
                    updateLog(log.showLog() + "Table " + cmbBoxValue + " Selected" + enter, log.showLog() + "Table " + cmbBoxValue + " Selected");
                    break;
                case "Customers":
                    cmbBoxValue = "Customers";
                    context = new CompanyCRMContext();
                    bi.DataSource = context.Customers.ToList();
                    dataGridView1.DataSource = bi;
                    dataGridView1.Refresh();
                    updateLog(log.showLog() + "Table " + cmbBoxValue + " Selected" + enter, log.showLog() + "Table " + cmbBoxValue + " Selected");
                    break;
                case "Invoice":
                    cmbBoxValue = "Invoice";
                    bi.DataSource = context.Invoice.ToList();
                    dataGridView1.DataSource = bi;
                    dataGridView1.Refresh();
                    updateLog(log.showLog() + "Table " + cmbBoxValue + " Selected" + enter, log.showLog() + "Table " + cmbBoxValue + " Selected");
                    break;
                case "Invoice-Details":
                    cmbBoxValue = "Invoice-Details";
                    bi.DataSource = context.Invoice_Detail.ToList();
                    dataGridView1.DataSource = bi;
                    dataGridView1.Refresh();
                    updateLog(log.showLog() + "Table " + cmbBoxValue + " Selected" + enter, log.showLog() + "Table " + cmbBoxValue + " Selected");
                    break;
            }             
        }

        // FILTER THE TABLE BY WORD OR NUMBER
        private void textBox1_buscar_TextChanged(object sender, EventArgs e)
        {
    
                switch (cmbBoxValue)
                {
                    case "Products":
                        dataGridView1.DataSource = context.productsItem.Where(x => x.Producte.Contains(textBox1_buscar.Text.ToUpper())).ToList();
                        updateLog(log.showLog() + "Filtering Table " + cmbBoxValue + enter, log.showLog() + "Filtering Table " + cmbBoxValue);
                        break;
                    case "Customers":
                        dataGridView1.DataSource = context.Customers.Where(x => x.Nom.Contains(textBox1_buscar.Text.ToUpper())
                        || x.Cognom1.Contains(textBox1_buscar.Text.ToUpper()) || x.Adreca.Contains(textBox1_buscar.Text.ToUpper())
                        || x.Poblacio.Contains(textBox1_buscar.Text.ToUpper()) || x.Provincia.Contains(textBox1_buscar.Text.ToUpper())
                        || x.Email.Contains(textBox1_buscar.Text.ToUpper())).ToList();
                        updateLog(log.showLog() + "Filtering Table " + cmbBoxValue + enter, log.showLog() + "Filtering Table " + cmbBoxValue);
                        break;
                    case "Invoice":
                        updateLog(log.showLog() + "Filtering Table " + cmbBoxValue + enter, log.showLog() + "Filtering Table " + cmbBoxValue);
                        break;
                    case "Invoice-Details":
                        updateLog(log.showLog() + "Filtering Table " + cmbBoxValue + enter, log.showLog() + "Filtering Table " + cmbBoxValue);
                        break;
                }
            
        }

        // ORDER THE TABLE BY HEADER
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (cmbBoxValue) {
                case "Products":
                    orderProducts(dataGridView1.Columns[e.ColumnIndex].Name);
                    break;
                case "Customers":
                    orderCustomers(dataGridView1.Columns[e.ColumnIndex].Name);
                    break;
                case "Invoice":
                    orderInvoice(dataGridView1.Columns[e.ColumnIndex].Name);
                    break;
                case "Invoice-Details":
                    orderInvoiceDetails(dataGridView1.Columns[e.ColumnIndex].Name);
                    break;
            }
     
        }

        // BUTTON THAT CALL FUNCTION PDFEXPORT
        private void btPDF_Click(object sender, EventArgs e)
        {
            if (user.Role == "write" || user.Role == "admin")
            {
                if (cmbBoxValue.Equals("") || cmbBoxValue == null)
                {
                    updateLog(log.showLog() + "TABLE NOT SELECTED" + enter, log.showLog() + "TABLE NOT SELECTED");
                }
                else exportPDF();
            }
        }

        // BUTTON THAT CALL FUNCTION EXPORTXML
        private void bt_exportar_Click(object sender, EventArgs e)
        {
            if (user.Role=="write" || user.Role=="admin") {
                if (cmbBoxValue.Equals("") || cmbBoxValue == null)
                {
                    updateLog(log.showLog() + "TABLE NOT SELECTED" + enter, log.showLog() + "TABLE NOT SELECTED");
                }
                else
                {
                    if (cmbTypeXML.SelectedItem.Equals("Rows")) exportXMLByRow();
                    else if (cmbTypeXML.SelectedItem.Equals("All")) exportXML();
                }
            }
        }

        // BUTTON THAT CALL FUNCTION IMPORTXML
        private void bt_importar_Click(object sender, EventArgs e)
        {
            if (user.Role == "write" || user.Role == "admin")
            {
                if (cmbBoxValue.Equals("") || cmbBoxValue == null)
                {
                    updateLog(log.showLog() + "TABLE NOT SELECTED" + enter, log.showLog() + "TABLE NOT SELECTED");
                }
                else importXML();
            }
        }

        /* OTHERS METHODS */
        // EXPORT TABLE SELECTED TO PDF
        private void exportPDF()
        {
            try
            {
                //int n = dataGridView1.ColumnCount;
                //if (n == 0) throw new Exception();
                //Creating iTextSharp Table from the DataTable data

                //Header
                PdfPTable headerTable = new PdfPTable(2);
                Paragraph title = new Paragraph(cmbBoxValue, FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                title.Alignment = Element.ALIGN_LEFT;
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("logo.png");
                img.ScaleToFit(105f, 105f);
                img.Alignment = Element.ALIGN_RIGHT;
                headerTable.DefaultCell.FixedHeight = 50f;
                headerTable.AddCell(title);
                headerTable.AddCell(img);

                //Body
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.HeaderRows = 1;
                // pdfTable.WidthPercentage = 40;
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.DefaultCell.BorderWidth = 1;
                pdfTable.SpacingBefore = 5;

                //Adding Header row
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    //cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                    pdfTable.AddCell(cell);
                }

                //Adding DataRow
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                        }
                    }
                }
                string folderPath = "";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderBrowserDialog.SelectedPath + "/";
                }

                //Exporting to PDF
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (FileStream stream = new FileStream(folderPath + cmbBoxValue + ".pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.SetMargins(30, 30, 30, 30);
                    pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.LETTER.Width, iTextSharp.text.PageSize.LETTER.Height));

                    pdfTable.SpacingBefore = 15f;
                    pdfDoc.Open();
                    //pdfDoc.Add(title);
                    //pdfDoc.Add(img);
                    pdfDoc.Add(headerTable);
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }catch (Exception) { richTextBox1.Text += log.showLog()+"ERROR: PROBLEM EXPORT PDF" + enter; }

            updateLog(log.showLog() + "USER[" + user.Name + "] EXPORT TABLE " + cmbBoxValue + " TO PDF" + enter, log.showLog() + "USER[" + user.Name + "] EXPORT TABLE " + cmbBoxValue + " TO PDF");
        }

        // IMPORT TABLE SELECTED TO XML
        private void importXML()
        {
            
            string sFileName = "", strItemNode="";
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sFileName = openFileDialog.FileName;
            }

            switch (cmbBoxValue)
            {
                case "Products":
                    strItemNode = "Product";
                    break;
                case "Customers":
                    strItemNode = "Customer";
                    break;
                case "Invoice":
                    strItemNode = "Invoice";
                    break;
                case "Invoice-Details":
                    strItemNode = "Invoice-Detail";
                    break;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(sFileName);
            string xmlcontents = doc.InnerXml;

            XmlNode nodeCompany = doc.SelectSingleNode("companycrm");

            XmlNodeList propProducts = nodeCompany.SelectNodes("Products");

            foreach (XmlNode item in propProducts)
            {
                //richTextBox1.Text += item.Value;
            }
        }

        // EXPORT TABLE SELECTED TO XML
        private void exportXML()
        {
            string nPath = "";
            string stritemNode = "";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                nPath = folderBrowserDialog.SelectedPath + "/";
            }

            switch (cmbBoxValue)
            {
                case "Products":
                    stritemNode = "Product";
                    break;
                case "Customers":
                    stritemNode = "Customer";
                    break;
                case "Invoice":
                    stritemNode = "Invoice";
                    break;
                case "Invoice-Details":
                    stritemNode = "Invoice-Detail";
                    break;
            }

            FileInfo info = new FileInfo(nPath + cmbBoxValue);

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("companycrm");
            xmlDoc.AppendChild(rootNode);

            XmlNode tableNode;
            XmlNode itemNode;
            XmlNode colNode = xmlDoc.CreateElement(dataGridView1.Columns[0].Name);

            tableNode = xmlDoc.CreateElement(cmbBoxValue);
            int y=0,x = 0,cmpRow= dataGridView1.Rows.Count-1;

             foreach (DataGridViewRow row in dataGridView1.Rows)
             {
                if (y<cmpRow) {
                    itemNode = xmlDoc.CreateElement(stritemNode);
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (x < dataGridView1.Columns.Count)
                        {
                            colNode = xmlDoc.CreateElement(dataGridView1.Columns[x].Name);

                            if (cell.Value != null) colNode.InnerText = cell.Value.ToString();

                            itemNode.AppendChild(colNode);
                        }
                        x++;
                    }
                    x = 0;

                    tableNode.AppendChild(itemNode);
                    y++;
                }
             }

            rootNode.AppendChild(tableNode);

            xmlDoc.Save(nPath + cmbBoxValue + ".xml");

            updateLog(log.showLog() + "USER[" + user.Name + "] EXPORT TABLE " + cmbBoxValue + " TO XML" + enter, log.showLog() + "USER[" + user.Name + "] EXPORT TABLE " + cmbBoxValue + " TO XML");
        }

        // EXPORT TABLE SELECTED TO XML BY ROW
        private void exportXMLByRow()
        {
            int countRows = dataGridView1.SelectedCells.Cast<DataGridViewCell>()
                         .Select(c => c.RowIndex).Distinct().Count();

            var selectedRows = dataGridView1.SelectedRows
            .OfType<DataGridViewRow>()
            .Where(row => !row.IsNewRow)
            .ToArray();

            string nPath = "";
            string stritemNode = "";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                nPath = folderBrowserDialog.SelectedPath + "/";
            }

            switch (cmbBoxValue)
            {
                case "Products":
                    stritemNode = "Product";
                    break;
                case "Customers":
                    stritemNode = "Customer";
                    break;
                case "Invoice":
                    stritemNode = "Invoice";
                    break;
                case "Invoice-Details":
                    stritemNode = "Invoice-Detail";
                    break;
            }

            FileInfo info = new FileInfo(nPath + cmbBoxValue);

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("companycrm");
            xmlDoc.AppendChild(rootNode);

            XmlNode tableNode;
            XmlNode itemNode;
            XmlNode colNode = xmlDoc.CreateElement(dataGridView1.Columns[0].Name);

            tableNode = xmlDoc.CreateElement(cmbBoxValue);
            int y = 0, x = 0, cmpRow = dataGridView1.Rows.Count - 1;

            foreach (DataGridViewRow row in selectedRows)
            {
                if (y < cmpRow)
                {
                    itemNode = xmlDoc.CreateElement(stritemNode);
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (x < dataGridView1.Columns.Count)
                        {
                            colNode = xmlDoc.CreateElement(dataGridView1.Columns[x].Name);

                            if (cell.Value != null) colNode.InnerText = cell.Value.ToString();

                            itemNode.AppendChild(colNode);
                        }
                        x++;
                    }
                    x = 0;

                    tableNode.AppendChild(itemNode);
                    y++;
                }
            }

            rootNode.AppendChild(tableNode);

            xmlDoc.Save(nPath + cmbBoxValue + ".xml");

            updateLog(log.showLog() + "USER[" + user.Name + "] EXPORT TABLE " + cmbBoxValue + " TO XML" + enter, log.showLog() + "USER[" + user.Name + "] EXPORT TABLE " + cmbBoxValue + " TO XML");
        }

        // UPDATE LOG
        private void updateLog(string strRich,string strLog){
            richTextBox1.Text += strRich;
            richTextBox2.Text += strRich;
            richTextBox3.Text += strRich;
            richTextBox4.Text += strRich;
            richTextBox5.Text += strRich;
            richTextBox6.Text += strRich;
            addToLog(strLog);
        }

        // ADD LOG TO STRINGBUILDER
        private void addToLog(String n)
        {
            sbLog.Append(n);
            sbLog.AppendLine();
        }

        /* TAB GESTION */
        // APPEND LOG IN TO A FILE
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateLog(log.showLog() + "Bye " + user.Name + enter, log.showLog() + "Bye " + user.Name);
            FileInfo info = new FileInfo(Environment.CurrentDirectory + "/log.txt");
            File.AppendAllText(Environment.CurrentDirectory + "/log.txt", sbLog.ToString());
        }

        // SELECT ACTUAL ROW
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            /* if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
             {
                 string jobId = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
                 string userId = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;

                 textBox1.Text = jobId;
                 textBox2.Text = userId;
             }*/
        }

        // SAVE CHANGES
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (user.Role == "admin" || user.Role == "write")
            {
                updateLog(log.showLog() + "ACCESS GRANTED" + enter, log.showLog() + "ACCESS GRANTED");

                dataGridView1.EndEdit();
                context.SaveChanges();
                dataGridView1.Refresh();
                updateLog(log.showLog() + cmbBoxValue + " SAVED" + enter, log.showLog() + cmbBoxValue + " SAVED");
            }
            else
            {
                updateLog(log.showLog() + "ACCESS NOT GRANTED" + enter, log.showLog() + "ACCESS NOT GRANTED");
            }
            
        }

        /* TAB USERS*/
        // ADD USER
        private void btAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                using (ctxLogin = new CompanyCRMContext())
                {
                    cmbRefresh();
                    var users = ctxLogin.Set<LoginUser>();
                    users.Add(new LoginUser
                    {
                        id = int.Parse(textBoxAddUserId.Text),
                        user = textBoxAddUserUser.Text,
                        password = textBoxAddUserPass.Text,
                        role = textBoxAddUserRole.Text
                    });
                    ctxLogin.SaveChanges();
                    cmbRefresh();
                    refreshLogin();
                    updateLog(log.showLog() + "USER ADDED" + enter, log.showLog() + "USER ADDED");
                }
            }
            catch (Exception) { MessageBox.Show("Error AddUser"); }
            
        }

        // SAVE USERS
        private void dataGridViewLogin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try { 
                dataGridViewLogin.EndEdit();
                ctxLogin.SaveChanges();
                refreshLogin();
                updateLog(log.showLog() + "USER SAVED" + enter, log.showLog() + "USER SAVED");
            }
            catch (Exception) { MessageBox.Show("Error SaveUser"); };
        }

        // DELETE USER
        private void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cmbRefresh();
                ctxLogin = new CompanyCRMContext();
                var user = new LoginUser { id = valcmbUser };
                ctxLogin.Entry(user).State = EntityState.Deleted;
                ctxLogin.SaveChanges();
                dataGridViewLogin.Refresh();
                refreshLogin();
                cmbRefresh();
                updateLog(log.showLog() + "USER DELETED" + enter, log.showLog() + "USER DELETED");
            }
            catch (Exception) { MessageBox.Show("Error DeleteUser"); }
        }

        // LOAD COMBOBOX USER
        private void cmbUserLogin()
        {
            cmbDeleteUser.Items.Clear();
            ctxLogin = new CompanyCRMContext();
            var users = ctxLogin.Login.ToList();
            foreach (var cust in users) cmbDeleteUser.Items.Add(cust.id);

        }

        // SAVE ACTUAL INDEX
        private void cmbDeleteUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbUser = int.Parse(cmbDeleteUser.SelectedItem.ToString());
        }

        // REFRESH TABLE LOGIN
        private void refreshLogin()
        {
            ctxLogin = new CompanyCRMContext();
            biProducts.DataSource = ctxLogin.Login.ToList();
            dataGridViewLogin.DataSource = biProducts;
            dataGridViewLogin.Refresh();
        }

        /* TAB PRODUCTS*/
        // ADD PRODUCT
        private void btAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                using (ctxProducts = new CompanyCRMContext())
                {
                    cmbRefresh();
                    var customers = ctxProducts.Set<Productes>();
                    customers.Add(new Productes
                    {
                        id_Producte = int.Parse(textBoxAddProductId.Text),
                        Producte = textBoxAddProductProduct.Text,
                        Preu = double.Parse(textBoxAddProductPrice.Text)
                    });
                    ctxProducts.SaveChanges();
                    refreshProducts();
                    cmbRefresh();
                    updateLog(log.showLog() + "PRODUCT ADDED" + enter, log.showLog() + "PRODUCT ADDED");
                }
            }
            catch (Exception u) { MessageBox.Show(u.Message); }      
        }

        // SAVE PRODUCTS
        private void dataGridViewProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewProducts.EndEdit();
                ctxProducts.SaveChanges();
                dataGridViewProducts.Refresh();
                refreshProducts();
                updateLog(log.showLog() + "PRODUCT SAVED" + enter, log.showLog() + "PRODUCT SAVED");
            }
            catch (Exception) { MessageBox.Show("Error SaveProduct"); };
        }

        // DELETE PRODUCT
        private void btDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                cmbRefresh();
                context = new CompanyCRMContext();
                var product = new Productes { id_Producte = valcmbProduct };
                context.Entry(product).State = EntityState.Deleted;
                context.SaveChanges();
                refreshProducts();
                cmbRefresh();
                updateLog(log.showLog() + "PRODUCT DELETED" + enter, log.showLog() + "PRODUCT DELETED");
            }
            catch (Exception) { MessageBox.Show("Error DeleteProduct"); }
        }

        // LOAD COMBOBOX PRODUCTS
        private void cmbProducts()
        {
            cmbDeleteProduct.Items.Clear();
            ctxProducts = new CompanyCRMContext();
            var products = ctxProducts.productsItem.ToList();
            foreach (var cust in products) cmbDeleteProduct.Items.Add(cust.id_Producte);
        }

        // SAVE ACTUAL INDEX
        private void cmbDeleteProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbProduct = int.Parse(cmbDeleteProduct.SelectedItem.ToString());
        }

        // REFRESH TABLE PRODUCTS
        private void refreshProducts()
        {
            ctxProducts = new CompanyCRMContext();
            biProducts.DataSource = ctxProducts.productsItem.ToList();
            dataGridViewProducts.DataSource = biProducts;
            dataGridViewProducts.Refresh();
        }

        // ORDER PRODUCTS
        private void orderProducts(string n)
        {
            if (orderCol)
            {
                switch (n)
                {
                    case "id_Producte":
                        dataGridView1.DataSource = context.productsItem.OrderByDescending(o => o.id_Producte).ToList();
                        break;
                    case "Producte":
                        dataGridView1.DataSource = context.productsItem.OrderByDescending(o => o.Producte).ToList();
                        break;
                    case "Preu":
                        dataGridView1.DataSource = context.productsItem.OrderByDescending(o => o.Preu).ToList();
                        break;
                }
                orderCol = false;
            }
            else
            {
                switch (n)
                {
                    case "id_Producte":
                        dataGridView1.DataSource = context.productsItem.OrderBy(o => o.id_Producte).ToList();
                        break;
                    case "Producte":
                        dataGridView1.DataSource = context.productsItem.OrderBy(o => o.Producte).ToList();
                        break;
                    case "Preu":
                        dataGridView1.DataSource = context.productsItem.OrderBy(o => o.Preu).ToList();
                        break;
                }
                orderCol = true;
            }
        }

        /* TAB CUSTOMERS*/
        // ADD CUSTOMER
        private void btAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                using (ctxCustomers = new CompanyCRMContext())
                {
                    cmbRefresh();
                    var customers = ctxCustomers.Set<Customers>();
                    customers.Add(new Customers
                    {
                        id_Client = int.Parse(textBoxAddCustomerId.Text),
                        Nom = textBoxAddCustomerName.Text,
                        Cognom1 = textBoxAddCustomerCognom.Text,
                        Poblacio = textBoxAddCustomerPoblation.Text,
                        Provincia = textBoxAddCustomerState.Text,
                        Telefon = int.Parse(textBoxAddCustomerTlf.Text),
                        Fax = int.Parse(textBoxAddCustomerFax.Text),
                        Email = textBoxAddCustomerMail.Text,
                        Adreca = textBoxAddCustomerAddress.Text
                    });
                    ctxCustomers.SaveChanges();
                    cmbRefresh();
                    refreshCustomers();
                    updateLog(log.showLog() + "CUSTOMER ADDED" + enter, log.showLog() + "CUSTOMER ADDED");
                }
            }
            catch (Exception) { MessageBox.Show("Error AddCustomer"); }         
        }

        // SAVE CUSTOMERS
        private void dataGridViewCustomers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewCustomers.EndEdit();
                ctxCustomers.SaveChanges();
                refreshCustomers();
                updateLog(log.showLog() + "PRODUCT SAVED" + enter, log.showLog() + "PRODUCT SAVED");
            }
            catch (Exception) { MessageBox.Show("Error SaveCustomer"); };
        }
        
        // DELETE CUSTOMER
        private void btDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                cmbRefresh();
                ctxCustomers = new CompanyCRMContext();
                var customer = new Customers { id_Client = valcmbCustomer };
                ctxCustomers.Entry(customer).State = EntityState.Deleted;
                ctxCustomers.SaveChanges();
                dataGridViewLogin.Refresh();
                refreshCustomers();
                cmbRefresh();
                updateLog(log.showLog() + "CUSTOMER DELETED" + enter, log.showLog() + "CUSTOMER DELETED");
            }
            catch (Exception) { MessageBox.Show("Error DeleteCustomer"); }
        }

        // SAVE ACTUAL INDEX
        private void cmbDeleteCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbCustomer = int.Parse(cmbDeleteCustomer.SelectedItem.ToString());
        }
        
        // LOAD COMBOBOX CUSTOMERS
        private void cmbCustomers()
        {
            cmbDeleteCustomer.Items.Clear();
            ctxCustomers = new CompanyCRMContext();
            var customers = ctxCustomers.Customers.ToList();
            foreach (var cust in customers) cmbDeleteCustomer.Items.Add(cust.id_Client);
        }

        // REFRESH TABLE CUSTOMERS
        private void refreshCustomers()
        {
            ctxCustomers = new CompanyCRMContext();
            biCustomers.DataSource = ctxCustomers.Customers.ToList();
            dataGridViewCustomers.DataSource = biCustomers;
            dataGridViewCustomers.Refresh();
        }

        // ORDER CUSTOMERS
        private void orderCustomers(string n)
        {
            if (orderCol)
            {
                switch (n)
                {
                    case "id_Client":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.id_Client).ToList();
                        break;
                    case "Nom":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Nom).ToList();
                        break;
                    case "Cognom1":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Cognom1).ToList();
                        break;
                    case "Adreca":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Adreca).ToList();
                        break;
                    case "Poblacio":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Poblacio).ToList();
                        break;
                    case "Provincia":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Provincia).ToList();
                        break;
                    case "Telefon":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Telefon).ToList();
                        break;
                    case "Fax":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Fax).ToList();
                        break;
                    case "E-mail":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.Email).ToList();
                        break;
                    case "Codi_Postal":
                        dataGridView1.DataSource = context.Customers.OrderByDescending(o => o.CodiPostal).ToList();
                        break;
                }
                orderCol = false;
            }
            else
            {
                switch (n)
                {
                    case "id_Client":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.id_Client).ToList();
                        break;
                    case "Nom":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Nom).ToList();
                        break;
                    case "Cognom1":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Cognom1).ToList();
                        break;
                    case "Adreca":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Adreca).ToList();
                        break;
                    case "Poblacio":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Poblacio).ToList();
                        break;
                    case "Provincia":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Provincia).ToList();
                        break;
                    case "Telefon":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Telefon).ToList();
                        break;
                    case "Fax":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Fax).ToList();
                        break;
                    case "E-mail":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.Email).ToList();
                        break;
                    case "Codi_Postal":
                        dataGridView1.DataSource = context.Customers.OrderBy(o => o.CodiPostal).ToList();
                        break;
                }
                orderCol = true;
            }
        }

        /* TAB INVOICE*/
        // ADD INVOICE
        private void btAddInvoice_Click(object sender, EventArgs e)
        {
            try {
                cmbRefresh();
                using (ctxInvoice = new CompanyCRMContext())
                {
                    var invoice = ctxInvoice.Set<Factura>();
                    invoice.Add(new Factura
                    {
                        n_Factura = int.Parse(textBoxAddInvoiceIDInvoice.Text),
                        id_Client = valcmbInvoice_Customer,
                        Data = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                        Descompte = int.Parse(textBoxAddInvoiceDisccount.Text),
                        IVA = int.Parse(textBoxAddInvoiceIVA.Text),
                    });
                    ctxInvoice.SaveChanges();
                    refreshInvoice();
                    cmbRefresh();
                    updateLog(log.showLog() + "INVOICE ADDED" + enter, log.showLog() + "INVOICE ADDED"); 
                }
            }
            catch (Exception) { MessageBox.Show("Error AddInvoice"); }
        }

        // SAVE INVOICES
        private void dataGridViewInvoice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewInvoice.EndEdit();
                ctxInvoice.SaveChanges();
                refreshInvoice();
                updateLog(log.showLog() + "INVOICE SAVED" + enter, log.showLog() + "INVOICE SAVED");
            }
            catch (Exception) { MessageBox.Show("Error SaveInvoice"); };
        }

        // DELETE INVOICE
        private void btDeleteInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                cmbRefresh();
                ctxInvoice = new CompanyCRMContext();
                var invoice = new Factura { n_Factura = valcmbDelInvoice_Invoice, id_Client = valcmbDelInvoice_Customer };
                ctxInvoice.Entry(invoice).State = EntityState.Deleted;
                ctxInvoice.SaveChanges();
                dataGridViewInvoice.Refresh();
                refreshInvoice();
                cmbRefresh();
                updateLog(log.showLog() + "INVOICE DELETED" + enter, log.showLog() + "INVOICE DELETED");
            }
            catch (Exception) { MessageBox.Show("Error DeleteInvoice"); }
        }

        // LOAD COMBOBOX INVOICE CUSTOMERS
        private void cmbInvoice()
        {
            cmbInvoice_Customer.Items.Clear();
            ctxInvoice = new CompanyCRMContext();
            var customers = ctxInvoice.Customers.ToList();
            foreach (var cust in customers) cmbInvoice_Customer.Items.Add(cust.id_Client);
        }

        // SAVE ACTUAL INDEX
        private void cmbInvoice_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbInvoice_Customer = int.Parse(cmbInvoice_Customer.SelectedItem.ToString());
        }

        private void cmbDeleteInvoiceInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbDelInvoice_Invoice = int.Parse(cmbDeleteInvoiceInvoice.SelectedItem.ToString());
        }

        private void cmbDeleteInvoiceCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbDelInvoice_Customer = int.Parse(cmbDeleteInvoiceCustomer.SelectedItem.ToString());
        }

        // SELECT ACTUAL ROWS
        private void dataGridViewInvoice_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewInvoice.SelectedRows.Count > 0)  
            {
                cmbDeleteInvoiceInvoice.Text = dataGridViewInvoice.SelectedRows[0].Cells[0].Value + string.Empty;
                cmbDeleteInvoiceCustomer.Text = dataGridViewInvoice.SelectedRows[0].Cells[1].Value + string.Empty;
            }
        }

        // LOAD COMBOBOX INVOICE DELETE INVOICE
        private void cmbInvoiceDelInvoice()
        {
            cmbDeleteInvoiceInvoice.Items.Clear();
            ctxInvoice = new CompanyCRMContext();
            var invoices = ctxInvoice.Invoice.ToList();
            foreach (var cust in invoices) cmbDeleteInvoiceInvoice.Items.Add(cust.n_Factura);

        }

        // LOAD COMBOBOX INVOICE DELETE CUSTOMER
        private void cmbInvoiceDelCustomer()
        {
            cmbDeleteInvoiceCustomer.Items.Clear();
            ctxCustomers = new CompanyCRMContext();
            var customers = ctxInvoice.Customers.ToList();
            foreach (var cust in customers) cmbDeleteInvoiceCustomer.Items.Add(cust.id_Client);
        }

        // REFRESH TABLE INVOICE
        private void refreshInvoice()
        {
            ctxInvoice = new CompanyCRMContext();
            biInvoice.DataSource = ctxInvoice.Invoice.ToList();
            dataGridViewInvoice.DataSource = biInvoice;
            dataGridViewInvoice.Refresh();
        }

        // BUTTON THAT CALL FUNCTION EXPORTXML INVOICE
        private void btExportPDFInvoice_Click(object sender, EventArgs e)
        {
            if (user.Role == "write" || user.Role == "admin") exportPDFInvoice();
        }

        // EXPORT INVOICE TO PDF
        private void exportPDFInvoice()
        {
            try
            {
                //Header
                PdfPTable headerTable = new PdfPTable(2);
                Paragraph title = new Paragraph(cmbBoxValue, FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                title.Alignment = Element.ALIGN_LEFT;
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("logo.png");
                img.ScaleToFit(105f, 105f);
                img.Alignment = Element.ALIGN_RIGHT;
                headerTable.DefaultCell.FixedHeight = 50f;
                headerTable.AddCell(title);
                headerTable.AddCell(img);

                //Body
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                pdfTable.DefaultCell.Padding = 3;
                pdfTable.HeaderRows = 1;
                pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.DefaultCell.BorderWidth = 1;
                pdfTable.SpacingBefore = 5;

                //Adding Header row
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    pdfTable.AddCell(cell);
                }

                //Adding DataRow
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                        }
                    }
                }
                string folderPath = "";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderBrowserDialog.SelectedPath + "/";
                }

                //Exporting to PDF
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (FileStream stream = new FileStream(folderPath + cmbBoxValue + ".pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.SetMargins(30, 30, 30, 30);
                    pdfDoc.SetPageSize(new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.LETTER.Width, iTextSharp.text.PageSize.LETTER.Height));

                    pdfTable.SpacingBefore = 15f;
                    pdfDoc.Open();
                    pdfDoc.Add(headerTable);
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
            catch (Exception) { richTextBox1.Text += log.showLog() + "ERROR: PROBLEM EXPORT PDF" + enter; }

            updateLog(log.showLog() + "USER[" + user.Name + "] EXPORT INVOICE " + " TO PDF" + enter, log.showLog() + "USER[" + user.Name + "] EXPORT INVOICE " + " TO PDF");
        }

        // ORDERS INVOICE
        private void orderInvoice(string n)
        {
            if (orderCol)
            {
                switch (n)
                {
                    case "n_Factura":
                        dataGridView1.DataSource = context.Invoice.OrderByDescending(o => o.n_Factura).ToList();
                        break;
                    case "id_Client":
                        dataGridView1.DataSource = context.Invoice.OrderByDescending(o => o.id_Client).ToList();
                        break;
                    case "Data":
                        dataGridView1.DataSource = context.Invoice.OrderByDescending(o => o.Data).ToList();
                        break;
                    case "Descompte":
                        dataGridView1.DataSource = context.Invoice.OrderByDescending(o => o.Descompte).ToList();
                        break;
                    case "IVA":
                        dataGridView1.DataSource = context.Invoice.OrderByDescending(o => o.IVA).ToList();
                        break;
                }
                orderCol = false;
            }
            else
            {
                switch (n)
                {
                    case "n_Factura":
                        dataGridView1.DataSource = context.Invoice.OrderBy(o => o.n_Factura).ToList();
                        break;
                    case "id_Client":
                        dataGridView1.DataSource = context.Invoice.OrderBy(o => o.id_Client).ToList();
                        break;
                    case "Data":
                        dataGridView1.DataSource = context.Invoice.OrderBy(o => o.Data).ToList();
                        break;
                    case "Descompte":
                        dataGridView1.DataSource = context.Invoice.OrderBy(o => o.Descompte).ToList();
                        break;
                    case "IVA":
                        dataGridView1.DataSource = context.Invoice.OrderBy(o => o.IVA).ToList();
                        break;
                }
                orderCol = true;
            }
        }

        /* TAB INVOICE-DETAILS */
        // ADD INVOICE-DETAIL
        private void btAddInvoiceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                cmbRefresh();
                using (ctxInvoiceDetails = new CompanyCRMContext())
                {
                    var invoiceDetails = ctxInvoiceDetails.Set<Factura_Detail>();
                    invoiceDetails.Add(new Factura_Detail
                    {
                        n_Factura = valcmbAddDetailID_Invoice,
                        id_Producte = valcmbAddDetailID_Product,
                        quantitat = int.Parse(textBoxAddInvoiceDetailsQuantity.Text),
                    });
                    ctxInvoiceDetails.SaveChanges();
                    refreshInvoiceDetails();
                    cmbRefresh();
                    updateLog(log.showLog() + "INVOICE-DETAILS ADDED" + enter, log.showLog() + "INVOICE ADDED");
                }
            }
            catch (Exception) { MessageBox.Show("Error AddInvoice-Details"); }
        }

        // SAVE INVOICES-DETAILS
        private void dataGridViewInvoiceDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewInvoiceDetails.EndEdit();
                ctxInvoiceDetails.SaveChanges();
                refreshInvoiceDetails();
                updateLog(log.showLog() + "INVOICE_DETAILS SAVED" + enter, log.showLog() + "INVOICE_DETAILS SAVED");
            }
            catch (Exception) { MessageBox.Show("Error SaveInvoice-Details"); };
        }

        // DELETE INVOICE-DETAIL
        private void btDeleteInvoiceDetails_Click(object sender, EventArgs e)
        {
             try
            {
                refreshInvoiceDetails();
                cmbRefresh();
                ctxInvoiceDetails = new CompanyCRMContext();
                var invoiceDetails = new Factura_Detail { n_Factura = valcmbDelDetailID_Invoice, id_Producte = valcmbDelDetailID_Product };
                ctxInvoiceDetails.Entry(invoiceDetails).State = EntityState.Deleted;
                ctxInvoiceDetails.SaveChanges();
                dataGridViewInvoiceDetails.Refresh();
                refreshInvoiceDetails();
                cmbRefresh();
                updateLog(log.showLog() + "INVOICE DELETED" + enter, log.showLog() + "INVOICE DELETED");
            }
            catch (Exception) { MessageBox.Show("Error DeleteInvoice"); }
        }

        // REFRESH TABLE INVOICE
        private void refreshInvoiceDetails()
        {
            ctxInvoiceDetails = new CompanyCRMContext();
            biInvoiceDetails.DataSource = ctxInvoiceDetails.Invoice_Detail.ToList();
            dataGridViewInvoiceDetails.DataSource = biInvoiceDetails;
            dataGridViewInvoiceDetails.Refresh();
        }

        // LOAD COMBOBOX INVOICE-DETAILS ADD INVOICE
        private void cmbInvoiceDetailsAddInvoice()
        {
            cmbAddInvoice_DetailsID_Invoice.Items.Clear();
            ctxInvoice = new CompanyCRMContext();
            var invoices = ctxInvoice.Invoice.ToList();
            foreach (var cust in invoices) cmbAddInvoice_DetailsID_Invoice.Items.Add(cust.n_Factura);
        }

        // LOAD COMBOBOX INVOICE-DETAILS ADD CUSTOMER
        private void cmbInvoiceDetailsAddCustomer()
        {
            cmbAddInvoice_DetailsID_Product.Items.Clear();
            ctxProducts = new CompanyCRMContext();
            var products = ctxProducts.productsItem.ToList();
            foreach (var cust in products) cmbAddInvoice_DetailsID_Product.Items.Add(cust.id_Producte);
        }

        // LOAD COMBOBOX INVOICE-DETAILS DELETE INVOICE
        private void cmbInvoiceDetailsDelInvoice()
        {
            cmbDeleteInvoice_DetailsID_Invoice.Items.Clear();
            ctxInvoice = new CompanyCRMContext();
            var invoices = ctxInvoice.Invoice.ToList();
            foreach (var cust in invoices) cmbDeleteInvoice_DetailsID_Invoice.Items.Add(cust.n_Factura);
        }

        // LOAD COMBOBOX INVOICE-DETAILS DELETE CUSTOMER
        private void cmbInvoiceDetailsDelCustomer()
        {
            cmbDeleteInvoice_DetailsID_Product.Items.Clear();
            ctxProducts = new CompanyCRMContext();
            var products = ctxProducts.productsItem.ToList();
            foreach (var cust in products) cmbDeleteInvoice_DetailsID_Product.Items.Add(cust.id_Producte);
        }

        // SAVE ACTUAL INDEX   
        private void cmbAddInvoice_DetailsID_Invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbAddDetailID_Invoice = int.Parse(cmbAddInvoice_DetailsID_Invoice.SelectedItem.ToString());
        }

        private void cmbAddInvoice_DetailsID_Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbAddDetailID_Product = int.Parse(cmbAddInvoice_DetailsID_Product.SelectedItem.ToString());
        }

        private void cmbDeleteInvoice_DetailsID_Invoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbDelDetailID_Invoice = int.Parse(cmbDeleteInvoice_DetailsID_Invoice.SelectedItem.ToString());
        }

        private void cmbDeleteInvoice_DetailsID_Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            valcmbDelDetailID_Product = int.Parse(cmbDeleteInvoice_DetailsID_Product.SelectedItem.ToString());
        }

        // ORDERS INVOICE
        private void orderInvoiceDetails(string n)
        {
            if (orderCol)
            {
                switch (n)
                {
                    case "n_Factura":
                        dataGridView1.DataSource = context.Invoice_Detail.OrderByDescending(o => o.n_Factura).ToList();
                        break;
                    case "id_Producte":
                        dataGridView1.DataSource = context.Invoice_Detail.OrderByDescending(o => o.id_Producte).ToList();
                        break;
                    case "quantitat":
                        dataGridView1.DataSource = context.Invoice_Detail.OrderByDescending(o => o.quantitat).ToList();
                        break;
                }
                orderCol = false;
            }
            else
            {
                switch (n)
                {
                    case "n_Factura":
                        dataGridView1.DataSource = context.Invoice_Detail.OrderBy(o => o.n_Factura).ToList();
                        break;
                    case "id_Producte":
                        dataGridView1.DataSource = context.Invoice_Detail.OrderBy(o => o.id_Producte).ToList();
                        break;
                    case "quantitat":
                        dataGridView1.DataSource = context.Invoice_Detail.OrderBy(o => o.quantitat).ToList();
                        break;
                }
                orderCol = true;
            }

        }

        // REFRESH ALL CMB FOR EVERY CHANGE
        private void cmbRefresh()
        {
            cmbUserLogin();
            cmbProducts();
            cmbCustomers();
            cmbInvoice();
            cmbInvoiceDelInvoice();
            cmbInvoiceDelCustomer();
            cmbInvoiceDetailsAddInvoice();
            cmbInvoiceDetailsAddCustomer();
            cmbInvoiceDetailsDelInvoice();
            cmbInvoiceDetailsDelCustomer();
        }
    }
}
//https://social.microsoft.com/Forums/en-US/e330ea7e-cadb-4bd8-b956-8ba0113df52d/how-to-make-column-headers-in-table-in-pdf-report-appear-bold-while-datas-in-table-appear-regular?forum=Offtopic