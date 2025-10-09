using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBookProject
{
    public class AddressBook
    {
        public List<Contact> ContactsList { get; set; } = new List<Contact>();

         public bool UpsertContact(Contact contact)
        {
            if (contact == null) return false;

            string n    = (contact.Name ?? "").Trim();
            string city = (contact.City ?? "").Trim();
            string mail = (contact.Email ?? "").Trim();
            string tel  = (contact.Phone ?? "").Trim();

            Contact? existing =
                (!string.IsNullOrEmpty(mail)
                    ? ContactsList.FirstOrDefault(c => string.Equals((c.Email ?? "").Trim(), mail, StringComparison.OrdinalIgnoreCase))
                    : null)
                ?? (!string.IsNullOrEmpty(tel)
                    ? ContactsList.FirstOrDefault(c => string.Equals((c.Phone ?? "").Trim(), tel, StringComparison.OrdinalIgnoreCase))
                    : null)
                ?? ContactsList.FirstOrDefault(c =>
                        string.Equals((c.Name ?? "").Trim(), n,   StringComparison.OrdinalIgnoreCase) &&
                        string.Equals((c.City ?? "").Trim(), city, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.Name          = n;
                existing.StreetAddress = (contact.StreetAddress ?? "").Trim();
                existing.PostalCode    = (contact.PostalCode ?? "").Trim();
                existing.City          = city;
                existing.Phone         = tel;
                existing.Email         = mail;
                return true; // uppdatering
            }

            ContactsList.Add(new Contact
            {
                Name          = n,
                StreetAddress = (contact.StreetAddress ?? "").Trim(),
                PostalCode    = (contact.PostalCode ?? "").Trim(),
                City          = city,
                Phone         = tel,
                Email         = mail
            });
            return false; // ny
        }

        // Oförändrad, behålls för ev. annan användning
        public void AddContact(Contact contact)
        {
            if (contact != null) ContactsList.Add(contact);
        }

        // CHANGE: ny metod – ta bort dubbletter vid uppstart (email > telefon > namn+postort)
        public int NormalizeDuplicates()
        {
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var cleaned = new List<Contact>();
            int removed = 0;

            foreach (var c in ContactsList)
            {
                string emailKey = (c.Email ?? "").Trim();
                string phoneKey = (c.Phone ?? "").Trim();
                string nameCity = ((c.Name ?? "").Trim() + "||" + (c.City ?? "").Trim());

                string key =
                    !string.IsNullOrEmpty(emailKey) ? "E:" + emailKey :
                    !string.IsNullOrEmpty(phoneKey) ? "P:" + phoneKey :
                                                      "NC:" + nameCity;

                if (seen.Add(key))
                    cleaned.Add(c);
                else
                    removed++;
            }

            ContactsList = cleaned;
            return removed;
        }

        public List<Contact> SearchContact(string searchtext)
        {
            string q = (searchtext ?? string.Empty).Trim().ToLowerInvariant(); // CHANGE: Trim + Invariant

            var resultat = ContactsList
                .Where(k =>
                    (k.Name ?? string.Empty).ToLowerInvariant().Contains(q) ||
                    (k.City ?? string.Empty).ToLowerInvariant().Contains(q))
                .ToList();

            if (resultat.Count == 0)
                Console.WriteLine("Inga kontakter hittades som matchade din sökning.");
            else
            {
                var fileHandler = new FileHandler();
                Console.WriteLine();
                Console.WriteLine("Sökresultat:");
                foreach (var contact in resultat)
                    Console.WriteLine(fileHandler.ToText(contact));
            }

            return resultat;
        }

        public static void UpdateContact(List<Contact> contacts)
        {
            Console.Write("Ange namnet på den kontakt som ska uppdateras: ");
            string nameToUpdate = Console.ReadLine();

            var contact = contacts.FirstOrDefault(c =>
                c.Name.Equals(nameToUpdate, StringComparison.OrdinalIgnoreCase));

            if (contact == null)
            {
                Console.WriteLine("Kontakten hittades inte.");
                return;
            }

            Console.WriteLine($"Uppdaterar kontakt: {contact.Name}");

            Console.Write("Ange nytt namn (lämna tomt för att behålla det aktuella): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) contact.Name = newName;

            Console.Write("Ange ny gatuadress (lämna tomt för att hålla den aktuella): ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAddress)) contact.StreetAddress = newAddress;

            Console.Write("Ange nytt postnummer (lämna tomt för att hålla det aktuellt): ");
            string newPostal = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPostal)) contact.PostalCode = newPostal;

            Console.Write("Ange ny stad (lämna tomt för att behålla aktuell stad): ");
            string newCity = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCity)) contact.City = newCity;

            Console.Write("Ange nytt telefonnummer (lämna tomt för att behålla det aktuella): ");
            string newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone)) contact.Phone = newPhone;

            Console.Write("Ange ny e-postadress (lämna tomt för att hålla den aktuell): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail)) contact.Email = newEmail;

            Console.WriteLine("Kontakten har uppdaterats!");
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
