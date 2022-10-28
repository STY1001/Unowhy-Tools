using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unowhy_Tools
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>

        public class Options
        {
            [Option('u', "user", Required = false, HelpText = "User profile")]
            public string userpath { get; set; }
        }

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.userpath == null)
                       {
                           string useridpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                           Application.Run(new main(useridpath));
                       }
                       else
                       {
                           Application.Run(new main(o.userpath));
                       }
                   });
        }
    }
}
