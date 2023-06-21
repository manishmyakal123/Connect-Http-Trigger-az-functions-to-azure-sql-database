using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FunctionApp1
{
    public static class XmlExtension
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null) { return string.Empty; }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter stringWriter = new StringWriter())
            {

                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(stringWriter, value);

                    return stringWriter.ToString();
                }

            }
        }
    }
}
