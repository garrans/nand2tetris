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
        public enum TokenType { KEYWORD = 0, SYMBOL, IDENTIFIER, INT_CONST, STRING_CONST, UNKNOWNTOKEN };
        public enum KeyWord
        {
            CLASS = 0, METHOD, FUNCTION, CONSTRUCTOR, INT, BOOLEAN, CHAR,
            VOID, VAR, STATIC, FIELD, LET, DO, IF, ELSE, WHILE, RETURN, TRUE, FALSE, NULL, THIS,
            UNKOWNKEYWORD
        } 

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

            Console.WriteLine("Starting processing files");
            
            // do compiler pass1
            foreach (FileInfo fileitem in filesToRead)
            {
                List<string> xmlTokens = new List<string>();
                List<string> semiParsed = new List<string>();
                Dictionary<int, string> openTokens = new Dictionary<int, string>();

                Console.WriteLine("    File is: {0}", fileitem.Name);
                // Compiler Pass 1
                FileOps currentFile = new FileOps();
                currentFile.readFile(fileitem.FullName, ".jack");
                bool multiLine = false;
                bool isMultiLine = false;

                foreach (string inputline in currentFile.readFile(fileitem.FullName, ".jack"))
                {
                    string currentline = inputline;             // allows reassignment of currentline if we need to
                    currentline = StripWhiteSpace(currentline, multiLine, out isMultiLine);
                    multiLine = isMultiLine;

                    if (isMultiLine)                           // Not middle of multiline comment
                    {
                        Console.Write("Multiline");              // First Step Tokenizer
                    }
                    if (currentline.Length > 0)
                    {

                        semiParsed.Add(currentline); // Add non blank entry to xml tokens
                        // check current line against a dictionary of symbols

                        foreach (string key in tokenTables.tokenTable.Keys)
                        {
                            string tempStr1;
                            Dictionary<int, string> tokensInLine = new Dictionary<int, string>();

                            while (currentline.Contains(key))
                            {
                                int index = currentline.IndexOf(key);
                                tokensInLine.Add(index, key);
                                //temp1 = currentline.Substring()
                                currentline = currentline.Substring(index, key.Length);
                                currentline.Insert(index, "#");
                                tempStr1 = "#" + key;
                                currentline.Replace(tempStr1, "#");
                            }
                            Console.Write("x");
                        }
                    }
                }
                FileOps outFile = new FileOps();
                outFile.writeNewFile(fileitem.FullName, "semiparsed", semiParsed);
                // Do compiler pass2
            
            }

            //Console.ReadLine();
            Console.WriteLine("End of Program, press Enter to End");
            Console.ReadLine();

        }

        public static string StripWhiteSpace(string codeline , bool multiLine, out bool isMultiLine )
        {
            // Strip comments and white space.
            isMultiLine = multiLine;

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
                        Console.Write("### COMMENT ###");
                        break;
                    }
                    else if (codeline[i + 1] == '/')
                    {
                        Console.Write("### COMMENT ###");
                        break;
                    }
                    else if ( codeline[i+1] =='*')
                    {
                        // not at end of codeline but still a comment, so ignore the rest of the codeline\
                        isMultiLine = true;
                        
                        for (int k = 0; k < codeline.Length; k++) // Check for end of block before reporting
                        {
                            if (codeline[k] == '*')
                           {
                                if (k == (codeline.Length - 1))
                                {
                                    Console.Write("### COMMENT ###");
                                    break;
                                }
                                else if (codeline[k+1] == '/') //if at the end of the row watch for  out of bounds
                                {
                                    isMultiLine = false;
                                    Console.Write("### COMMENT ###");
                                    break;
                                }
                            }
                        }
                        Console.Write("### COMMENT ###");
                        break;
                    }
                } // not a comment, check for end of comment block, otherwise keep going with this codeline
                else
                {
                    if (isMultiLine)
                    {
                        
                        if (codeline[i] == '*' )
                        {
                            if (i == (codeline.Length - 2))
                            {
                                if (codeline[i + 1] == '/')
                                {
                                    isMultiLine = false;
                                    Console.Write("### COMMENT ###");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (i == codeline.Length - 2) // have to do this first so we don't get an out of bounds
                                                          //error if the "//" is at the end of the codeline
                            {
                                // end of codeline "//"; done with this codeline
                                Console.Write("### COMMENT ###");
                                break;
                            }
                            if (codeline[i]=='*' & codeline[i+1]=='/')
                            {
                                isMultiLine = true;
                                Console.Write("### COMMENT ###");
                                break;
                            }
                        }
                    }
                }

                
                if (!char.IsWhiteSpace(codeline, i) & !isMultiLine)
                {
                    outTemp[j] = codeline[i]; // only copy if it's not whitespace
                    j++;
                }
            }
            string temp = new string(outTemp);
            temp = temp.Substring(0, j);
            return temp;
        }

        public TokenType tokenType(string line)
        {

            char firstchar = line[0];

            if (firstchar == '@')
            {
                //
                return TokenType.KEYWORD;
            }
            else
            {
                if (line.Contains('=') | line.Contains(";") | line.Contains("M") | line.Contains("D") | IsDigitsOrDashOnly(line))
                    {
                        return TokenType.KEYWORD;
                    }
                else
                {
                    //
                    return TokenType.UNKNOWNTOKEN;
                }
            }
        }

        public KeyWord keywordType(string line)
        {

            char firstchar = line[0];

            if (firstchar == '@')
            {
                //
                return KeyWord.UNKOWNKEYWORD;
            }
            else
            {
                if (line.Contains('=') | line.Contains(";") | line.Contains("M") | line.Contains("D") | IsDigitsOrDashOnly(line))
                    {
                        return KeyWord.UNKOWNKEYWORD;
                    }
                else
                {
                    //
                    return KeyWord.UNKOWNKEYWORD;
                }
            }
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
    }



}
