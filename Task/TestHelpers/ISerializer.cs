using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task.TestHelpers
{
    public interface ISerializer <TData>
    {
        TData Deserialize(MemoryStream stream);
        void Serialize(TData data, MemoryStream stream);
    }
}
