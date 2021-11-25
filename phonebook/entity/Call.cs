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
                foreach (var contact in contacts)
                {
                    foreach (var call in contact.Value)
                    {
                        if (call.Status == CallStatus.InProgress)
                        {
                            call.Status = CallStatus.Ended;
                        }
                    }
                }
            }

            var number = Contact.Choice(new List<Contact>(contacts.Keys));

            if (!contacts.ContainsKey(new Contact { Number = number }))
            {
                Console.WriteLine("Kontakt sa unesenim brojem ne postoji.");
                return;
            }

            // ...
        }

        // < 5 - pozdrav gospodine zovem iz hrvatskog telekoma vezano za anketu...; *beep*
        // < 10 - e [baba, dida, caca, majko], reci; $x dodi za stol rucak se hladi; evo me za 5 minuta; dodi; odmah; *beep*
        // pitanje za miljunas

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


    }
}
