using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.Helpers
{
    public static class CalculateRunningTimeFunction
    {

        public static void EstimateTime(Func<int> func)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var result = func.Invoke();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            System.Diagnostics.Debug.WriteLine(elapsedMs);
            Console.WriteLine(result);
        }
    }
}
