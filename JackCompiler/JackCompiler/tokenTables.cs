using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackCompiler
{
    class tokenTables
    {
        public static Dictionary<string, int> tokenTable = new Dictionary<string, int>()
            {
                {"class", 0},
                {"constructor", 0},
                {"function", 0},
                {"method", 0},
                {"field", 0},
                {"static", 0},
                {"var", 0},
                {"int", 0},
                {"boolean", 0},
                {"void", 0},
                {"true", 0},
                {"false", 0},
                {"null", 0},
                {"this", 0},
                {"let", 0},
                {"do", 0},
                {"if", 0},
                {"else", 0},
                {"while", 0},
                {"return", 0},
                {"{", 1},
                {"}", 1},
                {"[", 1},
                {"]", 1},
                {".", 1},
                {",", 1},
                {";", 1},
                {"+", 1},
                {"-", 1},
                {"*", 1},
                {"/", 1},
                {"&", 1},
                {"|", 1},
                {"<", 1},
                {">", 1},
                {"=", 1},
                {"~", 1},
             };

        public static bool contains(string token)
        {
            return (tokenTable.ContainsKey(token));
        }
        public static void addEntry(string token, int address)
        {
            tokenTable.Add(token, address);
        }
        public static int getAddress(string token)
        {
            // We only call this after we check to see that the token is present
            return tokenTable[token];
        }
    }
}
