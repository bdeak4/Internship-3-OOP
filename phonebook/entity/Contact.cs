using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phonebook.entity
{
    class Contact
    {
        public enum Type
        {
            Normal,
            Favorite,
            Blocked,
        };

        private string _name;
        private string _number;
        private Type _type = Type.Normal;

        public Contact(string name, string number)
        {
            _name = name;
            _number = number;
        }

        public Contact(string name, string number, Type type)
        {
            _name = name;
            _number = number;
            _type = type;
        }

    }
}
