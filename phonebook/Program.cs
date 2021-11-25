using System;
using System.Collections.Generic;
using phonebook.entity;
using System.Linq;

namespace phonebook
{
    enum Menu
    {
        Main,
        PrintContacts,
        AddContact,
        RemoveContact,
        ChangeContactType,
        ManageContact,
        PrintCalls,
        PrintCallsByContact = 51,
        AddCall,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var contacts = new Dictionary<Contact, Call[]>
            {
                { new Contact("Ante Antic", "0981234567", ContactType.Favorite),
                  new Call[] { new Call("0951234567", new DateTime(2021, 2, 2), CallStatus.Ended),
                               new Call("0961234567") } },

                { new Contact("Ana Anic", "0961234567", ContactType.Favorite),
                  new Call[] { new Call("0981234567", new DateTime(2021, 3, 1), CallStatus.Missed),
                               new Call("0981234567", new DateTime(2021, 3, 2), CallStatus.Ended) } },

                { new Contact("Marija Maric", "0951234567"),
                  new Call[] { new Call("0981234568", new DateTime(2021, 11, 11), CallStatus.Ended) } },

                { new Contact("Mate Matic", "0991234567", ContactType.Blocked),
                  new Call[] { } },
            };

            var menu = 0;
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                switch ((Menu)menu)
                {
                    case Menu.Main:
                        menu = Choice(new string[] {
                            "Ispis svih kontakata",
                            "Dodavanje novih kontakata u imenik",
                            "Brisanje kontakata iz imenika",
                            "Editiranje preference kontakta",
                            "Upravljanje kontaktom",
                            "Ispis svih poziva",
                        }, menu);

                        if (menu == 0)
                        {
                            exit = true;
                            Console.WriteLine("Izlaz iz aplikacije");
                        }
                        break;

                    case Menu.PrintContacts:
                        Contact.Print(new List<Contact>(contacts.Keys));
                        menu = Choice(new string[] { }, menu);
                        break;

                    case Menu.AddContact:
                        Contact.Add(contacts);
                        menu = Choice(new string[] { }, menu);
                        break;

                    case Menu.RemoveContact:
                        Contact.Remove(contacts);
                        menu = Choice(new string[] { }, menu);
                        break;

                    case Menu.ChangeContactType:
                        Contact.ChangeType(contacts);
                        menu = Choice(new string[] { }, menu);
                        break;

                    case Menu.ManageContact:
                        menu = Choice(new string[] {
                            "Ispis svih poziva sa kontaktom",
                            "Novi poziv",
                        }, menu);
                        break;

                    case Menu.PrintCallsByContact:
                        menu = Choice(new string[] { }, menu);
                        break;

                    case Menu.AddCall:
                        menu = Choice(new string[] { }, menu);
                        break;

                    case Menu.PrintCalls:
                        Call.Print(new List<Call>(contacts.Values.SelectMany(d => d)));
                        menu = Choice(new string[] { }, menu);
                        break;
                }
            }
        }

        static int Choice(string[] actions, int prev_choice)
        {
            if (actions.Length == 0)
            {
                Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik");
                Console.ReadKey();
                return (int)Menu.Main;
            }

            Console.WriteLine("Akcije:");
            for (var i = 0; i < actions.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {actions[i]}");
            }

            if (prev_choice == 0)
            {
                Console.WriteLine("0 - Izlaz iz aplikacije");
            }
            else
            {
                Console.WriteLine("0 - Povratak u glavni izbornik");
            }

            Console.Write("Odaberite akciju: ");
            bool success = int.TryParse(Console.ReadLine(), out int choice);
            while (choice < 0 || choice > actions.Length || !success)
            {
                Console.WriteLine("Odabir mora biti jedan od brojeva u listi.");
                Console.Write("Odaberite akciju: ");
                success = int.TryParse(Console.ReadLine(), out choice);
            }

            if (prev_choice > 0 && choice != 0)
            {
                choice += prev_choice * 10;
            }

            return choice;
        }
    }
}
