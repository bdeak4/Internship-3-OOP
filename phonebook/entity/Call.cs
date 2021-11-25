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

        private string _toNumber;
        private DateTime _date;
        private CallStatus _status = CallStatus.InProgress;

        public string ToNumber { get => _toNumber; set => _toNumber = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public CallStatus Status { get => _status; set => _status = value; }


        public Call(string toNumber)
        {
            _toNumber = toNumber;
            _date = DateTime.Now;
        }

        public Call(string toNumber, DateTime date, CallStatus status)
        {
            _toNumber = toNumber;
            _date = date;
            _status = status;
        }

        static public void Print(List<Call> calls)
        {
            Console.WriteLine($"{"Prema broju".PadRight(30)} | {"Datum".PadRight(30)} | Status");
            Console.WriteLine("------------------------------ | ------------------------------ | ------------------------------");

            foreach (var call in calls)
            {
                if (call.Status is CallStatus.Missed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (call.Status is CallStatus.InProgress)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.WriteLine($"{call.ToNumber.PadRight(30)} | {call.Date.ToString().PadRight(30)} | {call.Status}");
                Console.ResetColor();
            }
        }


    }
}
