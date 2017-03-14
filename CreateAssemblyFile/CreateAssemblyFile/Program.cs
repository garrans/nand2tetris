using System;
using System.Collections.Generic;
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
/// Class Assembler
///     parse the file twice
///     
///     
/// </summary>

namespace CreateAssemblyFile
{
    class program
   {
        enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND }; 
        static void Main(string[] args)
        {
            string filename;
            filename = "";
            string[] outputstrings = { };


            fileOps assemblerfile = new fileOps();

            // Get Filename

            while (filename == "")
            {

                if (args.Length == 0)
                {
                    Console.WriteLine("No arguments, need to source them");
                    Console.WriteLine("Please enter the filename of the file you want to read in");
                    filename = Console.ReadLine();
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
            assemblerfile.readFile(filename, "assembler");

            // create Assembler instance
             
            // do assembler pass1
            // Do assembler pass2
            
            // Create outputfile.hack
            fileOps hackfile = new fileOps();
            hackfile.writeFile(filename, "hack", outputstrings);


            Console.ReadLine();
            Console.WriteLine("End of Program, press Enter to End");
            Console.ReadLine();

        }
    }
}
