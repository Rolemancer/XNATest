using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;


namespace SimpleGame
{
    public class XmlManager<T>
    {
        public Type ThisType;// {get; private set;}

        public XmlManager()
        {
            ThisType = typeof(T);
        }

        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(ThisType);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }

        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(ThisType);
                xml.Serialize(writer, obj);
            }
        }
    }
}
