using System;
using System.Collections.Generic;
using phonebook.entity;

namespace phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var contacts = new Dictionary<Contact, Call[]>
            {
                { new Contact("Ante Antic", "0981234567", ContactType.Favorite),
                  new Call[] { new Call(new DateTime(2021, 2, 2), Call.Status.Ended), new Call() } },
                { new Contact("Ana Anic", "0951234567"),
                  new Call[] { new Call(new DateTime(2021, 11, 11), Call.Status.Ended) } },
                { new Contact("Mate Matic", "0991234567", ContactType.Blocked),
                  new Call[] { } },
            };
            int menu = 0;
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                switch (menu)
                {
                    case 0:
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
                    case 1:
                        Contact.Print(new List<Contact>(contacts.Keys));
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 2:
                        Contact.Add(contacts);
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 3:
                        Contact.Remove(contacts);
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 4:
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 5:
                        menu = Choice(new string[] {
                            "Ispis svih poziva sa kontaktom",
                            "Novi poziv",
                        }, menu);
                        break;
                    case 51:
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 52:
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 53:
                        menu = Choice(new string[] { }, menu);
                        break;
                    case 6:
                        menu = Choice(new string[] { }, menu);
                        break;
                }
            }
        }

        static int Choice(string[] actions, int prev_choice)
        {
            Console.WriteLine("Akcije:");
            for (var i = 0; i < actions.Length; i++)
                Console.WriteLine(i + 1 + " - " + actions[i]);

            if (prev_choice == 0) Console.WriteLine("0 - Izlaz iz aplikacije");
            else Console.WriteLine("0 - Povratak u glavni izbornik");

            Console.Write("Odaberite akciju: ");
            bool success = int.TryParse(Console.ReadLine(), out int choice);
            while (choice < 0 || choice > actions.Length || !success)
            {
                Console.WriteLine("Odabir mora biti jedan od brojeva u listi.");
                Console.Write("Odaberite akciju: ");
                success = int.TryParse(Console.ReadLine(), out choice);
            }

            if (prev_choice > 0 && choice != 0)
                choice += prev_choice * 10;

            return choice;
        }
    }
}
