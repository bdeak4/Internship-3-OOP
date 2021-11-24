using System;
using System.Collections.Generic;
using phonebook.entity;

namespace phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<Contact, Call[]>
            {
                { new Contact("Ante Antic", "0981234567", Contact.Type.Favorite),
                  new Call[] { new Call(new DateTime(2021, 2, 2), Call.Status.Ended), new Call() } },
                { new Contact("Ana Anic", "0951234567"),
                  new Call[] { new Call(new DateTime(2021, 11, 11), Call.Status.Ended) } },
                { new Contact("Mate Matic", "0991234567", Contact.Type.Blocked),
                  new Call[] { } },
            };
        }
    }
}
