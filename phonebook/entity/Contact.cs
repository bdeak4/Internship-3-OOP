using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phonebook.entity
{
    public enum ContactType
    {
        Normal,
        Favorite,
        Blocked,
    };

    class Contact
    {
        private string _name;
        private string _number;
        private ContactType _type = ContactType.Normal;

        public string Name { get => _name; set => _name = value; }
        public string Number { get => _number; set => _number = value; }
        public ContactType Type { get => _type; set => _type = value; }

        public Contact(string name, string number)
        {
            _name = name;
            _number = number;
        }

        public Contact(string name, string number, ContactType type)
        {
            _name = name;
            _number = number;
            _type = type;
        }

        static public void Print(List<Contact> contacts)
        {
            Console.WriteLine(
                "Ime i prezime".PadRight(30) + " | " +
                "Broj".PadRight(30) + " | Preferenca"
            );
            Console.WriteLine("------------------------------ | ------------------------------ | ------------------------------");
            foreach (var contact in contacts)
            {
                if (contact.Type is ContactType.Blocked)
                    Console.ForegroundColor = ConsoleColor.Red;
                if (contact.Type is ContactType.Favorite)
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    contact.Name.PadRight(30) + " | " +
                    contact.Number.PadRight(30) + " | " + 
                    contact.Type
                );
                Console.ResetColor();
            }
        }
    }
}
