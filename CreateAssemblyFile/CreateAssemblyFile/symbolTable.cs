using System.Collections.Generic;
namespace CreateAssemblyFile
{
    // This class creates a symbol table and initializes it.
    // We also implement the book ST functions.
    // This is straight from the book, except where noted
    // Dictionaries in C# make this really easy

    internal class SymbolTable
    {
        public static Dictionary<string, int> symbolTable = new Dictionary<string, int>()
        {
            {"SP", 0},
            {"LCL", 1},
            {"ARG", 2},
            {"THIS", 3},
            {"THAT", 4},
            {"R0", 0},
            {"R1", 1},
            {"R2", 2},
            {"R3", 3},
            {"R4", 4},
            {"R5", 5},
            {"R6", 6},
            {"R7", 7},
            {"R8", 8},
            {"R9", 9},
            {"R10", 10},
            {"R11", 11},
            {"R12", 12},
            {"R13", 13},
            {"R14", 14},
            {"R15", 15},
            {"SCREEN", 0x4000},
            {"KBD", 0x6000}
        };

        public static bool contains(string symbol)
        {
            return (symbolTable.ContainsKey(symbol));
        }
        public static void addEntry(string symbol, int address)
        {
            symbolTable.Add(symbol, address);
        }
        public static int getAddress(string symbol)
        {
            // We only call this after we check to see that the symbol is present
            return symbolTable[symbol];
        }


    }
}