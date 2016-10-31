using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS
{
    public class Matrix
    {
        private int[,] _M;
        private int _m, _n;

        public Matrix(int m, int n)
        {
            _m = m;
            _n = n;
            _M = new int[_m, _n];
        }

        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < _m && j >= 0 && j < _n)
                    return _M[i, j];
                else
                    return 0;
            }
            set { _M[i, j] = value; }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < _m; i++)
            {
                for (int j = 0; j < _n; j++)
                    res += String.Format("{0} ", _M[i, j]);
                res += "\n";
            }
            return res;
        }

    }
    public static class Solver
    {
        private static Matrix len = null;
        private static int m, n;
        private static List<string[]> LCS = null;
        private static string[] _x;
        private static string[] _y;
        public static List<string[]> getAllLCS(string[] x, string[] y)
        {
            m = x.Length;
            n = y.Length;
            _x = x;
            _y = y;

            len = new Matrix(m, n);

            LCS = new List<string[]>();

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (x[i] == y[j])
                        len[i, j] = 1 + len[i - 1, j - 1];
                    else
                        len[i, j] = Math.Max(len[i - 1, j], len[i, j - 1]);

            //Console.WriteLine(len);

            string[] aggr = new string[len[m - 1, n - 1]];
            Collect(m - 1, n - 1, aggr);

            return LCS;
        }

        private static void Collect(int i, int j, string[] aggr)
        {
            if (i < 0 || j < 0)
            {
                string[] a = new string[aggr.Length];
                for (int t = 0; t < aggr.Length; t++)
                    a[t] = aggr[t];
                LCS.Add(a);
                return;
            }
                

            if (_x[i] == _y[j])
            {
                aggr[len[i, j] - 1] = _x[i];
                Collect(i - 1, j - 1, aggr);
            }
                
            else
            {
                int max = Math.Max(len[i - 1, j], len[i, j - 1]);
                if (len[i - 1, j] == max)
                    Collect(i - 1, j, aggr);
                if (len[i, j - 1] == max)
                    Collect(i, j - 1, aggr);
            }
         
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            string[] x = {"A", "B", "C", "B", "D", "A", "B"};
            string[] y = { "B", "D", "C", "A", "B", "A" };

            List<string[]> allLCS = Solver.getAllLCS(x, y);

            Console.ReadKey();
        }
    }
}
