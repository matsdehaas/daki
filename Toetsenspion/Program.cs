using System.Text;

class MijnElement
{
    public char Waarde;
    public MijnElement Volgende;
    public MijnElement Vorige;

    public MijnElement(char waarde)
    {
        Waarde = waarde;
    }
}

class MijnLinkedList
{
    private MijnElement head;
    private MijnElement tail;
    private MijnElement cursor;

    public MijnLinkedList()
    {
        // head en tail
        head = new MijnElement('\0');
        tail = new MijnElement('\0');
        head.Volgende = tail;
        tail.Vorige = head;
        cursor = head; // Cursor begint aan het begin van de lijst
    }

    public void VoegIn(char waarde)
    {
        MijnElement nieuwElement = new MijnElement(waarde);
        nieuwElement.Volgende = cursor.Volgende;
        nieuwElement.Vorige = cursor;
        cursor.Volgende.Vorige = nieuwElement;
        cursor.Volgende = nieuwElement;
        cursor = nieuwElement; // Verplaats cursor naar het nieuwe element
    }

    public void Verwijder()
    {
        if (cursor != head) // Verwijder alleen als er iets links van de cursor staat
        {
            cursor.Vorige.Volgende = cursor.Volgende;
            cursor.Volgende.Vorige = cursor.Vorige;
            cursor = cursor.Vorige; // Verplaats cursor naar links
        }
    }

    public void VerplaatsLinks()
    {
        if (cursor != head) // Verplaats naar links( als mogelijk)
        {
            cursor = cursor.Vorige;
        }
    }

    public void VerplaatsRechts()
    {
        if (cursor.Volgende != tail) // Verplaats naar rechts (als mogelijk)
        {
            cursor = cursor.Volgende;
        }
    }

    public string MaakString()
    {
        StringBuilder sb = new StringBuilder();
        MijnElement huidige = head.Volgende;
        while (huidige != tail)
        {
            sb.Append(huidige.Waarde);
            huidige = huidige.Volgende;
        }
        return sb.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // het aantal wachtwoorden
        for (int i = 0; i < n; i++)
        {
            string toetsaanslagen = Console.ReadLine();
            MijnLinkedList wachtwoord = new MijnLinkedList();

            // Verwerk elke toetsaanslag in de string
            foreach (char aanslag in toetsaanslagen)
            {
                switch (aanslag)
                {
                    case '<':
                        wachtwoord.VerplaatsLinks();
                        break;
                    case '>':
                        wachtwoord.VerplaatsRechts();
                        break;
                    case '-':
                        wachtwoord.Verwijder();
                        break;
                    default:
                        wachtwoord.VoegIn(aanslag);
                        break;
                }
            }

            // Print het gereconstrueerde wachtwoord >:)
            Console.WriteLine(wachtwoord.MaakString());
        }
    }
}
