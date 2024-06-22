using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiThucHanhC_
{
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}