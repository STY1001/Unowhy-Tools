using System.ServiceProcess;

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
