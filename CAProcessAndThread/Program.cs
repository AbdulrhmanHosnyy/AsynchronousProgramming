using System;
using System.Diagnostics;
namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            // This gets the id of the current process
            Console.WriteLine($"Process ID is: {Process.GetCurrentProcess().Id}");
            // This gets the id of the current thread that the process working on
            Console.WriteLine($"Thread ID is: {Thread.CurrentThread.ManagedThreadId}");
            // This gets the id of the current processor that the current process working on
            Console.WriteLine($"Processor ID is: {Thread.GetCurrentProcessorId()}");
            Console.ReadLine();
        }
    }
}