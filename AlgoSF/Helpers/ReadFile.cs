using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoSF.Helpers
{
    class ReadFile
    {
        public static int[] ReturnArrayOfIntFromPath(string path)
        {
            var result = new List<int>();

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(Convert.ToInt32(reader.ReadLine()));
                }
            }

            return result.ToArray();

        }

        public static List<List<int>> ReturnAdjacentListFromFile(string path)
        {
            var result = new List<List<int>>();

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(reader.ReadLine().Split().Where(x => int.TryParse(x, out _)).Select( x => Convert.ToInt32(x)).ToList());
                }
            }

            return result;
        }
    }
}
