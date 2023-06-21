using ConsoleApp1;

using System.Xml.Serialization;


Person person = new Person { Name = "John", Age = 30 };

XmlSerializer serializer = new XmlSerializer(typeof(Person));
using (StringWriter writer = new StringWriter())
{

    serializer.Serialize(writer, person);
    string xmlString = writer.ToString();
    Console.WriteLine(xmlString);
}

