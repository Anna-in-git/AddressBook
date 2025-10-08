
namespace ContactsBook
{
    public class AddressBook
    {
        //List to hold contacts
        public List<Contact> ContactsList { get; set; }

        // Constructor: initialize the list and add some test contacts
        public AddressBook()
        {
            // Initialize the contact list
            ContactsList = new List<Contact>();

            // Adding test contacts directly when the AddressBook is created
            ContactsList.Add(new Contact
            {
                Name = "Anna Andersson",
                StreetAddress = "Storgatan 1",
                PostalCode = "11122",
                City = "Stockholm",
                Phone = "0701234567",
                Email = "anna@example.com"
            });

            ContactsList.Add(new Contact
            {
                Name = "Björn Svensson",
                StreetAddress = "Lillgatan 5",
                PostalCode = "22233",
                City = "Göteborg",
                Phone = "0737654321",
                Email = "bjorn@example.com"
            });

            ContactsList.Add(new Contact
            {
                Name = "Clara Nilsson",
                StreetAddress = "Högvägen 10",
                PostalCode = "33344",
                City = "Malmö",
                Phone = "0721112233",
                Email = "clara@example.com"
            });
        }

        // Method to add a new contact to the list
        public void AddContact(Contact contact)
        {
            ContactsList.Add(contact);
        }

        public List<Contact> SearchContact(string searchtext)
        {
            List<Contact> resultat = new List<Contact>();

            resultat = ContactsList.Where(k => k.Name.ToLower().Contains(searchtext.ToLower()) || k.City.ToLower().Contains(searchtext.ToLower())).ToList();

            return resultat;

        }

        //Method to update existing contact
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
            if (!string.IsNullOrWhiteSpace(newName))
                contact.Name = newName;

            Console.Write("Enter new street address (leave blank to keep current): ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAddress))
                contact.StreetAddress = newAddress;

            Console.Write("Enter new postal code (leave blank to keep current): ");
            string newPostal = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPostal))
                contact.PostalCode = newPostal;

            Console.Write("Enter new city (leave blank to keep current): ");
            string newCity = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCity))
                contact.City = newCity;

            Console.Write("Enter new phone (leave blank to keep current): ");
            string newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone))
                contact.Phone = newPhone;

            Console.Write("Enter new email (leave blank to keep current): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
                contact.Email = newEmail;

            Console.WriteLine("Contact updated successfully!");
        }

public bool DeleteContact(string name)
{
    // Försök hitta en kontakt i listan som matchar det angivna namnet (ignorerar stora/små bokstäver)
    var contactToRemove = ContactsList
        .FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    // Om kontakten hittades, ta bort den
    if (contactToRemove != null)
    {
        ContactsList.Remove(contactToRemove);
        return true; // successfully deleted
    }

    return false; // contact not found
    }
    }

}
