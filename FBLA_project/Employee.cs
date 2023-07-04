using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLA_project
{
    public class Employee : IComparable
    {
   
        public Employee(string FirstName, string lastName, string email, string phone, string position)
        {
            this.FirstName = FirstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Position = position;
        }
        public Employee(string FirstName, string lastName)
        {
            this.FirstName = FirstName;
            this.LastName = lastName;
            this.Email = "";
            this.Phone = "";
            this.Position = "";
        }
        public Employee()
        {
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Position = "";
        }

        public string FirstName { get; set; }
 

        public string LastName { get; set; }
   

        public string Email { get; set; }
    

        public string Phone { get; set; }
    

        public string Position { get; set; }
    

        public int CompareTo(object obj)
        {
            return LastName.CompareTo(((Employee)obj).LastName);
        }
        public override bool Equals(object obj)
        {
            return this.LastName.Equals(((Employee)obj).LastName) && this.FirstName.Equals(((Employee)obj).FirstName);
        }
        public override string ToString()
        {
            return string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4}",FirstName,LastName,Email,Phone,Position);
        }
    }
}
