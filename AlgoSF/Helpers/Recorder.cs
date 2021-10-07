using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Diagnostics.Process;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.Helpers
{
    public static class Recorder
    {
        static Stopwatch timer = new Stopwatch();
        static long bytesPhysicalBefore = 0;
        static long bytesVirtualBefore = 0;

        public static void Start()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
            bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
            timer.Restart();
        }

        public static void Stop()
        {
            timer.Stop();
            long bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
            long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;


            Console.WriteLine($"{bytesPhysicalAfter - bytesPhysicalBefore} physical bytes used");
            Console.WriteLine($"{bytesVirtualAfter - bytesVirtualBefore} virtual bytes used");
            Console.WriteLine($"{timer.Elapsed} time span ellapsed");
            Console.WriteLine($"{timer.ElapsedMilliseconds} time span ellapsed millisecond");
            Console.WriteLine("*************************************************************************");
        }
    }
}
