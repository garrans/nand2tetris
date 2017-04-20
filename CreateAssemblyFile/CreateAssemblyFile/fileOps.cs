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
            int filenamelength = stringfileName.LastIndexOf(".");
            Console.Write("fileOps.openfile ");
            stringfileName = stringfileName.Substring(0, filenamelength);

            string newfilename = stringfileName + "." + filetype;
            
            Console.WriteLine(newfilename);

            // Create a string array with the lines of text
            using (StreamWriter outputFile = new StreamWriter(newfilename))
                foreach (string line in outputfile)
                    outputFile.WriteLine(line);

//            // Sample write file using streamwriter
//            string[] lines = { "First line", "Second line", "Third line" };
//
//            // Set a variable to the My Documents path.
//            string mydocpath =
//                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
//
//           // Write the string array to a new file named "WriteLines.txt".
//            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\WriteLines.txt"))
//           {
//              foreach (string line in lines)
//                 outputFile.WriteLine(line);
//            }

            }

        [STAThread]
        public List<string> readFile(string stringfileName, string filetype)
        {
            //string[] filestring = { };   tried this first to no avail, found that Lists were better! :-))

            List<string> filelines = new List<string>();
            // while ((line = file.ReadLine()) != null) // encapsulates the book’s "hasMoreCommands" and "advance"
            //    string[] filestring = { "hello", "how", "are", "you" };

            Console.WriteLine("fileOps.Readfile {0}",stringfileName);

            try
            {
                if (File.Exists(stringfileName))
                {

                    using (StreamReader sr = new StreamReader(stringfileName))
                    {
                        int i = 0;
                        while (sr.Peek() >= 0)
                        {
                            string temp = StripWhiteSpace(sr.ReadLine());
                            if (temp.Length>0)
                            {
                                filelines.Add(temp);
                                Console.WriteLine(filelines[i]);
                                i = i + 1;
                            }
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


        public static string StripWhiteSpace(string codeline)
        {
            // Strip comments and white space.
            char[] outTemp = new char[codeline.Length]; // need a char array since strings are read-only
                                                    // j is our non-whitespace index
            int j = 0;
            for (int i = 0; i < codeline.Length; i++)
            {
                if (codeline[i] == '/')
                {
                    if (i == codeline.Length - 2) // have to do this first so we don't get an out of bounds
                                              //error if the "//" is at the end of the codeline
                    {
                        // end of codeline "//"; done with this codeline
                        break;
                    }
                    else if (codeline[i + 1] == '/')
                    {
                        // not at end of codeline but still a comment, so ignore the rest of the codeline
                        break;
                    }
                } // not a comment, keep going with this codeline
                if (!char.IsWhiteSpace(codeline, i))
                {
                    outTemp[j] = codeline[i]; // only copy if it's not whitespace
                    j++;
                }
            }
            string temp = new string(outTemp);
            temp = temp.Substring(0, j);
            return temp;
        }
    }
}
