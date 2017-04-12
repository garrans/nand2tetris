using System;
using System.Collections;
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
        // public List<string>outputfile = new List<string>();
        public enum Parser_CommandType { Parser_NO_COMMAND = 0, Parser_A_COMMAND, Parser_C_COMMAND, Parser_L_COMMAND };

        public AssemblerProcessor(List<string> filecontents)
        {
            int i = 0;
            outputfile = new List<string>();
            this.filecontents = filecontents;
            i = filecontents.Count;
            Console.WriteLine("There are {0} lines in the input file", i);

            /*
            foreach (string line in filecontents)
            {
                // Console.WriteLine(line);
                i += 1;
            } 
            */
        }

        public Parser_CommandType command_type(string line)
        {

            char firstchar = line[0];

            if (firstchar == '@')
            {
                //Console.Write("A Command");
                return Parser_CommandType.Parser_A_COMMAND;
            }
            else
            {
                if (line.Contains('=') | line.Contains(";") | line.Contains("M") | line.Contains("D") | line.Contains("A") | IsDigitsOrDashOnly(line))
                    {
                        //Console.Write("C Command");
                        return Parser_CommandType.Parser_C_COMMAND;
                    }
                else
                {
                    //Console.Write("no command");
                    return Parser_CommandType.Parser_NO_COMMAND;
                }
            }
            
            /*
            switch (firstchar)
            {
                case '@':
                    Console.Write("A Command");
                    return Parser_CommandType.Parser_A_COMMAND;
                case '0':
                    Console.Write("C Command");
                    return Parser_CommandType.Parser_C_COMMAND;
                default:
                    Console.Write("no command");
                    return Parser_CommandType.Parser_NO_COMMAND;
            }
            */

        }

        public void ParseOne()
        {
            int romaddr = 0;

            Console.WriteLine("ParseOne");

            foreach (string line in filecontents)
            {
                if (line.Contains("("))
                {
                    string substring = line.Substring(1, line.IndexOf(")")-1);
                    Console.WriteLine("Found label: {0} at ROM Address {1}", substring, romaddr);
                    SymbolTable.addEntry(substring, romaddr);
                }
                else
                {
                    romaddr += 1;
                }
            }
            foreach (string key in SymbolTable.symbolTable.Keys) 
            {
                Console.WriteLine("{0}: {1}", key, SymbolTable.symbolTable[key]);
            }
/*            foreach (string item in SymbolTable)
            {
                Console.WriteLine("SymbolTable");

            }*/
        }

        public void ParseTwo()
        {
            Console.WriteLine(" **************      ParseTwo");
            // moved following line to class level
            // List<int> outputfile = new List<int>();
            int ramaddr = 15;
            int romaddr = 0;

            // assemble the instruction as an integer.
            // n |= compValue | destValue | jumpValue;
            // convert the integer to text represt

            foreach (string line in filecontents)
            {
                Console.Write("ROM Address: {0} is an ", romaddr);
                switch (command_type(line))
                {
                    case Parser_CommandType.Parser_A_COMMAND:
                        int ramaddr_out = 0;
                        outputfile.Insert(outputfile.Count, ParseACommand(line, ramaddr, out ramaddr_out));
                        ramaddr = ramaddr_out;
                        Console.WriteLine();
                        break;

                    case Parser_CommandType.Parser_C_COMMAND:
                        Console.Write("C Command: ");
                        Console.WriteLine(line);
                        outputfile.Insert(outputfile.Count, ParseCCommand(line));
                        break;

                    case Parser_CommandType.Parser_L_COMMAND:
                        Console.Write("L Command: ");
                        Console.Write("***************** ERROR ERROR ********************** ");
                        Console.WriteLine(line);
                        break;

                    case Parser_CommandType.Parser_NO_COMMAND:
                        Console.Write("No Command");
                        Console.WriteLine(line);
                        break;

                    default:
                        break;
                }

                ++romaddr;
            }

        }

        public static int CodeTranslate(string codeline)
        {
            //translate the passed in Code
            Console.WriteLine(codeline);
            return 0;
        }

        private static string ParseCCommand(string line)
        {
            int outputCommand = 0; // int rep of binary command
            int compValue = 0;
            int destValue = 0;
            int jumpValue = 0;
            char[] splitters = { '=', ';' };
            string[] splitLine;
            string jumpString = "";
            string compString = "";
            bool dest = false;
            //bool jump = false;

            splitLine = line.Split(splitters);
            if (line.Contains('='))
            {
                dest = true;
                CTables.destTable.TryGetValue(splitLine[0],out destValue);
                destValue = destValue << 3;
            }
            else
            {
                compString = splitLine[0];
            }
            if (line.Contains(';'))
            {
                //jump = true;
                if (dest)
                {
                    jumpString = splitLine[2];
                    compString = splitLine[1];
                }
                else
                {
                    jumpString = splitLine[1];
                    compString = splitLine[0];
                }
                CTables.jumpTable.TryGetValue(jumpString,out jumpValue);
            }
            
            CTables.compTable.TryGetValue(compString, out compValue);
            compValue = compValue << 6;

            outputCommand = 0xE000; // The first 3 bits are 1
            outputCommand |= compValue | destValue | jumpValue;

            return Convert.ToString(outputCommand, 2).PadLeft(8, '0');
        }

        public static string ParseACommand(string line, int ramaddr, out int ramaddr_out)
        {

            ramaddr_out = ramaddr;
            int byteout;
            Console.Write("A Command ");
            //is it symbol or numeric?

            if (line[1] == '-')
            {
                //is negative therefore an actual number
                byteout = Convert.ToInt16(line);
                line = Convert.ToString(byteout, 2).PadLeft(16, '0');
                Console.WriteLine("Number {0} is binary {1} ", byteout, line);
            }
            else
            {
                if (IsDigitsOnly(line.Substring(1, line.Length - 1)))
                {
                    //actual number
                    byteout = Convert.ToInt16(line.Substring(1, line.Length - 1));
                    line = Convert.ToString(byteout, 2).PadLeft(16, '0');
                    Console.WriteLine("Number {0} is binary {1} ", byteout, line);
                }
                else
                {
                    // its a symbol
                    // check if in Symboltable, if not then add it as a RAM item
                    // then write out the ram value
                    int address = 0;
                    string symbol = line.Substring(1, line.Length - 1);
                    if (SymbolTable.symbolTable.ContainsKey(symbol))
                    {
                        SymbolTable.symbolTable.TryGetValue(symbol, out address);
                    }
                    else
                    {
                        SymbolTable.addEntry(symbol, ramaddr);
                        //  add 1 to RAM
                        ramaddr = ramaddr_out + 1;
                    }
                    //byteout = SymbolTable.symbolTable.TryGetValue(symbol, out address);
                    byteout = Convert.ToInt16(address);
                    line = Convert.ToString(byteout, 2).PadLeft(16, '0');
                    Console.WriteLine("Number {0} is at address {1} binary {2} ", symbol, byteout, line);
                }
            }
            return line;
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                        return false;
                }
            }
            return true;
        }
        static bool IsDigitsOrDashOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    if (c != '-')
                        return false;
                }
            }
            return true;
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
        public void Test()
        {
            Console.WriteLine("CTables.Test");
        }

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


