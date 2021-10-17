using RuneAPI.Models;
using System;
using System.IO;

namespace RuneDataToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args.Length > 0 ? args[0] : "C:\\dev\\d2webstuff\\runes.txt";

            string[] rows = File.ReadAllLines(file);

            var headers = rows[0].Split('\t');

            for (int i = 1; i < rows.Length; i++)
            {
            }
        }
    }
}
