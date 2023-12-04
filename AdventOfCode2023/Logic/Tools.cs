using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2023
{
    public static class Tools
    {
        public static List<T> ReadListFromFile<T>(Func<string, T> func, string location)
        {
            var list = new List<T>();
            using var reader = new StreamReader(location, Encoding.Default);
            while (!reader.EndOfStream) list.Add(func(reader.ReadLine()));
            return list;
        }

        public static List<int> GetNumbers(string fileLocation)
        {
            using var reader = new StreamReader(fileLocation, Encoding.Default);
            var firstLine = reader.ReadLine();
            var numbers = firstLine.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(y => int.Parse(y)).ToList();
            return numbers;
        }
    }
}