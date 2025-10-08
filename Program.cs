using AddressBookProject;
using ContactsBook;

class Program
{
    static void Main(string[] args)
    {
        //Hitta och läs från fil om den finns
        if (File.Exists("contacts.txt"))
        {
            Console.WriteLine("Läser in kontakter från fil:");
            FileHandler.ReadFromFile();
        }
        else
        {
            Console.WriteLine("Ingen kontaktfil hittades, startar med tom adressbok.");
        }


        // Skapa en ny adressbok
        AddressBook addressBook = new AddressBook();

        Console.WriteLine("Alla kontakter i adressboken:");
        foreach (var c in addressBook.ContactsList)
        {
            Console.WriteLine($"{c.Name}, {c.City}");
        }

        Console.WriteLine("\nSök efter 'Stockholm':");
        var resultat = addressBook.SearchContact("Stockholm");
        foreach (var r in resultat)
        {
            Console.WriteLine($"{r.Name} - {r.City}");
        }

        Console.WriteLine("\nSkriv ut till fil:");
        foreach (var c in addressBook.ContactsList)
        {
            FileHandler.WriteToFile(c);
        }

        Console.WriteLine("\nLäser upp från fil:");
        FileHandler.ReadFromFile();

        Console.WriteLine("\nKlart! Tryck på valfri tangent för att avsluta.");
        Console.ReadKey();
    }
}
