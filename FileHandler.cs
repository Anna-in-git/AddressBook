using System;
using System.Collections.Generic;
using System.IO;

namespace AddressBookProject
{
    public class FileHandler
    {
        public string ToText(Contact contact)
        {
            // Oförändrad
            return $"{contact.Name},{contact.StreetAddress},{contact.PostalCode},{contact.City},{contact.Phone},{contact.Email}";
        }

        public void WriteToFile(Contact contact)
        {
            // Oförändrad: används när vi lägger NY post (inte update)
            using (StreamWriter writer = new StreamWriter("contacts.txt", append: true))
                writer.WriteLine(ToText(contact));
        }

        public void SaveAll(List<Contact> contacts)
        {
            // Oförändrad: skriv om hela filen (efter update/delete/normalize)
            using (StreamWriter writer = new StreamWriter("contacts.txt", append: false))
                foreach (var c in contacts)
                    writer.WriteLine(ToText(c));
        }

        public List<Contact> ReadFromFile()
        {
            var contactsList = new List<Contact>();
            using (StreamReader reader = new StreamReader("contacts.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var c = FromText(line);
                    if (c != null) contactsList.Add(c);
                }
            }
            return contactsList;
        }

        public Contact? FromText(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;
            string[] parts = line.Split(',');
            if (parts.Length < 6) return null;

            // CHANGE: Trimma alla fält vid inläsning för stabil matchning
            return new Contact
            {
                Name          = parts[0].Trim(),
                StreetAddress = parts[1].Trim(),
                PostalCode    = parts[2].Trim(),
                City          = parts[3].Trim(),
                Phone         = parts[4].Trim(),
                Email         = parts[5].Trim()
            };
        }
    }
}
