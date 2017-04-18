using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Program Organization
/// 
/// Class Program
///     Manages operation of the entire program.... v.v. the procedural aspects
///     
/// Class FileOps
///     File Operations & Methods
///     means of creating a file handler, opening it etc.
/// 
/// Class AssemblerProcessor
///     parse the file twice
///     
///     
/// </summary>

namespace CreateAssemblyFile
{
    class program
   {
        [STAThread]
        static void Main(string[] args)
        {
            string filename;
            filename = "";
            List<string> outputLines = new List<string>();
            FileOps assemblerfile = new FileOps();

            // Get Filename

            while (filename == "")
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("No arguments, need to source them");
                    Console.WriteLine("Please enter the filename of the file you want to read in");

                    // filename = Console.ReadLine();
                    filename = "max/Max.asm";
                    filename = "pong/Pong.asm";
                    filename = "test.asm";


                    // Open file dialog approach from: http://stackoverflow.com/questions/15270387/browse-for-folder-in-console-application
                    // but changed to the OpenFileDialog 

                    OpenFileDialog fileSelectPopUp = new OpenFileDialog();

                    fileSelectPopUp.Title = "Select ASM File";
                    fileSelectPopUp.ValidateNames = false;
                    fileSelectPopUp.Filter = "All ASM FILES (*.asm)|*.asm|All files (*.*)|*.*";
                    fileSelectPopUp.RestoreDirectory = true;
                    if (fileSelectPopUp.ShowDialog() == DialogResult.OK)
                    {
                        filename = fileSelectPopUp.FileName;
                    }
                    //FileDialog fbd = new FileDialog();
                    //FolderBrowserDialog fbd = new FolderBrowserDialog();
                    //if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                        //filename = fbd.ToString();
                        /*
                        System.IO.StreamReader sr = new
                           System.IO.StreamReader(openFileDialog1.FileName);
                        MessageBox.Show(sr.ReadToEnd());
                        sr.Close();
                        */
                    //}
                    if (filename.Length==0)
                    {
                        Console.WriteLine("No seriously give me a filename!");
                        filename = Console.ReadLine();
                    }
                }
                else
                {
                    filename = args[0];
                    Console.WriteLine(args[0]);
                }

            }


            List<string> assemblerInput = new List<string>();
            assemblerInput = assemblerfile.readFile(filename, "assembler");

            // create Assembler instance

            // do assembler pass1
            AssemblerProcessor assembleHackFile = new AssemblerProcessor(assemblerInput);
            assembleHackFile.ParseOne();

            // Do assembler pass2
            assembleHackFile.ParseTwo();
            outputLines = assembleHackFile.outputfile;
            
            // stopping point
            // todo
            /* need to convert to binary using :
             * Convert.ToString(MyVeryOwnByte, 2).PadLeft(8, '0');
             * 
             * before writing out binary values.
             * which isn't done yet.
             * */

            // Create outputfile.hack
            FileOps hackfile = new FileOps();
            hackfile.writeFile(filename, "hack", outputLines);


            //Console.ReadLine();
            Console.WriteLine("End of Program, press Enter to End");
            Console.ReadLine();

        }
    }
}
