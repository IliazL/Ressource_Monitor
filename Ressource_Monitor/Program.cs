using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Threading;

namespace Ressource_Monitor
{
    class Program
    {
        /// <summary>
        /// pulls CPU and Memory information
        /// </summary>
        public static PerformanceCounter _cpuPerf = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        
        public static PerformanceCounter _memPerf = new PerformanceCounter("Memory", "Available MBytes");

        public static ConsoleColor _currentForeground = Console.ForegroundColor;

        static void Main(string[] args)
        {

            _cpuPerf.NextValue();
            _memPerf.NextValue();


            // Create Threads for each CPU and Memory
            Thread cpuThread = new Thread(new ThreadStart(CPU));
            Thread memThread = new Thread(new ThreadStart(Memory));

            Console.SetWindowSize(90, 60);

            
            // Ressource Monitor v1 by Dominik aka (Iliaz)
            // 14/05/2015

            

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Ressource Monitor version1");
            Console.WriteLine("");
            Console.ForegroundColor = _currentForeground;
                                                          
           
            cpuThread.Start();
            memThread.Start();

                       
        }

        #region Thread Functions
        public static void CPU()
        {            
            while(true)
            {
                int currentCPUPercentage = (int)_cpuPerf.NextValue();
                if (currentCPUPercentage < 100)
                {
                    Console.WriteLine("CPU Load        : {0}%", currentCPUPercentage);
                }
                if (currentCPUPercentage == 100)
                {
                    ConsoleColor red = ConsoleColor.Red;
                    ColoredCPULoad(red, currentCPUPercentage);

                }
                

                Thread.Sleep(1000);
            }
        }

        public static void Memory()
        {         

            while(true)
            {
                int currentMemPercantage = (int)_memPerf.NextValue();
                if (currentMemPercantage > 4096)
                {
                    Console.WriteLine("Memory available: {0}", currentMemPercantage);
                }
                if (currentMemPercantage < 4096)
                {
                    ConsoleColor yellow = ConsoleColor.Yellow;
                    ColoredMemLoad(yellow, currentMemPercantage);
                }

                Thread.Sleep(1000);
            }
        }
        #endregion

        /// <summary>
        /// these 2 methods change the foreground color of the console, but only for 1 line
        /// </summary>
        /// <param name="color"></param>
        /// <param name="i"></param>
        public static void ColoredCPULoad(ConsoleColor color, int i)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine("CPU Load        : {0}%", i);
            Console.ForegroundColor = originalColor;
        }

        public static void ColoredMemLoad(ConsoleColor color, int i)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine("Memory available: {0}", i);
            Console.ForegroundColor = originalColor;
        }
    }
}
