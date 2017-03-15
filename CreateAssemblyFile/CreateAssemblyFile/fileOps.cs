using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAssemblyFile
{
    class FileOps
    {
        
        public FileOps()
        //initialize fileOps
        {
            Console.WriteLine("FileOps Initialize");

        }
        //open file for read
        public void writeFile(string stringfileName, string filetype, string[] outputfile)
        {
            Console.WriteLine("fileOps.openfile");
            Console.WriteLine(stringfileName);
            //for each string in outputfile[]
                // write string
            //endfor
           
        }
        
        public string[] readFile(string stringfileName, string filetype)
        {
            string[] filestring = { "hello", "how", "are", "you" };
            Console.WriteLine("fileOps.openfile");
            Console.WriteLine(stringfileName);
            return filestring;

        }
        
        //read line
        //close file
        //open file for write
        //write line


       //` public static fileOps assemblerfile { get; private set; }


    }
}
