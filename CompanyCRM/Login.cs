using CompanyCRM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Login : Form
	{
        CompanyCRMContext context = new CompanyCRMContext();
        Boolean check;
        String role = "";
        User user;

        public Login()
		{
			InitializeComponent();
		}

		private void login_button_Click(object sender, EventArgs e)
		{

			if (check_user())
			{
				Main mainProgram = new Main(user,"Hi "+user.Name);
				mainProgram.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("User or Password Incorrect");
            }
		}

		private Boolean check_user()
		{
            ArrayList users = new ArrayList();
            check = false;
            context = new CompanyCRMContext();
            var log = context.Login.ToList();
            foreach (var cust in log)
            {
                if (textBox_user.Text.Equals(cust.user.ToString()))
                {
                    if(textBox_pass.Text.Equals(cust.password.ToString())) {
                        role = cust.role;
                        check = true;
                         user = new User(cust.id.ToString(),cust.user,cust.password, cust.role);
                        break;
                    }
                } else
                {
                    check = false;
                }
            }
            return check;
		}
	}
}
