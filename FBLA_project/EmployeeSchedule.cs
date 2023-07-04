using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLA_project
{
   public  class EmployeeSchedule
    {
       

        public EmployeeSchedule(List<DaySchedule> schedule,Employee employee)
        {
            this.Schedule = schedule;
            this.Employee = employee;
        }
        public EmployeeSchedule()
        {
            this.Schedule = new List<DaySchedule>();
            this.Employee = new Employee();
        }

        public List<DaySchedule> Schedule { get; set; }


        public Employee Employee { get; set; }
        public override bool Equals(object obj)
        {
            return Employee.Equals(((EmployeeSchedule)obj).Employee);
        }
        public override string ToString()
        {
            string output = "";
            for(int x=0;x<Schedule.Count;x++)
            {
                output += Schedule[x].ToString() + "\r\n";
            }
            return output;
        }

    }
}
