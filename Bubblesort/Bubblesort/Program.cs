using System;

namespace Bubblesort
{

    internal class Program
    {
        int zahl = 0;
        int max = 0;
        static void Main()
        {
            Program program = new Program();
            const int MAX_NUMBER_ROUND = 10;
            int[] a = new int[100];
            Random rnd = new Random();
            

            for (int r = 0; r < MAX_NUMBER_ROUND; r++)
            {
                for (int i = 0; i < a.Length - 1; i++)
                {
                    a[i] = rnd.Next(1, 100);
                }
                program.BubbleSort(ref a);

            if (program.max < program.zahl)
                {
                    program.max = program.zahl;
                }
                System.Diagnostics.Debug.WriteLine("{0};{1};{2}", r, program.zahl, program.max);
                program.max = 0;
                program.zahl = 0;
            }
            Console.ReadLine();
        }
        public void PrintArray(int[] a)
        {
            foreach (int i in a)
            {
                Console.WriteLine(i);
            }
        }
        public void BubbleSort(ref int[] a)
        {
            bool done = false;

            while (!done)
            {       
                done = true;
                for (int i = 0; i < a.Length - 1; i++)
                {
                    if (a[i] > a[i + 1])
                    {
                        int x = a[i + 1];
                        a[i + 1] = a[i];
                        a[i] = x;
                        zahl++;
                        done = false;
                    }
                }
            }
           
        }
    }
}
