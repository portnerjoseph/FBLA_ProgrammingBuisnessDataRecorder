using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLA_project
{
    public class Customer :IComparable
    {


        public Customer(DateTime Time, string FirstName,string lastName,string email, string phone )
        {
            this.FirstName = FirstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            Times = new List<DateTime>{ Time };
        }

        public Customer(List<DateTime> Time, string FirstName,string lastName, string email, string phone)
        {
            this.FirstName = FirstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            Times = Time;
        }
        public Customer(string FirstName, string lastName)
        {
            this.FirstName = FirstName;
            this.LastName = lastName;
            this.Email = "";
            this.Phone = "";
            Times = new List<DateTime>();
        }
        public Customer()
        {
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            Times = new List<DateTime>();
        }

        public List<DateTime> Times { get; set; }
        
       
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        
        public string Email { get; set; }
     
        public string Phone { get; set; }
  
        public int CompareTo(object obj)
        {
            return this.LastName.CompareTo(((Customer)obj).LastName);
        }
        public override bool Equals(object obj)
        {
            return this.LastName.Equals(((Customer)obj).LastName) && this.FirstName.Equals(((Customer)obj).FirstName);
        }
        public override string ToString()
        {
            return string.Format("{0,-20}{1,-20}{2,-20}{3}", FirstName, LastName, Email, Phone);
        }
    }
}
