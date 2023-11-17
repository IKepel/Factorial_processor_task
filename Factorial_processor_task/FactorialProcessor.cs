using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorial_processor_task
{
    public class FactorialProcessor
    {
        public void Go(int param, bool parallelMode)
        {
            if (param < 1 || param > 15)
            {
                throw new ArgumentException("param should be between 1 and 15");
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            if (parallelMode)
            {
                List<Task> tasks = new List<Task>();

                for (int i = 1; i <= param; i++)
                {
                    int copyOfI = i;
                    Task task = Task.Run(() =>
                    {
                        PrintFactorial(copyOfI, CalculateFactorial(copyOfI));
                    });
                    tasks.Add(task);
                }

                Task.WhenAll(tasks).ContinueWith(_ =>
                {
                    PrintExecutionTime(stopwatch);
                }).Wait();
            }
            else
            {
                for (int i = 1; i <= param; i++)
                {
                    PrintFactorial(i, CalculateFactorial(i));
                }
                PrintExecutionTime(stopwatch);
            }
        }

        private void PrintExecutionTime(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalSeconds} seconds ({stopwatch.Elapsed.TotalMilliseconds} milliseconds)");
        }

        private void PrintFactorial(int number, long factorial)
        {
            Console.WriteLine($"Factorial of {number}: {factorial}");
        }

        private long CalculateFactorial(int n)
        {
            if (n == 1 || n == 0) return 1;
            else
            {
                Thread.Sleep(100);
                return n * CalculateFactorial(n - 1);
            }
        }
    }
}