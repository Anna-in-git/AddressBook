using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookProject
{
    public class AddressBook
    {
        public List<Contact> ContactsList { get; set; } = new List<Contact>();

        public void AddContact(Contact contact)
        {
            if (contact != null) ContactsList.Add(contact);
        }

        public List<Contact> SearchContact(string searchtext)
        {
            List<Contact> resultat = new List<Contact>();
            string q = (searchtext ?? string.Empty).ToLower();

            resultat = ContactsList
                .Where(k =>
                    (k.Name ?? string.Empty).ToLower().Contains(q) ||
                    (k.City ?? string.Empty).ToLower().Contains(q))
                .ToList();

            if (resultat.Count == 0)
            {
                Console.WriteLine("No contacts found matching your search.");
            }
            else
            {
                var fileHandler = new FileHandler();
                Console.WriteLine();
                Console.WriteLine("Search results:");
                foreach (var contact in resultat)
                    Console.WriteLine(fileHandler.ToText(contact));
            }

            return resultat;
        }

        public static void UpdateContact(List<Contact> contacts)
        {
            Console.Write("Enter the name of the contact to update: ");
            string nameToUpdate = Console.ReadLine();

            var contact = contacts.FirstOrDefault(c =>
                c.Name.Equals(nameToUpdate, StringComparison.OrdinalIgnoreCase));

            if (contact == null)
            {
                Console.WriteLine("Contact not found.");
                return;
            }

            Console.WriteLine($"Updating contact: {contact.Name}");

            Console.Write("Enter new name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) contact.Name = newName;

            Console.Write("Enter new street address (leave blank to keep current): ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAddress)) contact.StreetAddress = newAddress;

            Console.Write("Enter new postal code (leave blank to keep current): ");
            string newPostal = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPostal)) contact.PostalCode = newPostal;

            Console.Write("Enter new city (leave blank to keep current): ");
            string newCity = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCity)) contact.City = newCity;

            Console.Write("Enter new phone (leave blank to keep current): ");
            string newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone)) contact.Phone = newPhone;

            Console.Write("Enter new email (leave blank to keep current): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail)) contact.Email = newEmail;

            Console.WriteLine("Contact updated successfully!");
        }

        public bool DeleteContact(string name)
        {
            var contactToRemove = ContactsList
                .FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (contactToRemove != null)
            {
                ContactsList.Remove(contactToRemove);
                return true;
            }
            return false;
        }
    }
}
