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

        public Contact()
        {
        }
        
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

        static public void Add(Dictionary<Contact, Call[]> contacts)
        {
            var contact = new Contact();
            contact.Name = Ask("Unesite ime i prezime novog kontakta: ");
            contact.Number = AskNumber("Unesite broj novog kontakta: ");
            if (contacts.ContainsKey(contact))
            {
                Console.WriteLine("Kontakt sa istim brojem vec postoji.");
                return;
            }
            contacts.Add(contact, new Call[] { });
            Console.WriteLine("Kontakt sa uspjesno dodan.");
        }

        static string Ask(string prompt)
        {
            Console.Write(prompt);
            string str = Console.ReadLine();
            while (str.Length == 0)
            {
                Console.WriteLine("Unos ne smije biti prazan.");
                Console.Write(prompt);
                str = Console.ReadLine();
            }
            return str;
        }

        static string AskNumber(string prompt)
        {
            Console.Write(prompt);
            string oib = Console.ReadLine();
            while (!IsDigitsOnly(oib) || oib.Length == 0)
            {
                Console.WriteLine("Broj mora sadrzavati samo brojeve.");
                Console.Write(prompt);
                oib = Console.ReadLine();
            }
            return oib;
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
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

        public override bool Equals(object obj)
        {
            Contact other = obj as Contact;
            return other != null && other.Number == this.Number;
        }
    }
}
