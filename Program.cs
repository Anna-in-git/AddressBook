using System;
using System.IO;
using AddressBookProject;

public class Program
{
    static void Main(string[] args)
    {
        var addressBook = new AddressBook();
        var fileHandler = new FileHandler();

        if (!File.Exists("contacts.txt"))
        {
            using (File.Create("contacts.txt")) { }
            Console.WriteLine("Ingen kontaktfil hittades, startar med tom adressbok.");
        }
        else
        {
            Console.WriteLine("Läser in kontakter från fil:");
            addressBook.ContactsList = fileHandler.ReadFromFile();
            
            // CHANGE: normalisera dubbletter som redan finns i filen
            int removed = addressBook.NormalizeDuplicates();
            if (removed > 0)
            {
                fileHandler.SaveAll(addressBook.ContactsList);
                Console.WriteLine($"Rensade {removed} dubblett(er) vid start.");
            }
        }

        while (true)
        {
            Console.WriteLine("Välkommen till Adressboken!");
            Console.WriteLine("---------------------------\n");
            Console.WriteLine("1. Lägg till/uppdatera kontakt"); // CHANGE: tydligt att det är upsert
            Console.WriteLine("2. Sök kontakt (namn/postort)");
            Console.WriteLine("3. Uppdatera kontakt (via namn)");
            Console.WriteLine("4. Ta bort kontakt");
            Console.WriteLine("5. Visa alla kontakter");
            Console.WriteLine("6. Utgång\n"); // CHANGE: tog bort felskrivet 'Bara'
            Console.Write("Välj ett alternativ (1-6): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    var nc = new Contact();
                    Console.Write("Namn: ");             nc.Name = Console.ReadLine();
                    Console.Write("Gatuadress: ");       nc.StreetAddress = Console.ReadLine();
                    Console.Write("Postnummer: ");       nc.PostalCode = Console.ReadLine();
                    Console.Write("Postort/Stad: ");     nc.City = Console.ReadLine();
                    Console.Write("Telefon: ");          nc.Phone = Console.ReadLine();
                    Console.Write("E-post: ");           nc.Email = Console.ReadLine();

                    // CHANGE: använd Upsert, inte AddContact+WriteToFile
                    bool wasUpdate = addressBook.UpsertContact(nc);
                    if (wasUpdate)
                    {
                        fileHandler.SaveAll(addressBook.ContactsList); // skriv om filen vid uppdatering
                        Console.WriteLine("Kontakt uppdaterad.");
                    }
                    else
                    {
                        fileHandler.WriteToFile(nc); // ny kontakt -> append
                        Console.WriteLine("Kontakt tillagd.");
                    }
                    break;

                case "2":
                    Console.Write("Ange söktext (namn eller postort): ");
                    addressBook.SearchContact(Console.ReadLine());
                    break;

                case "3":
                    AddressBook.UpdateContact(addressBook.ContactsList);
                    fileHandler.SaveAll(addressBook.ContactsList); // spegla ändringar
                    break;

                case "4":
                    Console.Write("Ange namnet på den kontakt som ska raderas: ");
                    var nameToDelete = Console.ReadLine();
                    bool deleted = addressBook.DeleteContact(nameToDelete);
                    Console.WriteLine(deleted ? "Kontakt borttagen." : "Kontakten hittades inte.");
                    if (deleted) fileHandler.SaveAll(addressBook.ContactsList);
                    break;

                case "5":
                    foreach (var c in addressBook.ContactsList)
                        Console.WriteLine(fileHandler.ToText(c));
                    break;

                case "6":
                    Console.WriteLine("\nKlart! Tryck på valfri tangent för att avsluta.");
                    Console.ReadKey();
                    return;

                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }

            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
