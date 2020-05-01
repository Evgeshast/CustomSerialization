using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task.TestHelpers
{
	public class XmlDataContractSerializerTester<T> : ISerializer<T>
	{
        private readonly XmlObjectSerializer serializer;

        public XmlDataContractSerializerTester(XmlObjectSerializer serializer)
        {
            this.serializer = serializer;
        }

        public T Deserialize(MemoryStream stream)
        {
            return (T)serializer.ReadObject(stream);
        }

        public void Serialize(T data, MemoryStream stream)
        {
            serializer.WriteObject(stream, data);
        }
    }
}
