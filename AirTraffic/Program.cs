using System;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine().Split()[0]);
        Punt[] punten = new Punt[n];

        // Input verwerken en opslaan als punten
        for (int i = 0; i < n; i++)
        {
            string[] line = Console.ReadLine().Split();
            punten[i] = new Punt(int.Parse(line[0]), int.Parse(line[1]));
        }

        // Sorteer de punten op x-coordinaat
        Array.Sort(punten, (p1, p2) => p1.X.CompareTo(p2.X));

        // Roep het algoritme aan en schrijf het antwoord naar de console.
        long kortsteAfstand = KortsteAfstand(punten, 0, punten.Length - 1);
        Console.WriteLine(kortsteAfstand);
    }

    // Recursieve functie om de kleinste afstand te berekenen
    static long KortsteAfstand(Punt[] punten, int links, int rechts)
    {
        // Base case: als er 2 of 3 punten zijn, zoek de kortste afstand
        if (rechts - links <= 2)
        {
            long kortsteAfstand = long.MaxValue;
            for (int i = links; i <= rechts; i++)
            {
                for (int j = i + 1; j <= rechts; j++)
                {
                    long afstand = punten[i].Afstand(punten[j]);
                    if (afstand < kortsteAfstand)
                    {
                        kortsteAfstand = afstand;
                    }
                }
            }
            return kortsteAfstand;
        }

        // Recursief: Verdeel de punten in linker- en rechterhelft en herhaal.
        int midden = (links + rechts) / 2;
        long minL = KortsteAfstand(punten, links, midden);
        long minR = KortsteAfstand(punten, midden + 1, rechts);
        long min = Math.Min(minL, minR);

        // Vind het deel rond het midden en sla op als middenDeel
        int midX = punten[midden].X;
        var middenDeel = new System.Collections.Generic.List<Punt>();
        for (int i = links; i <= rechts; i++)
        {
            if (Math.Abs(punten[i].X - midX) < min)
            {
                middenDeel.Add(punten[i]);
            }
        }

        // Sorteer middenDeel op y-coordinaat
        middenDeel.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));

        // Controleer de punten in middenDeel
        for (int i = 0; i < middenDeel.Count; i++)
        {
            for (int j = i + 1; j < middenDeel.Count && (middenDeel[j].Y - middenDeel[i].Y) < min; j++)
            {
                long afstand = middenDeel[i].Afstand(middenDeel[j]);
                if (afstand < min)
                {
                    min = afstand;
                }
            }
        }

        return min;
    }
}

// Punt klasse om de coördinaten van de punten op te slaan
class Punt
{
    public int X { get; set; }
    public int Y { get; set; }

    public Punt(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Returnt de afstand^2 tussen p1 en p2 door p1.Afstand(p2) aan te roepen, gebruikt long om overflow te voorkomen
    public long Afstand(Punt p2)
    {
        long dx = this.X - p2.X;
        long dy = this.Y - p2.Y;
        return dx * dx + dy * dy;
    }
}
