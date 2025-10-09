using System;
using System.IO;
using AddressBookProject;

public class Program
{
    static void Main(string[] args)
    {
        AddressBook addressBook = new AddressBook();
        FileHandler fileHandler = new FileHandler();

        if (!File.Exists("contacts.txt"))
        {
            using (File.Create("contacts.txt")) { }
            Console.WriteLine("Ingen kontaktfil hittades, startar med tom adressbok.");
        }
        else
        {
            Console.WriteLine("Läser in kontakter från fil:");
            addressBook.ContactsList = fileHandler.ReadFromFile();
        }

        while (true)
        {
            Console.WriteLine("Välkommen till Adressboken!");
            Console.WriteLine("---------------------------\n");

            Console.WriteLine("1. Add contact");
            Console.WriteLine("2. Search contact");
            Console.WriteLine("3. Update contact");
            Console.WriteLine("4. Delete contact");
            Console.WriteLine("5. Show all contacts");
            Console.WriteLine("6. Exit\n");
            Console.Write("Choose an option (1-6): ");

            string choice = Console.ReadLine();
            Console.WriteLine();


            switch (choice)
            {
                case "1": // Add contact
                    {
                        var c = new Contact();
                        Console.Write("Name: "); c.Name = Console.ReadLine();
                        Console.Write("Street address: "); c.StreetAddress = Console.ReadLine();
                        Console.Write("Postal code: "); c.PostalCode = Console.ReadLine();
                        Console.Write("City: "); c.City = Console.ReadLine();
                        Console.Write("Phone: "); c.Phone = Console.ReadLine();
                        Console.Write("Email: "); c.Email = Console.ReadLine();

                        addressBook.AddContact(c);
                        fileHandler.WriteToFile(c);
                        Console.WriteLine("Kontakt tillagd.");
                        break;
                    }

                case "2": // Search contact
                    {
                        Console.Write("Search text (name/city): ");
                        string q = Console.ReadLine();
                        addressBook.SearchContact(q);
                        break;
                    }

                case "3": // Update contact
                    {
                        AddressBook.UpdateContact(addressBook.ContactsList);
                        fileHandler.SaveAll(addressBook.ContactsList); // spara ändringar
                        break;
                    }

                case "4": // Delete contact
                    {
                        Console.Write("Enter the name of the contact to delete: ");
                        string name = Console.ReadLine();
                        bool ok = addressBook.DeleteContact(name);
                        if (ok)
                        {
                            fileHandler.SaveAll(addressBook.ContactsList);
                            Console.WriteLine("Kontakt raderad.");
                        }
                        else Console.WriteLine("Kontakt hittades inte.");
                        break;
                    }

                case "5": // Show all contacts
                    {
                        if (addressBook.ContactsList.Count == 0)
                        {
                            Console.WriteLine("Inga kontakter.");
                        }
                        else
                        {
                            Console.WriteLine("Alla kontakter:");
                            foreach (var c in addressBook.ContactsList)
                                Console.WriteLine(fileHandler.ToText(c));
                        }
                        break;
                    }

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
