using System;

class Zaagbeweging : IComparable<Zaagbeweging>
{
    public long Kosten { get; }
    public bool IsHorizontaal { get; }

    public Zaagbeweging(long kosten, bool isHorizontaal)
    {
        Kosten = kosten;
        IsHorizontaal = isHorizontaal;
    }

    // Vergelijk op kosten, grotere kosten komen eerst.
    public int CompareTo(Zaagbeweging other)
    {
        return other.Kosten.CompareTo(this.Kosten);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Verwerk de eerste regel invoer
        string[] eersteRegel = Console.ReadLine().Split();
        long h = long.Parse(eersteRegel[0]);
        long b = long.Parse(eersteRegel[1]);
        long k = long.Parse(eersteRegel[2]);

        // Lijsten voor zaagbewegingen
        Zaagbeweging[] zaagBewegingen = new Zaagbeweging[h - 1 + b - 1];

        // Verwerk de kosten van de horizontale zaagbewegingen
        for (long i = 0; i < h - 1; i++)
        {
            long kosten = long.Parse(Console.ReadLine().Split()[0]);
            zaagBewegingen[i] = new Zaagbeweging(kosten, true); // true = horizontaal
        }

        // Verwerk de kosten van de verticale zaagbewegingen
        for (long i = 0; i < b - 1; i++)
        {
            long kosten = long.Parse(Console.ReadLine().Split()[0]);
            zaagBewegingen[h - 1 + i] = new Zaagbeweging(kosten, false); // false = verticaal
        }

        // Sorteer de zaagbewegingen
        Sorteer<Zaagbeweging>(zaagBewegingen, 0, zaagBewegingen.Length - 1, (int)k);

        // Bereken de totale kosten en het aantal keren zagen
        long totaleKosten = 0;
        int horizontalePlanken = 1;
        int verticalePlanken = 1;

        foreach (var zaag in zaagBewegingen)
        {
            if (zaag.IsHorizontaal)
            {
                totaleKosten += zaag.Kosten * verticalePlanken;
                horizontalePlanken++;
            }
            else
            {
                totaleKosten += zaag.Kosten * horizontalePlanken;
                verticalePlanken++;
            }
        }

        // Het totale aantal keren zagen is h * b - 1
        long totaleZagen = h * b - 1; // Verander naar long
        Console.WriteLine($"{totaleKosten} {totaleZagen}");
    }

    // Hybride sorteeralgoritme met quicksort en selection sort
    static void Sorteer<T>(T[] array, int links, int rechts, int k) where T : IComparable<T>
    {
        // Gebruik selection sort arrays kleiner dan k
        if (rechts - links <= k)
        {
            for (int i = links; i <= rechts; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j <= rechts; j++)
                {
                    if (array[j].CompareTo(array[minIndex]) < 0)
                    {
                        minIndex = j;
                    }
                }

                // Verwissel array[i] met array[minIndex]
                T a = array[i];
                array[i] = array[minIndex];
                array[minIndex] = a;
            }
        }
        else // Quicksort voor grotere arrays
        {
            if (links < rechts)
            {
                // Partitioneer de array
                T pivot = array[rechts];
                int i = links - 1;

                for (int j = links; j < rechts; j++)
                {
                    if (array[j].CompareTo(pivot) < 0)
                    {
                        i++;
                        // Verwissel array[i] met array[j]
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }

                // Plaats pivot op de juiste positie
                T temp1 = array[i + 1];
                array[i + 1] = array[rechts];
                array[rechts] = temp1;

                int pivotIndex = i + 1;

                // Recursieve aanroepen voor quicksort
                Sorteer(array, links, pivotIndex - 1, k);
                Sorteer(array, pivotIndex + 1, rechts, k);
            }
        }
    }
}
