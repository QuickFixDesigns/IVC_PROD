using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    class Program
    {

        static void Main(string[] args)
        {
            Maintenance.ResetDb();
            Admin.LoadDataBase();
            Projects.LoadDataBase();
            Refresh.UpdateProjectHeader();
        }
    }
}
