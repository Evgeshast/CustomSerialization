using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.TestHelpers
{
    public class SerializationTester<TData>
    {
        private readonly ISerializer<TData> serializer;
        private readonly bool showResult;

        public SerializationTester(ISerializer<TData> serializer, bool showResult = false)
        {
            this.serializer = serializer;
            this.showResult = showResult;
        }

        public TData SerializeAndDeserialize(TData data)
        {
            var stream = new MemoryStream();

            Console.WriteLine("Start serialization");
            serializer.Serialize(data, stream);
            Console.WriteLine("Serialization finished");

            if (showResult)
            {
                var r = Console.OutputEncoding.GetString(stream.GetBuffer(), 0, (int)stream.Length);
                Console.WriteLine(r);
            }

            stream.Seek(0, SeekOrigin.Begin);
            Console.WriteLine("Start deserialization");
            TData result = serializer.Deserialize(stream);
            Console.WriteLine("Deserialization finished");

            return result;
        }
    }
}
