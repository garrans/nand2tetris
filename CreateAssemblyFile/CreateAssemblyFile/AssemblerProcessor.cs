using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAssemblyFile
{
    class AssemblerProcessor
    {
        public string[] filecontents;
        public string[] outputfile;
        enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND }; 

        
        public AssemblerProcessor(string[] filecontents)
        {
            this.filecontents = filecontents;
            foreach (string line in filecontents)
            {
                Console.WriteLine(line);
            }
        }
        public void ParseOne()
        {
            Console.WriteLine("ParseOne");
        }

        public void ParseTwo()
        {
            Console.WriteLine("ParseTwo");
            outputfile = filecontents;
        }

    }
}
