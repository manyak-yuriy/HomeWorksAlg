using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextJustification
{
    static class TextJustifier
    {
        private static List<string> getWords(string text)
        {
            return text.Split(' ').ToList();
        }

        public static string getJustifiedText(string text, int lineLength)
        {
            List<string> words = TextJustifier.getWords(text);

            /*
            foreach (var word in words)
                Console.WriteLine(word);
            */

            int wordCnt = words.Count;
            int[,] badness = new int[wordCnt, wordCnt];

            for (int i = 0; i < wordCnt; i++)
            {
                int totalLen = 0;
                for (int j = i; j < wordCnt; j++)
                {
                    badness[i, j] = lineLength - (totalLen += words[j].Length);
                    badness[i, j] = (badness[i, j] > 0)? badness[i, j] * badness[i, j] * badness[i, j] : int.MaxValue;
                }
            }

            int n = wordCnt;

            /*
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("{0} ", badness[i, j]);
                Console.WriteLine();
            }
            */

            int[] DP = new int[n];
            int[] DPind = new int[n];

            for (int i = n - 1; i >= 0; i--)
            {
                DP[i] = badness[i, n - 1];
                DPind[i] = n;

                for (int j = i + 1; j < n; j++)
                {
                    if (badness[i, j - 1] == int.MaxValue)
                        continue;

                    int t = badness[i, j - 1] + DP[j];
                    if (t < DP[i])
                    {
                        DP[i] = (int)t;
                        DPind[i] = j;
                    }
                }
            }

            int end = 0;
            int start;

            string justified = string.Empty;

            do
            {
                start = end;
                end = DPind[start];

                for (int i = start; i < end; i++)
                    justified += words[i] + ' ';

                justified += Environment.NewLine;

            } while (end < n);

            return justified;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string text = "this is an input for justification procedure";

            Console.WriteLine("Justified text: \n\n{0}", TextJustifier.getJustifiedText(text, lineLength: 20));

            Console.ReadKey();
        }
    }
}
