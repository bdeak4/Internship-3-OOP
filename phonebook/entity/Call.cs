using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phonebook.entity
{
    class Call
    {
        public enum Status
        {
            InProgress,
            Missed,
            Ended,
        };

        private DateTime _date;
        private Status _status = Status.InProgress;

        public Call()
        {
            _date = DateTime.Now;
        }

        public Call(DateTime date, Status status)
        {
            _date = date;
            _status = status;
        }

    }
}
