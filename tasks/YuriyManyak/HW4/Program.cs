using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class Heap
    {
        private int parent(int i)
        {
            return i / 2;
        }

        private int left(int i)
        {
            return 2 * i;
        }

        private int right(int i)
        {
            return 2 * i + 1;
        }

        private void insert(int a)
        {
            _pq[++_n] = a;
            heapUp(_n);
        }

        public int extractMin()
        {
            int temp = _pq[1];
            _pq[1] = _pq[_n];

            _n--;

            heapDown();
            
            return temp;
        }

        private void swap(int i, int j)
        {
            int t = _pq[i];
            _pq[i] = _pq[j];
            _pq[j] = t;
        }

        private void heapUp(int k)
        {
            while (k > 1 && _pq[k / 2] > _pq[k])
            {
                swap(k / 2, k);
                k = k / 2;
            }
        }

        private void heapDown(int k = 1)
        {
            int l, r, min;

            int value = _pq[k];

            while (k <= _n / 2)
            {
                l = left(k);
                r = right(k);

                if (r > _n)
                    min = l;
                else
                {
                    if (_pq[l] > _pq[r])
                        min = r;
                    else
                    {
                        min = l ;
                    }
                }

                if (value <= _pq[min])
                    break;

                _pq[k] = _pq[min];
                k = min;
            }

            _pq[k] = value;
        }

        private int[] _pq;
        private int _n = 0;

        public Heap(int[] a)
        {
            _pq = new int[a.Length + 1];
			
			int len = a.Length;

            for (int i = 0; i < len; i++)
                insert(a[i]);
        }

        public int[] Sorted()
        {
            int[] result = new int[_n];

            int len = _n;

            for (int i = 0; i < len; i++)
               result[i] = extractMin();

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Heap heap = new Heap(new int[]{35, 2, 3, 1, 9, 10, 89, 43, 1, -45, 8});

            int[] sortedArray = heap.Sorted();

            Console.WriteLine("Sorted:");

            foreach (var e in sortedArray)
                Console.Write("{0} ", e);

            Console.ReadKey();
        }
    }
}
