using RuneAPI.Models;
using System;
using System.IO;

namespace RuneDataToCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = args.Length > 0 ? args[0] : "E:\\Casc\\D2Data\\runes.txt";

            string[] rows = File.ReadAllLines(file);

            var headers = rows[0].Split('\t');

            for (int i = 1; i < rows.Length; i++)
            {
                var rowData = rows[i].Split('\t');

                var rune = new Rune
                {
                    Name = rowData[1],
                    RuneString = rowData[14],
                    Rune1 = rowData[15],
                    Rune2 = rowData[16],
                    Rune3 = rowData[17],
                    Rune4 = rowData[18],
                    Rune5 = rowData[19],
                    Rune6 = rowData[20],
                };
            }
        }
    }
}
