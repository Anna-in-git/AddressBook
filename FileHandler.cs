using ContactsBook;

namespace AddressBookProject
{
    public class FileHandler
    {
        

        //ToText - Gör om data så det kan skrivas på en rad i txt-filen
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

        //FromString - Läser ut objektet från en rad i txt-filen
        public List<Contact> ReadFromFile()// returnar en lista med kontakter
        {
            List<Contact> ContactsList = new List<Contact>();//instansiera listan

            using (StreamReader reader = new StreamReader("contacts.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    ContactsList.Add(FromText(line));//lägger till varje kontakt i listan
                }
            }

            return ContactsList;
        }

        //FromText - Läser ut objektet från en rad i txt-filen och gör om till ett Contact-objekt
        public Contact FromText(string line)
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
