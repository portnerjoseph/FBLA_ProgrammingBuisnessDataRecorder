using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FBLA_project
{
    public class TaskCollection
    {
    
        public TaskCollection()
        {
            Tasks = new List<Task>();
        }
        public TaskCollection(List<Task> tasks)
        {
            this.Tasks = tasks;
        }
        /// <summary>
        /// Serealizes object into xml file
        /// </summary>
        /// <param name="fileName">xml filename</param>
        public void save(string fileName)
        {
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                (new XmlSerializer(GetType())).Serialize(writer, this);
                writer.Flush();
            }
        }
        public List<Task> Tasks { get; set; }
        public override string ToString()
        {
            string output = "";
            for (int x = 0; x < Tasks.Count; x++)
            {
                output += "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
                output += Tasks[x];
                output += "\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n";
            }
            return output;
        }

    }
}
