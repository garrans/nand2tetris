using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
        //open file for writing
        public void writeFile(string stringfileName, string filetype, List<string> outputfile)
        {
            Console.WriteLine("fileOps.openfile");
            Console.WriteLine(stringfileName);
            //for each string in outputfile[]
                // write string
            //endfor
           
        }
        
        public List<string> readFile(string stringfileName, string filetype)
        {
            //string[] filestring = { };   tried this first to no avail, found that Lists were better! :-))

            List<string> filelines = new List<string>();
            // while ((line = file.ReadLine()) != null) // encapsulates the book’s "hasMoreCommands" and "advance"
            //    string[] filestring = { "hello", "how", "are", "you" };

            Console.WriteLine("fileOps.Readfile");
            Console.WriteLine(stringfileName);

            try
            {
                if (File.Exists(stringfileName))
                {

                    using (StreamReader sr = new StreamReader(stringfileName))
                    {
                        int i = 0;
                        while (sr.Peek() >= 0)
                        {
                            filelines.Add(sr.ReadLine());
                            Console.WriteLine(filelines[0]);
                            i = i + 1;
                        }
                    }
                }
                else
                {
                        Console.WriteLine("File not found");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            
            return filelines;
        }
        
        //read line
        //close file
        //open file for write
        //write line


       //` public static fileOps assemblerfile { get; private set; }


    }
}
