using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLA_project
{
    public class DaySchedule :IComparable
    {
       
        public DaySchedule(DayOfWeek day,List<Task> tasks,DateTime shiftStart,DateTime shiftEnd)
        {
            this.Day = day;
            this.Tasks = tasks;
            this.ShiftStart = shiftStart;
            this.ShiftEnd = shiftEnd;
        }
        public DaySchedule(DayOfWeek day)
        {
            this.Day = day;
            this.Tasks = new List<Task>();
            this.ShiftStart = new DateTime();
            ShiftEnd = new DateTime();
        }
        public DaySchedule()
        {
            this.Day = 0;
            this.Tasks = new List<Task>();
            this.ShiftStart = new DateTime();
            ShiftEnd = new DateTime();
        }
        public DayOfWeek Day { get; set; }
       

        public List<Task> Tasks { get; set; }
      

        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
     

     
        public override bool Equals(object obj)
        {
            return Day.Equals(((DaySchedule)obj).Day);
        }

        public int CompareTo(object obj)
        {
            return Day.CompareTo(((DaySchedule)obj).Day);
        }
        public override string ToString()
        {
            string output = string.Format("{0,-30}-{1,-30}{2}", Day, ShiftStart, ShiftEnd);
            output += "\r\n";
            foreach (Task n in Tasks)
            {
                output += n.Name + "\r\n";
            }
            return output+"\r\n";
        }
    }
}
