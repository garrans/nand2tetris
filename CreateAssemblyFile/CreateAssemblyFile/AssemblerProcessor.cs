using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAssemblyFile
{
    class AssemblerProcessor
    {
        
        public List<string> filecontents;
        public List<string> outputfile;
        enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND };

        public AssemblerProcessor(List<string> filecontents)
        {
            this.filecontents = filecontents;
            int i = 0;
            foreach (string line in filecontents)
            {
                // Console.WriteLine(line);
                i += 1;

            }

            Console.WriteLine("There are {0} lines in the input file", i);
        }
        public void ParseOne()
        {
            Console.WriteLine("ParseOne");
            foreach (string line in filecontents)
            {
                Console.WriteLine(line);
                SymbolTable.addEntry()

            }
        }
        public void ParseTwo()
        {
            Console.WriteLine("ParseTwo");
            // assemble the instruction as an integer.
            // n |= compValue | destValue | jumpValue;
            // convert the integer to text represt
            outputfile = filecontents;

        }

        public static int CodeTranslate(string codeline)
        {
            //translate the passed in Code
            Console.WriteLine(" ");
            return 0;
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

    public class CTables
    {
        public static Dictionary<string, int> compTable = new Dictionary<string, int>()
        {
            // a= 0
            { "0", 0x2a},
            {"1", 0x3f},
            {"-1", 0x3a},
            {"D", 0x0c},
            {"!D", 0x0d},
            {"-D", 0x0f},
            {"D+1", 0x1f},
            {"1+D", 0x1f},

            {"D&A", 0x00},
            {"A&D", 0x00},
            {"D|A", 0x15},
            {"A|D", 0x15},
            // a = 1
            {"M", 0x30+0x40},
            {"!M", 0x31+0x40},
            {"-M", 0x33+0x40},
            {"M+1", 0x37+0x40},
            {"1+M", 0x37+0x40},
            {"M-1", 0x32+0x40},
            //
            {"D|M", 0x15+0x40},
            {"M|D", 0x15+0x40}
        };
    
    public static Dictionary<string, int> destTable = new Dictionary<string, int>()
        {
        // Allow all combinations of these letters (i.e., don't care about order);
        //this is not done in the book
            { "", 0},
            {"M", 1},
            {"D", 2},
            {"A", 4},
            {"MD", 3},
            {"DM", 3},
            {"AM", 5},
            {"MA", 5},
            {"AD", 6},
            {"DA", 6},
            {"ADM", 7},
            {"AMD", 7},
            {"DAM", 7},
            {"DMA", 7},
            {"MAD", 7},
            {"MDA", 7}
        };

        public static Dictionary<string, int> jumpTable = new Dictionary<string, int>()
        {
            {"", 0},
            {"JGT", 1},
            {"JEQ", 2},
            {"JGE", 3},
            {"JLT", 4},
            {"JNE", 5},
            {"JLE", 6},
            {"JMP", 7},
        };
    }
}


