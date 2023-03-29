using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Unowhy_Tools_Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var main = new Main())
            {
                ServiceBase.Run(main);
            }
        }
    }
}
