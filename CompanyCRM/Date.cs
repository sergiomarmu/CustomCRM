using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyCRM
{
    class Date
    {
        public Date()
        {

        }
        public string showLog()
        {
            return "[" + DateTime.Now.ToShortDateString() +
                     " - " + DateTime.Now.ToString("HH:mm:ss tt") + "] -- ";
        } 
    }
}
