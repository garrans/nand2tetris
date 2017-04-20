using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JackCompiler
{

    class FileOps
    {

        string filename;
        string workingDirectory;

        public FileOps()
        //initialize fileOps
        {
            // how to determine who called you so we can write it in the messages?
            Console.WriteLine("FileOps Initialize");
        }


        public List<string> readFile(string stringfileName, string filetype)
        {
            //string[] filestring = { };   tried this first to no avail, found that Lists were better! :-))

            List<string> filelines = new List<string>();

            Console.WriteLine("    fileOps.Readfile {0}", stringfileName);
            try
            {
                if (File.Exists(stringfileName))
                {
                    using (StreamReader sr = new StreamReader(stringfileName))
                    {
                        int i = 0;
                        while (sr.Peek() >= 0)
                        {
                            string temp = sr.ReadLine();
                            filelines.Add(temp);
                            Console.WriteLine("          {0}", filelines[i]);
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

        //open file for writing
        public void writeNewFile(string stringfileName, string filetype, List<string> outputfile)
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
        }


        //[STAThread]
        public FileInfo[] getAndReadDirectory(string directory)
        //public List<string> getAndReadDirectory(string directory)
        {
            
            List<string> filelines = new List<string>();

            // Open file dialog approach from: http://stackoverflow.com/questions/15270387/browse-for-folder-in-console-application
            // but changed to the OpenFileDialog 
            OpenFileDialog fileSelectPopUp = new OpenFileDialog();

            fileSelectPopUp.Title = "Select a single .jack file in the directory to compile, all in dir will be compiled";
            fileSelectPopUp.ValidateNames = false;
            fileSelectPopUp.Filter = "All .jack FILES (*.jack)|*.jack|All files (*.*)|*.*";
            fileSelectPopUp.RestoreDirectory = true;
            if (fileSelectPopUp.ShowDialog() == DialogResult.OK)
            {
                filename = fileSelectPopUp.FileName;
                int lastSlash = filename.LastIndexOf(@"\");
                directory = filename.Substring(0, lastSlash);
                workingDirectory = directory;
            }

            Console.WriteLine("fileOps.readDirectoryList {0}",directory );

            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] Files = d.GetFiles("*.jack"); //Getting Text files
            return Files;
        }

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
