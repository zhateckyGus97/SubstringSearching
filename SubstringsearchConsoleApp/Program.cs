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
            string pattern = "Anna";

            BruteForceSearching BFS = new BruteForceSearching();
            BouyerMoorSearching BMS = new BouyerMoorSearching();
            Stopwatch stopwatch = new Stopwatch();

            //BruteForce
            stopwatch.Start();
            list = BFS.FindSubstring(text, pattern);
            stopwatch.Stop();
            Console.WriteLine($"BFS = {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine(list.Count);
            Console.WriteLine();

            //Bouyer-Moore
            list.Clear();
            stopwatch.Restart();
            list = BMS.FindSubstring(text, pattern);
            stopwatch.Stop();
            Console.WriteLine($"BMS = {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine(list.Count);

            /*foreach (int item in list)
            {
                Console.WriteLine(item);
            }*/

            Console.ReadKey();
        }
    }
}
