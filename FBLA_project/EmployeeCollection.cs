using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FBLA_project
{
    public class EmployeeCollection
    {
       
        public EmployeeCollection(List<EmployeeSchedule> employeeSchedules)
        {
            this.EmployeeSchedules = employeeSchedules;
        }
        public EmployeeCollection()
        {
            this.EmployeeSchedules = new List<EmployeeSchedule>();
        }
        /// <summary>
        /// Serealizes object into xml file
        /// </summary>
        /// <param name="fileName">xml filename</param>
        public void save(string fileName)
        {
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                (new XmlSerializer(this.GetType())).Serialize(writer, this);
                writer.Flush();
            }
        }
        public List<EmployeeSchedule> EmployeeSchedules { get; set; }
       
        
    }
}
