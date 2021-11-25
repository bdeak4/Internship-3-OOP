using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phonebook.entity
{
    public enum CallStatus
    {
        InProgress,
        Missed,
        Ended,
    };

    class Call
    {

        private DateTime _date;
        private CallStatus _status = CallStatus.InProgress;

        public DateTime Date { get => _date; set => _date = value; }
        public CallStatus Status { get => _status; set => _status = value; }


        public Call()
        {
            _date = DateTime.Now;
        }

        public Call(CallStatus status)
        {
            _date = DateTime.Now;
            _status = status;
        }

        public Call(DateTime date, CallStatus status)
        {
            _date = date;
            _status = status;
        }

        static public void Add(Dictionary<Contact, Call[]> contacts)
        {
            var inprogress_calls = contacts.Values
                .SelectMany(d => d)
                .Where(d => d.Status == CallStatus.InProgress)
                .Count();

            if (inprogress_calls > 0)
            {
                Console.WriteLine("Poziv u tijeku. Prekinuti poziv? (da/ne)");
                if (Console.ReadLine() != "da")
                {
                    return;
                }
                foreach (var _contact in contacts)
                {
                    foreach (var call_ in _contact.Value)
                    {
                        if (call_.Status == CallStatus.InProgress)
                        {
                            call_.Status = CallStatus.Ended;
                        }
                    }
                }
            }

            var number = Contact.Choice(new List<Contact>(contacts.Keys));
            var contact = new Contact { Number = number };

            if (!contacts.ContainsKey(contact))
            {
                Console.WriteLine("Kontakt sa unesenim brojem ne postoji.");
                return;
            }

            if (contacts.Keys.Where(c => c.Number == number).Select(c => c.Type).First() == ContactType.Blocked)
            {
                Console.WriteLine("Nije moguce obaviti poziv. Kontakt je blokiran.");
                return;
            }

            var r = new Random();
            var status = (CallStatus)r.Next(3);

            var call = new Call(status);

            switch(status)
            {
                case CallStatus.InProgress:
                    var duration = r.Next(1, 21);
                    Console.WriteLine($"Kristalna kugla kaze da ce ovaj poziv trajati {duration}s");

                    if (duration >= 10)
                    {
                        var name = contacts.Keys.Where(c => c.Number == number).Select(c => c.Name).First();
                        if (name != "")
                        {
                            name = name.Split(" ")[0];
                        }
                        else
                        {
                            name = "mali";
                        }

                        RunLines(new string[] {
                            "e majko, reci",
                            $"{name} odma dodi za stol rucak se hladi",
                            "evo me za 5 minuta",
                            "rekla san odma",
                            "ok",
                            "*beep*",
                        }, duration);
                    }
                    else
                    {
                        var company = new string[] { "hrvatskog telekoma", "tele2", "a1" };
                        RunLines(new string[] {
                            $"Pozdrav gospodine zovem iz {company[r.Next(3)]} vezano za anketu...",
                            "*beep*",
                        }, duration);
                    }

                    call.Status = CallStatus.Ended;
                    Console.WriteLine("Poziv je zavrsio.");
                    break;

                case CallStatus.Missed:
                    Console.WriteLine("Kontakt je propustio poziv.");
                    break;

                case CallStatus.Ended:
                    Console.WriteLine("Poziv je zavrsio.");
                    break;
            }
            contacts[contact] = contacts[contact].Concat(new Call[] { call }).ToArray();
        }

        static public void PrintByContact(Dictionary<Contact, Call[]> contacts)
        {
            var number = Contact.Choice(new List<Contact>(contacts.Keys));

            if (!contacts.ContainsKey(new Contact { Number = number }))
            {
                Console.WriteLine("Kontakt sa unesenim brojem ne postoji.");
                return;
            }

            Print(contacts
                .Where(c => c.Key.Number == number)
                .ToDictionary(x => x.Key, x => x.Value)
            );
        }

        static public void Print(Dictionary<Contact, Call[]> contacts)
        {
            Console.WriteLine($"{"Kontakt".PadRight(30)} | {"Datum".PadRight(30)} | Status");
            Console.WriteLine("------------------------------ | ------------------------------ | ------------------------------");

            foreach (var contact in contacts)
            {
                foreach (var call in contact.Value.OrderByDescending(c => c.Date))
                {
                    if (call.Status is CallStatus.Missed)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (call.Status is CallStatus.InProgress)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.WriteLine($"{contact.Key.Name.PadRight(30)} | {call.Date.ToString().PadRight(30)} | {call.Status}");
                    Console.ResetColor();
                }
            }
        }

        static void RunLines(string[] lines, int duration)
        {
            Console.WriteLine("transkript razgovora:");
            float p = (float)duration / lines.Length;
            foreach (var line in lines)
            {
                Task.Delay((int)(p * 1000)).Wait();
                Console.WriteLine(line);
            }
        }
    }
}
