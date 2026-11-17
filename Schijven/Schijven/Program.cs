using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Regex filter = new Regex(@"([0-9]{1,7})");
        Regex filter2 = new Regex(@"([0-9]{1,9})");
        Regex filter4 = new Regex(@"([0-9]{1,10})");
        Match match = filter.Match(Console.ReadLine());
        int n = int.Parse(match.Value);

        int[] catalogus = new int[n + 2];
        catalogus[0] = 0;
        for (int i = 1; i <= n; i++)
        {
            Match match2 = filter2.Match(Console.ReadLine());
            int number = int.Parse(match2.Value);
            catalogus[i] = number;
        }
        catalogus[n + 1] = int.MaxValue;
        Match match3 = filter.Match(Console.ReadLine());
        int m = int.Parse(match3.Value);

        long totaal = 0;

        for (int i = 0; i < m; i++)
        {
            Match match4 = filter4.Match(Console.ReadLine());
            int steunblok = int.Parse(match4.Value);

            int laag = 0;
            int hoog = catalogus.Length - 1;

            while (laag < hoog)
            {
                int midden = laag + (hoog - laag) / 2;

                if (catalogus[midden] <= steunblok)
                {
                    laag = midden + 1;
                }
                else
                {
                    hoog = midden;
                }
            }
            totaal += steunblok - catalogus[laag - 1];
        }
        Console.WriteLine(totaal);
    }
}

