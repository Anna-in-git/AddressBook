namespace AddressBookProject;

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

    //ReadFromFile - hämtar ut en lista med kontakter
    public List<Contact> ReadFromFile()
    {
        List<Contact> contactsList = new List<Contact>(); // Deklarera och instansiera lista

        using (StreamReader reader = new StreamReader("contacts.txt"))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                contactsList.Add(FromText(line));
            }
        }
        return contactsList;
    }

    //FromText - Läser ut objektet från en rad i txt-filen
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
