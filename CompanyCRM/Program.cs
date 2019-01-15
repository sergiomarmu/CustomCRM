using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace CompanyCRM
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            User n = new User();
            n.Id = "1";
            n.Name = "PEPE";
            n.Password = "12345";
            n.Role = "admin";
            Application.Run(new Main(n,"Hi admin"));
        }
    }
}
