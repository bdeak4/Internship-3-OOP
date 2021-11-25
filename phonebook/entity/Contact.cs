using System;
using System.Collections.Generic;
using System.Linq;

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
            contact.Number = Ask("Unesite broj novog korisnika: ");

            if (contacts.ContainsKey(contact))
            {
                Console.WriteLine("Kontakt sa istim brojem vec postoji.");
                return;
            }

            contacts.Add(contact, new Call[] { });
            Console.WriteLine("Kontakt uspjesno dodan.");
        }

        static public void Remove(Dictionary<Contact, Call[]> contacts)
        {
            var contact = new Contact();
            contact.Number = Choice(new List<Contact>(contacts.Keys));

            if (!contacts.ContainsKey(contact))
            {
                Console.WriteLine("Kontakt sa unesenim brojem ne postoji.");
                return;
            }

            contacts.Remove(contact);
            Console.WriteLine("Kontakt uspjesno obrisan.");
        }

        static public void ChangeType(Dictionary<Contact, Call[]> contacts)
        {
            var number = Choice(new List<Contact>(contacts.Keys));

            if (!contacts.ContainsKey(new Contact() { Number = number }))
            {
                Console.WriteLine("Kontakt sa unesenim brojem ne postoji.");
                return;
            }

            contacts = contacts.ToDictionary(
                d => {
                    if (d.Key.Number == number) d.Key.Type = ChooseType();
                    return d.Key;
                },
                d => d.Value
            );

            Console.WriteLine("Preferenca kontakta uspjesno izmjenjena.");
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

        static string Choice(List<Contact> contacts)
        {
            Console.WriteLine("Opcije:");
            for (var i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {contacts[i].Number} - {contacts[i].Name}");
            }

            string number = Ask("Unesite redni broj ili custom vrijednost: ");
            bool success = int.TryParse(number, out int choice);

            if (choice > 0 && choice <= contacts.Count && success)
            {
                return contacts[choice - 1].Number;
            }

            return number;
        }

        static ContactType ChooseType()
        {
            Console.WriteLine("Preference:");
            var types = Enum.GetNames(typeof(ContactType));

            for (var i = 0; i < types.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {types[i]}");
            }

            string prompt = "Unesite redni broj preference: ";
            bool success = int.TryParse(Ask(prompt), out int choice);

            while (choice < 0 || choice >= types.Length || !success)
            {
                Console.WriteLine("Unos ne smije biti prazan.");
                Console.Write(prompt);
                success = int.TryParse(Ask(prompt), out choice);
            }

            return (ContactType)(choice - 1);
        }

        static public void Print(List<Contact> contacts)
        {
            Console.WriteLine($"{"Ime i prezime".PadRight(30)} | {"Broj".PadRight(30)} | Preferenca");
            Console.WriteLine("------------------------------ | ------------------------------ | ------------------------------");
            
            foreach (var contact in contacts)
            {
                if (contact.Type is ContactType.Blocked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (contact.Type is ContactType.Favorite)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"{contact.Name.PadRight(30)} | {contact.Number.PadRight(30)} | {contact.Type}");
                Console.ResetColor();
            }
        }

        public override int GetHashCode()
        {
            if (Number == null) return 0;
            return Number.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Contact other = obj as Contact;
            return other != null && other.Number == this.Number;
        }
    }
}
