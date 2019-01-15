using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCRM
{
    public class User
    {
        string id;
        string name;
        string password;
        string role;

        public User(){ }
        public User(string id, string name, string password, string role)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.role = role;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get{return name;}
            set{name = value;}
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}
