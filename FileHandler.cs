using System;
using System.Collections.Generic;
using System.IO;

namespace AddressBookProject
{
    public class FileHandler
    {
        public string ToText(Contact contact)
        {
            return $"{contact.Name},{contact.StreetAddress},{contact.PostalCode},{contact.City},{contact.Phone},{contact.Email}";
        }

        public void WriteToFile(Contact contact)
        {
            using (StreamWriter writer = new StreamWriter("contacts.txt", append: true))
            {
                writer.WriteLine(ToText(contact));
            }
        }

        public void SaveAll(List<Contact> contacts)
        {
            using (StreamWriter writer = new StreamWriter("contacts.txt", append: false))
            {
                foreach (var c in contacts)
                    writer.WriteLine(ToText(c));
            }
        }

        public List<Contact> ReadFromFile()
        {
            List<Contact> contactsList = new List<Contact>();

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

            return new Contact
            {
                Name = parts[0],
                StreetAddress = parts[1],
                PostalCode = parts[2],
                City = parts[3],
                Phone = parts[4],
                Email = parts[5]
            };
        }
    }
}
