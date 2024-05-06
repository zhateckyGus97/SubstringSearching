using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SubstringSearchLibrary;

namespace SubstringsearchConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            StreamReader sr = new StreamReader("anna.txt");
            string text = sr.ReadToEnd().ToLower();
            //string text = "aaaaaaaaaa";
            string pattern = "но";

            BruteForceSearching BFS = new BruteForceSearching();
            BouyerMoorSearching BMS = new BouyerMoorSearching();
            RabinKarpSearching RKS = new RabinKarpSearching();
            KnutMorrisPratSearching KMP = new KnutMorrisPratSearching();
            Stopwatch stopwatch = new Stopwatch();

            #region BFS and BMS

            //BruteForce
            stopwatch.Start();
            list = BFS.FindSubstring(text, pattern);
            stopwatch.Stop();
            Console.WriteLine($"BruteForceSearching: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Matches: " + list.Count);
            Console.WriteLine();

            //Bouyer-Moore
            list.Clear();
            stopwatch.Restart();
            list = BMS.FindSubstring(text, pattern);
            stopwatch.Stop();
            Console.WriteLine($"BouyerMooreSearching: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Matches: " + list.Count);
            Console.WriteLine();
            #endregion

            #region RKS and KMPS

            //RabinKarpSearching
            list.Clear();
            stopwatch.Restart();
            list = RKS.FindSubstring(text,pattern);
            stopwatch.Stop();
            Console.WriteLine($"RabinKarpSearching: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Matches: " + list.Count);
            Console.WriteLine();

            //KnutMorrisPrat
            list.Clear();
            stopwatch.Restart();
            list = KMP.FindSubstring(text,pattern);
            stopwatch.Stop();
            Console.WriteLine($"KnutMorrisPrattSearching: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Matches: " + list.Count);
            #endregion
            
            Console.ReadKey();
        }
    }
}
