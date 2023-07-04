using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FBLA_project
{
    public class CustomerCollection
    {
       
        public CustomerCollection(List<Customer> customers)
        {
            this.Customers = customers;
        }
        public CustomerCollection()
        {
            this.Customers = new List<Customer>();
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
        public List<Customer> Customers { get; set; }


    }
}
