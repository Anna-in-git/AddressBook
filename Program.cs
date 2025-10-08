using AddressBookProject;

public class Program
{
    static void Main(string[] args)
    {
        // Skapa en ny adressbok
        AddressBook addressBook = new AddressBook();

        //Hitta och läs från fil om den finns
        // Kontrollera om filen finns, annars skapa den
        if (!File.Exists("contacts.txt"))
        {
            using (File.Create("contacts.txt")) { } // Skapar en tom fil
            Console.WriteLine("Ingen kontaktfil hittades, startar med tom adressbok.");
        }
        else
        {
            Console.WriteLine("Läser in kontakter från fil:");
            FileHandler fileHandler = new FileHandler();
            addressBook.ContactsList = fileHandler.ReadFromFile(); // Anropar metoden för att läsa från fil och fyller adressboken
        }

        //Huvudmeny

        // 1. Lägg till kontakt
        // 2. Sök kontakt
        // 3. Ta bort kontakt
        // 4. Uppdatera kontakt
        // 5. Visa alla kontakter
        // 6. Avsluta

        Console.WriteLine("Välkommen till Adressboken!");
        Console.WriteLine("---------------------------");
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("1. Add contact");
        Console.WriteLine("2. Search contact");
        Console.WriteLine("3. Update contact");
        Console.WriteLine("4. Delete contact");
        Console.WriteLine("5. Show all contacts");
        Console.WriteLine("6. Exit");
        Console.WriteLine();
        Console.Write("Choose an option (1-6): ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Lägg till en ny kontakt
                Contact newContact = new Contact(); // Skapa en ny kontakt
                Console.Write("Name: ");
                newContact.Name = Console.ReadLine();
                Console.Write("Street Address: ");
                newContact.StreetAddress = Console.ReadLine();
                Console.Write("Postal Code: ");
                newContact.PostalCode = Console.ReadLine();
                Console.Write("City: ");
                newContact.City = Console.ReadLine();
                Console.Write("Phone: ");
                newContact.Phone = Console.ReadLine();
                Console.Write("Email: ");
                newContact.Email = Console.ReadLine();
                addressBook.AddContact(newContact);
                Console.WriteLine("Contact added!");

                break;
            case "2":
                // Sök kontakt
                Console.WriteLine("Enter search text (name or city): ");
                string searchtext = Console.ReadLine();
                addressBook.SearchContact(searchtext);
                break;
            case "3":
                // Uppdatera kontakt (anropa UpdateContact i AdressBook)
                break;
            case "4":
                // Ta bort kontakt
                break;
            case "5":
                // Visa alla kontakter
                break;
            case "6":
                // Avsluta
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;


        }



        Console.WriteLine("\nKlart! Tryck på valfri tangent för att avsluta.");
        Console.ReadKey();
    }
}