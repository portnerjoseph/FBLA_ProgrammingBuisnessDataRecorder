using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLA_project
{
    public class Task
    {
        public Task(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
        public Task()
        {
            this.Name = "";
            this.Description = "";
        }

        public string Name { get; set; }
    

        public string Description { get; set; }
      
        public override bool Equals(object obj)
        {
            return Name.Equals(((Task)obj).Name);
        }
        public override string ToString()
        {
            return Name +"\r\n\r\n" +Description;
        }
    }
}
