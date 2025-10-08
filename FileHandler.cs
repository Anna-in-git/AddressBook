using ContactsBook;

namespace AddressBookProject
{
    public class FileHandler
    {

        //ToText - Gör om data så det kan skrivas på en rad i txt-filen
        public static string ToText(Contact contact)
        {
            return $"{contact.Name},{contact.StreetAddress},{contact.PostalCode},{contact.City},{contact.Phone},{contact.Email}";
        }

        public static void WriteToFile(Contact contact)
        {
            using (StreamWriter writer = new StreamWriter("contacts.txt", append: true))
            {

                writer.WriteLine(FileHandler.ToText(contact));
            }
        }

        //ReadFromFile - hämtar ut en lista med kontakter
        public List<Contact> ReadFromFile()
        {
            using (StreamReader reader = new StreamReader("contacts.txt"))
            {
                contactsList = new List<Contact>();//instansiera listan

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    contactsList.Add(FromText(line));//lägger till varje kontakt i listan
                }
            }

            return contactsList;
        }

        //FromText - Läser ut objektet från en rad i txt-filen
        public static Contact FromText(string line)
        {
            string[] parts = line.Split(',');
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
