using static System.Math;
using System.Windows.Input;
namespace MatrixTest
{
    internal class Matrix
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j< 10;  j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }
                Console.Write("\n");
            }
            Console.WriteLine("Введите размерность поля n * m");
            string n_m = Console.ReadLine();
            int n = Convert.ToInt32(n_m.Split(" ")[0]) - 1;
            int m = Convert.ToInt32(n_m.Split(" ")[1]) - 1;
            //-----------------------------------------------------------
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Abs(i - n) <= 1 & Abs(j - m) <= 1) 
                    {
                        matrix[i, j] = 1;
                    }
                    matrix[n, m] = 2;
                    Console.Write($"{matrix[i, j]}\t");
                }

                Console.Write("\n");
            }
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.RightArrow) 
                {
                    m++;
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.LeftArrow)
                {
                    m--;
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    n--;
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.DownArrow)
                {
                    n++;
                }
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        matrix[i, j] = 0;
                        if (Abs(i - n) <= 1 & Abs(j - m) <= 1)
                        {
                            matrix[i, j] = 1;
                        }
                        matrix[n, m] = 2;
                        Console.Write($"{matrix[i, j]}\t");
                    }

                    Console.Write("\n");
                }
                Console.WriteLine("\n");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }

            }
        }
    }
}