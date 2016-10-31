using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textRec
{

    static class StringExtractor
    {
        static HashSet<string> _dict;
        
        private static int _n;

        private static bool[,] _d;

        private static string _inpString;

        public static List<string> getAllCombinations(HashSet<string> dictionary, string inpString)
        {
            _inpString = inpString;
            _n = _inpString.Length;
            _dict = dictionary;

            _d = new bool[_n, _n];
            int i;

            for (int d = 0; d <= _n - 1; d++)
                for (i = 0; i <= _n - 1 - d; i++)
                {
                    int j = i + d;
                    if (dictionary.Contains(_inpString.Substring(i, d + 1)))
                        _d[i, j] = true;
                    else
                        for (int k = i + 1; k < j; k++)
                            if (_d[i, k] && _d[k, j])
                                _d[i, j] = true;
                }

            List<string> results = new List<string>();
            Collect("", 0, results);

            return results;
        }

        private static void Collect(string s, int i, List<string> results)
        {

            if (i == _n)
            {
                results.Add(s);
            }

            int j;
            for (j = i; j < _n; j++)
                if (_d[i, j])
                {
                    string sub = _inpString.Substring(i, j - i + 1);
                    if (_dict.Contains(sub))
                    {
                        Collect(s + " " + sub, j + 1, results);
                    }

                }
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            HashSet<string> dictionary = ReadFromFile("dict_en.txt");

            const string inpString = "catsanddog";

            List<string> combinations = StringExtractor.getAllCombinations(dictionary, inpString);

            Console.ReadKey();
        }

        public static HashSet<string> ReadFromFile(string fName)
        {
            int counter = 0;
            string line;

            HashSet<string> dict = new HashSet<string>();

            System.IO.StreamReader file = new System.IO.StreamReader(fName);
            while ((line = file.ReadLine()) != null)
            {
                dict.Add(line);
                counter++;
            }

            file.Close();

            return dict;
        }


    }
}
