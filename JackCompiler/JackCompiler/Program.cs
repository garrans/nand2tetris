using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace JackCompiler
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // List<string> filesToRead = new List<string>();
            FileOps inputfile = new FileOps();
            FileInfo[] filesToRead =  inputfile.getAndReadDirectory("");

            foreach (FileInfo fileitem in filesToRead)
            {
                Console.WriteLine("    File is: {0}", fileitem.Name);

            }


            // do compiler pass1

            // Do compiler pass2
            
            //Console.ReadLine();
            Console.WriteLine("End of Program, press Enter to End");
            Console.ReadLine();

        }
    }
}
