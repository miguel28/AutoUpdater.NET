using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace AutoUpdaterDotNET
{
    public class XMLUpdateManifest
    {
        public string version = "";
        public string url = "";
        public string changelog = "";
        public string signature = "";


        public XMLUpdateManifest()
        {

        }

        public void Serialize(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLUpdateManifest));
            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static XMLUpdateManifest Deserialize(string filename)
        {
            XMLUpdateManifest ret = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(XMLUpdateManifest));
            TextReader reader = new StreamReader(@"D:\myXml.xml");
            ret = (XMLUpdateManifest)deserializer.Deserialize(reader);
            reader.Close();
            return ret;
        }
    }
}
