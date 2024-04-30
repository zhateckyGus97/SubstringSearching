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
            #region GUS
            List<int> list = new List<int>();
            StreamReader sr = new StreamReader("Anna Karenina.txt");
            //StreamReader sr = new StreamReader("input.txt");
            string text = sr.ReadToEnd();
            //string pattern = "Anna";
            string pattern = "Анна";

            BruteForceSearching BFS = new BruteForceSearching();
            BouyerMoorSearching BMS = new BouyerMoorSearching();
            RabinKarpSearching RKS = new RabinKarpSearching();
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
            Console.WriteLine();
            #endregion

            #region SOKOL

            //RabinKarpSearching
            list.Clear();
            stopwatch.Restart();
            list =RKS.FindSubstring(text,pattern);
            stopwatch.Stop();
            Console.WriteLine($"RKS = {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine(list.Count);
            Console.WriteLine();

            //KnutMorrisPrat
            KnutMorrisPratSearching KMP=new KnutMorrisPratSearching();
            list.Clear();
            stopwatch.Restart();
            list =KMP.FindSubstring(text,pattern);
            stopwatch.Stop();
            Console.WriteLine($"KMP = {stopwatch.ElapsedMilliseconds}");
            Console.WriteLine(list.Count);
            #endregion
            Console.ReadKey();
        }
    }
}
