using System;
class Program
{
    static void Main(string[] args)
    {
        string n = Console.ReadLine();

        int x;

        if (!Int32.TryParse(n, out x))
        {
            Console.WriteLine("Wrong input for n");
            return;
        }

        int[] output = new int[x];

        for (int i = 0; i < x; i++)
        {
            string line = Console.ReadLine();
            string[] splitLine = line.Split(' ');
            int a;
            int b;

            if (!Int32.TryParse(splitLine[0], out a))
            {
                Console.WriteLine("Wrong input for a");
                return;
            } else if (!Int32.TryParse(splitLine[1], out b))
            {
                Console.WriteLine("Wrong input for b");
                return;
            }

            //Console.WriteLine(a + b);
            output[i] = a + b;
        }

        foreach (int c in output)
        {
            Console.WriteLine(c);
        }
    }
}

