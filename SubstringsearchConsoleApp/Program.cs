using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubstringSearchLibrary;

namespace SubstringsearchConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            StreamReader sr = new StreamReader("Anna Karenina.txt");
            string text = sr.ReadToEnd();


            BruteForceSearching BFS = new BruteForceSearching();
            BouyerMoorSearching BMS = new BouyerMoorSearching();
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            list = BFS.FindSubstring(text, "are");
            stopwatch.Stop();
            Console.WriteLine($"BFS = {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine(list.Count);


            list.Clear();
            stopwatch.Restart();
            list = BMS.FindSubstring(text, "are");
            stopwatch.Stop();
            Console.WriteLine($"BMS = {stopwatch.ElapsedMilliseconds}");

            /*foreach (int item in list)
            {
                Console.WriteLine(item);
            }*/

            Console.ReadKey();
        }
    }
}
